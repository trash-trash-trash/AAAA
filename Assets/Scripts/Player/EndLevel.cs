using System;
using TMPro;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public AAAGameManager gameManager;

    public LevelManager levelManager;

    public GameObject playerObj;
    public PlayerHealth playerHealth;

    public Rank currentRank;

    public event Action<float, int, Rank, bool> AnnounceEndLevelScreen;

    void Start()
    {
        levelManager.AnnounceLevelFinished += CalculateRank;
    }

    private void CalculateRank()
    {
        gameManager = AAAGameManager.Instance;
        gameManager.timer.Stop();
        gameManager.PauseGame(true);
        float time = gameManager.timer.CurrentTime;

        //might need to hack wait...
        bool record = gameManager.timer.aNewRecord;
        //hack
        playerHealth = levelManager.spawner.spawnedPlayer.GetComponent<PlayerHealth>();

        //double hack
        PauseMenuBrain pauseMenu = levelManager.spawner.spawnedPlayer.GetComponentInChildren<PauseMenuBrain>();
        pauseMenu.ChangeState(PauseMenuStates.CantPause);

        int deaths = playerHealth.deathCounter;
        Rank rank = CalculateScore();
        AnnounceEndLevelScreen?.Invoke(time, deaths, rank, record);
    }

    Rank CalculateScore()
    {
        gameManager = AAAGameManager.Instance;
        float time = gameManager.timer.CurrentTime;
        int deaths = playerHealth.deathCounter;

        // Start at S
        int rankIndex = (int)Rank.S;

        // Time penalty
        if (time > 160f)
        {
            int timePenalty = Mathf.FloorToInt((time - 160f) / 30f) + 1;
            rankIndex += timePenalty;
        }

        // Death penalty
        rankIndex += deaths;

        // Clamp to max rank (D = 4)
        rankIndex = Mathf.Clamp(rankIndex, (int)Rank.S, (int)Rank.D);

        currentRank = (Rank)rankIndex;
        return currentRank;
    }

    public void ReturnToMainMenu()
    {
        SceneLoader loader = SceneLoader.Instance;
        loader.LoadScene("MainMenuScene");
    }

    void OnDisable()
    {
        levelManager.AnnounceLevelFinished -= CalculateRank;
    }
}

public enum Rank
{
    S,
    A,
    B,
    C,
    D
}