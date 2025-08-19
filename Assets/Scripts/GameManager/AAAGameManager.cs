using System;
using UnityEditor;
using UnityEngine;

public enum Difficulty
{
    Normal,
    Hard
}

public class AAAGameManager : MonoBehaviour
{
    public static AAAGameManager Instance { get; private set; }

    public static bool gameOver = false;

    public Difficulty currentDifficulty;
    public bool isGamePaused = false;

    public float musicVolume = 100f;
    public float SFXVolume = 100f;

    public SceneLoader sceneLoader;
    
    public Timer timer;
    
    public event Action<bool> AnnouncePause;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        sceneLoader.AnnounceSceneLoaded += StartGame;
    }

    public void StartGame(string s)
    {
        if(isGamePaused)
            PauseGame(false);
        //TODO: save for different levels, different spawn pos
        timer.ResetTimer();
        timer.StartTimer();
    }

    public void StopGame()
    {
        timer.Stop();
    }

    public void ChangeDifficulty(Difficulty newDifficulty)
    {
        currentDifficulty = newDifficulty;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicVolume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        SFXVolume = volume;
    }
    
    public void PauseGame(bool pause)
    {
        isGamePaused = pause;
        Time.timeScale = pause ? 0f : 1f;
        AnnouncePause?.Invoke(pause);
    }

    private void OnDestroy()
    {
        sceneLoader.AnnounceSceneLoaded -= StartGame;
    }

    public void ReallyQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
