using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PauseMenuBrain : MonoBehaviour
{
    public PauseMenuStates currentState;

    public AAAGameManager gameManager;
    public PlayerInputHandler playerInputs;
    public GameObject idleObj;
    public GameObject cantPauseObj;
    public GameObject pauseObj;
    public GameObject optionsObj;
    public GameObject quitObj;

    public GameObjectStateManager stateManager;

    private Dictionary<PauseMenuStates, GameObject> statesDict;

    public bool canPause = true;

    void Awake()
    {
        if (statesDict == null)
        {
            statesDict = new Dictionary<PauseMenuStates, GameObject>()
            {
                { PauseMenuStates.Idle, idleObj },
                { PauseMenuStates.CantPause, cantPauseObj },
                { PauseMenuStates.Paused, pauseObj },
                { PauseMenuStates.Options, optionsObj },
                { PauseMenuStates.Quit, quitObj }
            };
        }

        gameManager = AAAGameManager.Instance;
        ChangeState(PauseMenuStates.Idle);
        playerInputs.AnnounceQuit += FlipPauseState;
    }

    private void FlipPauseState(bool obj)
    {
        if (!canPause)
            return;
        if (obj)
        {
            if (currentState == PauseMenuStates.Paused)
            {
                ChangeState(PauseMenuStates.Idle);
            }
            else
            {
                //might be bit too general
                ChangeState(PauseMenuStates.Paused);
            }
        }
    }

    public void ChangeState(PauseMenuStates state)
    {
        if (statesDict.TryGetValue(state, out GameObject obj))
        {
            currentState = state;
            stateManager.ChangeState(obj);
        }
    }

    public void FlipPause(bool obj)
    {
        if (obj)
        {
            gameManager.PauseGame(true);
        }
        else
        {
            gameManager.PauseGame(false);
        }
    }

    void OnDisable()
    {
        playerInputs.AnnounceQuit -= FlipPauseState;
    }
}

public enum PauseMenuStates
{
    Idle,
    CantPause,
    Paused,
    Options,
    Quit
}