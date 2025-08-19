using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float BestTime = 10000f;
}

public static class JSONPrefs
{
    private static string folderPath = Path.Combine(Application.dataPath, "SaveLoad");
    private static string filePath = Path.Combine(folderPath, "SaveData.json");

    private static SaveData data;

    private static void EnsureLoaded()
    {
        if (data != null) return;

        Directory.CreateDirectory(folderPath);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            data = new SaveData();
            Save();
        }
    }

    public static void SetBestTime(float value)
    {
        EnsureLoaded();
        data.BestTime = value;
        Save();
    }

    public static float GetBestTime()
    {
        EnsureLoaded();
        return data.BestTime;
    }

    public static void Save()
    {
        EnsureLoaded();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
#if UNITY_EDITOR
        AssetDatabase.Refresh(); // Make Unity show the file
#endif
        Debug.Log("Saved JSONPrefs to: " + filePath);
    }
}