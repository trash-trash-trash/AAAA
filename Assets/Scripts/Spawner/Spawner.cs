using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boardPrefab;
    public GameObject followMannequinPrefab;

    public List<SpawnLocation> spawnLocations = new List<SpawnLocation>();
    public GameObject spawnedObjsParentObj;

    public GameObject spawnedPlayer;
    public Vector3 playerSpawnPosition;
    public Quaternion playerSpawnRotation;

    public Dictionary<SpawnType, GameObject> objsDict;
    public Dictionary<SpawnLocation, GameObject> spawnedObjsDict = new Dictionary<SpawnLocation, GameObject>();

    //weird shouldnt go here
    public DoorManager doorManager;
    
    public bool initialised=false;

    void Start()
    {
        objsDict = new Dictionary<SpawnType, GameObject>()
        {
            { SpawnType.Player , playerPrefab },
            { SpawnType.CardboardMannequin, boardPrefab },
            { SpawnType.FollowMannequin, followMannequinPrefab }
        };
        initialised = true;
        
        SpawnPlayer();
    }

    public void Spawn()
    {
        foreach (SpawnLocation loc in spawnLocations)
        {
            if (!objsDict.TryGetValue(loc.spawnType, out GameObject prefabToSpawn))
                continue;

            GameObject spawnedObj;
            if (spawnedObjsDict.ContainsKey(loc))
            {
                spawnedObj = spawnedObjsDict[loc];
                ResetSpawnedObject(spawnedObj, loc);
            }
            else
            {
                spawnedObj = Instantiate(prefabToSpawn, loc.transform.position, loc.transform.rotation, spawnedObjsParentObj.transform);
                spawnedObjsDict.Add(loc, spawnedObj);
                ResetSpawnedObject(spawnedObj, loc);
            }

            loc.gameObject.SetActive(false);
        }
    }

    private void ResetSpawnedObject(GameObject obj, SpawnLocation loc)
    {
        obj.transform.position = loc.transform.position;
        obj.transform.rotation = loc.transform.rotation;

        if (loc.spawnType == SpawnType.Player)
        {
            Health HP = obj.GetComponent<Health>();
            if (HP != null)
            {
                HP.AnnounceIsAlive -= Respawn;
                HP.Rez();
                HP.AnnounceIsAlive += Respawn;
            }
        }

        obj.SetActive(true);
    }

    public void SpawnPlayer()
    {
        //seems bad make separate
        //repeating code
        foreach (SpawnLocation loc in spawnLocations)
        {
            if (loc.spawnType != SpawnType.Player)
                continue;

            playerSpawnPosition = loc.transform.position;
            playerSpawnRotation = loc.transform.rotation;

            if (spawnedPlayer == null)
            {
                spawnedPlayer = Instantiate(playerPrefab, playerSpawnPosition, playerSpawnRotation);
                Health HP = spawnedPlayer.GetComponent<Health>();
                if (HP != null)
                {
                    HP.AnnounceIsAlive -= Respawn;
                    HP.Rez();
                    HP.AnnounceIsAlive += Respawn;
                }
            }
            else
            {
                ResetPlayer();
            }

            loc.gameObject.SetActive(false);
            break;
        }
    }

    private void ResetPlayer()
    {
        if (spawnedPlayer == null)
            return;

        doorManager.ResetDoors();
        
        spawnedPlayer.transform.position = playerSpawnPosition;
        spawnedPlayer.transform.rotation = playerSpawnRotation;

        Health HP = spawnedPlayer.GetComponent<Health>();
        if (HP != null)
        {
            HP.AnnounceIsAlive -= Respawn;
            HP.Rez();
            HP.AnnounceIsAlive += Respawn;
        }

        spawnedPlayer.SetActive(true);
    }

    public void Respawn(bool input)
    {
        if (!input)
        {
            Spawn();
            ResetPlayer();
        }
    }
}
