using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public float CurrentTime { get; private set; } = 0f;
    public bool IsRunning { get; private set; } = false;

    public float bestTime = 10000f;
    
    private const string BestTimeKey = "BestTime";

    public bool aNewRecord = false;
    
    void Start()
    {
        aNewRecord = false;
        LoadBestTime();
    }
    
    void Update()
    {
        if (!IsRunning) return;

        CurrentTime += Time.deltaTime; 
    }

    public void StartTimer()
    {
        aNewRecord = false;
        CurrentTime = 0f;
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
        if(CurrentTime<bestTime)
        {
            //TODO: SAVE/LOAD
            bestTime = CurrentTime;
            SaveBestTime();
            aNewRecord = true;
            Debug.Log("NEW BEST TIME: "+bestTime);
        }
    }

    public void Resume()
    {
        IsRunning = true;
    }

    public void ResetTimer()
    {
        CurrentTime = 0f;
    }
    
    private void SaveBestTime()
    {
        JSONPrefs.SetBestTime(bestTime);
    }

    private void LoadBestTime()
    {
            bestTime = JSONPrefs.GetBestTime();
    }
}