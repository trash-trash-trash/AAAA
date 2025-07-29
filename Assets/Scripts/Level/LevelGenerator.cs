using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject currentObj;

    public GameObject emptyPrefab;
    public GameObject groundPrefab;
    public GameObject wallPrefab;
    public GameObject hallwayPrefab;

    public MyLevelPart selectedPart = MyLevelPart.Ground;
    public Vector3 spawnPosition = Vector3.zero;
    public int rotationIndex = 0;

    private Dictionary<MyLevelPart, GameObject> levelPartsDict;

    public Dictionary<Vector3, LevelPart> levelDict = new Dictionary<Vector3, LevelPart>();

    public Material ghostMaterial;
    public Material setMaterial;

    private void Start()
    {
        levelPartsDict = new Dictionary<MyLevelPart, GameObject>
        {
            { MyLevelPart.Empty, emptyPrefab },
            { MyLevelPart.Ground, groundPrefab },
            { MyLevelPart.Wall, wallPrefab },
            { MyLevelPart.Hallway, hallwayPrefab }
        };
        SpawnGhost();
    }

    public void Rotate()
    {
        int rotate = rotationIndex + 90;
        if (rotate == 360)
            rotate = 0;
        rotationIndex = rotate;
        
        Debug.Log("rotated");
        SpawnGhost();
    }

    public void SetSpawnPosition(Vector2 input)
    {
        spawnPosition = new Vector3(spawnPosition.x + input.x, 0, spawnPosition.z + input.y);
        currentObj.transform.position = spawnPosition;
    }

    private void SpawnGhost()
    {
        if (!levelPartsDict.ContainsKey(selectedPart)) return;
        
        if(currentObj!=null)
            Destroy(currentObj);

        Quaternion rotation = Quaternion.Euler(0, rotationIndex, 0);
        GameObject ghost = Instantiate(levelPartsDict[selectedPart], spawnPosition, rotation, transform);
        
        currentObj = ghost;
        SetMaterialToAllRenderers(ghost, ghostMaterial);
    }


    // Called by a UI Button or Editor
    public void SpawnLevelPart()
    {
        MyLevelPart partType = selectedPart;
        Vector3 position = spawnPosition;
        if (levelDict.TryGetValue(position, out LevelPart oldPart))
        {
            if (oldPart.Occupied)
            {
                Debug.Log("part exists there");
                ReplaceLevelPart();
                return;
            }
        }

        if (!levelPartsDict.ContainsKey(partType))
        {
            Debug.LogWarning($"No prefab for {partType}");
            return;
        }

        Quaternion rotation = Quaternion.identity;

        switch (partType)
        {
            case MyLevelPart.Ground:
                rotation = Quaternion.Euler(0, 0, 0);
                break;

            case MyLevelPart.Hallway:
                rotation = Quaternion.Euler(0, rotationIndex, 0);
                break;

            case MyLevelPart.Wall:
                rotation = Quaternion.Euler(0, rotationIndex, 0);
                break;
        }

        GameObject newPart = Instantiate(levelPartsDict[partType], position, rotation, transform);

        LevelPart levelPart = newPart.AddComponent<LevelPart>();
        levelPart.myLevelPart = partType;
        levelPart.address = position;
        levelPart.rotation = rotation.eulerAngles;
        levelPart.Occupied = true;
        levelPart.instance = newPart;
        SetMaterialToAllRenderers(newPart, setMaterial);
        levelDict.Add(position, levelPart);
        Debug.Log("Spawned " + levelPart.myLevelPart + "!");
        currentObj = null;
        SpawnGhost();
    }

    public void SetMaterialToAllRenderers(GameObject obj, Material material)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(includeInactive: true);
        foreach (var renderer in renderers)
        {
            renderer.material = material;
        }
    }
    
    public void ReplaceLevelPart()
    {
        MyLevelPart newPartType = selectedPart;
        Vector3 position = spawnPosition;
        // If an existing level part is at the position, remove it
        if (levelDict.TryGetValue(position, out LevelPart oldPart))
        {
            if (oldPart != null)
            {
                DestroyImmediate(oldPart.gameObject); // Use DestroyImmediate if replacing in Editor mode
            }

            levelDict.Remove(position);
            Debug.Log("replaced " + oldPart);
        }

        // Now spawn the new part and add it to the dictionary

        SpawnGhost();
    }

    public void ScrollSelectedPart(Vector2 scrollInput)
    {
        int direction = (int)Mathf.Sign(scrollInput.x);

        int currentIndex = (int)selectedPart;
        int enumLength = System.Enum.GetValues(typeof(MyLevelPart)).Length;

        currentIndex = (currentIndex + direction + enumLength) % enumLength;

        selectedPart = (MyLevelPart)currentIndex;
        SpawnGhost();
    }
}