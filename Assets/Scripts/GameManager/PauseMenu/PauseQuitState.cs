#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class PauseQuitState : PauseStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
    }

    public void DontQuit()
    {
        brain.ChangeState(PauseMenuStates.Paused);
    }

    public void ReallyQuit()
    {
        brain.gameManager.ReallyQuit();
    }
}