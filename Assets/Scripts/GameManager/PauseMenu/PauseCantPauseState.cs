using UnityEngine;

public class PauseCantPauseState : PauseStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        brain.canPause = false;
    }
}
