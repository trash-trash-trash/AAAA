using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject boardPrefab;
    public GameObject followMannequinPrefab;

    public List<SpawnLocation> spawnLocations = new List<SpawnLocation>();

    public Dictionary<SpawnType, GameObject> objsDict;

    void Start()
    {
        objsDict = new Dictionary<SpawnType, GameObject>()
        {
            { SpawnType.Board, boardPrefab },
            { SpawnType.FollowMannequin, followMannequinPrefab }
        };
        Spawn();
    }

    public void Spawn()
    {
        foreach (SpawnLocation loc in spawnLocations)
        {
            if (objsDict.TryGetValue(loc.spawnType, out GameObject prefabToSpawn))
            {
                GameObject spawnedObj = Instantiate(prefabToSpawn, loc.transform.position, loc.transform.rotation);
                Destroy(loc.gameObject);
            }
        }
    }
}
