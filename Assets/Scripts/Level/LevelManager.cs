using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelTrigger activeTrigger;
    public LevelTrigger previousTrigger;
    
    public List<LevelTrigger> levelTriggers = new List<LevelTrigger>();

    public Queue<LevelTrigger> triggerQueue = new Queue<LevelTrigger>();

    public List<GameObject> spawnParents = new List<GameObject>();
    
    public Spawner spawner;
    
    public int spawnIndex = 0;

    void Start()
    {
        foreach (LevelTrigger trigger in levelTriggers)
        {
            trigger.gameObject.SetActive(false);
            triggerQueue.Enqueue(trigger);
        }

        StartCoroutine(WaitForSpawner());
    }

    IEnumerator WaitForSpawner()
    {
        yield return new WaitForFixedUpdate();
        
        if (!spawner.initialised)
            yield return new WaitForFixedUpdate();
        
        ActivateNextTrigger();
    }


    void ActivateNextTrigger()
    {
        if (triggerQueue.Count == 0) return;

        if (activeTrigger != null)
        {
            previousTrigger = activeTrigger;
            previousTrigger.active = false;
        }

        activeTrigger = triggerQueue.Dequeue();

        if (spawner.spawnedPlayer == null)
            spawner.SpawnPlayer();
        
        if (previousTrigger != null)
        {
            spawner.playerSpawnPosition = previousTrigger.transform.position;
            spawner.playerSpawnRotation = previousTrigger.transform.rotation;
        }

        activeTrigger.gameObject.SetActive(true);
        activeTrigger.AnnouncePlayerEntered += TriggerTriggered;
    }


    private void TriggerTriggered(LevelTrigger trigger)
    {
        activeTrigger.AnnouncePlayerEntered -= TriggerTriggered;
        ActivateSpawner();

        ActivateNextTrigger();
    }
    
    void ActivateSpawner()
    {
        if (spawnIndex < spawnParents.Count)
        {
            GameObject currentParent = spawnParents[spawnIndex];
            SpawnLocation[] foundLocations = currentParent.GetComponentsInChildren<SpawnLocation>(true);

            spawner.spawnLocations = new List<SpawnLocation>(foundLocations);

            spawner.Spawn();

            spawnIndex++;
        }
    }
}
