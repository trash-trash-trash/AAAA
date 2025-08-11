using System;
using UnityEngine;

public enum Difficulty
{
    Normal,
    Hard
}

public class AAAGameManager : MonoBehaviour
{
    public static AAAGameManager Instance { get; private set; }

    public Difficulty currentDifficulty;
    public bool isGamePaused = false;

    public float musicVolume = 100f;
    public float SFXVolume = 100f;

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
}
