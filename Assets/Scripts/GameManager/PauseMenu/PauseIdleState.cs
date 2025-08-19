using UnityEngine;

public class PauseIdleState : PauseStateBase
{
    public GameObject pauseBackground;
    public override void OnEnable()
    {
        base.OnEnable();
        brain.canPause = true;
        pauseBackground.SetActive(false);
        brain.FlipPause(false);
        Cursor.visible = false;
    }
}
