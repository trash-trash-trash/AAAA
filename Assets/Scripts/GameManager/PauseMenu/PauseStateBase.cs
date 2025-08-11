using UnityEngine;

public class PauseStateBase : MonoBehaviour
{
    public PauseMenuBrain brain;
    
    public virtual void OnEnable()
    {
        brain = GetComponentInParent<PauseMenuBrain>();
    }
}
