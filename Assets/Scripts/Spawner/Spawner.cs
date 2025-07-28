using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boardPrefab;
    public GameObject followMannequinPrefab;

    public List<SpawnLocation> spawnLocations = new List<SpawnLocation>();
    public GameObject spawnedObjsParentObj;

    private Vector3 playerSpawnTransform;
    private Quaternion playerSpawnRotation;

    public Dictionary<SpawnType, GameObject> objsDict;
    public Dictionary<SpawnLocation, GameObject> spawnedObjsDict = new Dictionary<SpawnLocation, GameObject>();

    void Start()
    {
        objsDict = new Dictionary<SpawnType, GameObject>()
        {
            { SpawnType.Player , playerPrefab },
            { SpawnType.Board, boardPrefab },
            { SpawnType.FollowMannequin, followMannequinPrefab }
        };

        Spawn();
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
            playerSpawnTransform = loc.transform.position;
            playerSpawnRotation = loc.transform.rotation;

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

    public void Respawn(bool input)
    {
        if (!input)
        {
            Spawn(); // Just repositions and resets; no destruction
        }
    }
}
