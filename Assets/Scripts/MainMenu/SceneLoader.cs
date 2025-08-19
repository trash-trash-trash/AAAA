using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor; // Needed for SceneAsset
#endif

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [Header("Scenes (drag .unity files here)")]
#if UNITY_EDITOR
    public List<SceneAsset> sceneAssets = new List<SceneAsset>(); // Editable in Inspector
#endif
    [HideInInspector] public List<string> sceneNames = new List<string>(); // Runtime names

    /// <summary>
    /// Invoked when a scene has finished loading.
    /// String parameter = scene name.
    /// </summary>
    public event Action<string> AnnounceSceneLoaded;

    void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Hook into Unity's sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

#if UNITY_EDITOR
        sceneNames.Clear();
        foreach (var sceneAsset in sceneAssets)
        {
            if (sceneAsset != null)
            {
                sceneNames.Add(sceneAsset.name);
            }
        }
#endif
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");
        AnnounceSceneLoaded?.Invoke(scene.name);
    }

    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name not provided!");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}