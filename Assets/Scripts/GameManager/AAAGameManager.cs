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
    

    public void PauseGame(bool pause)
    {
        isGamePaused = pause;
        Time.timeScale = pause ? 0f : 1f;
    }
}
