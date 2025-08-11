using UnityEngine;

public class PauseState : PauseStateBase
{
    public GameObject pauseBackground;
    
    public override void OnEnable()
    {
        base.OnEnable();
        pauseBackground.SetActive(true);
        brain.FlipPause(true);
        Cursor.visible = true;
    }

    public void Options()
    {
        brain.ChangeState(PauseMenuStates.Options);
    }

    public void Quit()
    {
        brain.ChangeState(PauseMenuStates.Quit);
    }

    private void OnDisable()
    {
    }
}
