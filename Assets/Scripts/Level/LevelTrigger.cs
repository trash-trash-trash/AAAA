using System;
using UnityEngine;

[Serializable]
public class LevelTrigger : MonoBehaviour
{
    public int levelIndex;
    
    public bool active = false;

    public event Action<LevelTrigger> AnnouncePlayerEntered;
    
    public void ChangeActive(bool input)
    {
        active = input;
    }
    
    public virtual void OnTriggerEnter(Collider other)
    {
        if (!active)
            return;
        
        if (other.GetComponent<IPlayer>()!=null)
        {
            AnnouncePlayerEntered?.Invoke(this);
        }
    }
}
