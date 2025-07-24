using System;
using UnityEngine;

[Serializable]
public class Interactable : MonoBehaviour, IInteractable
{
    public Transform iInteractTransform;

    public bool canInteractWith = true;
    public bool iInteractInRangeToInteract = false;

    public virtual void Interact()
    {
    }

    public virtual string InteractString()
    {
        return "";
    }

    //change to ontriggerstay? note this will trigger every frame while overlapped
    public virtual void OnTriggerStay(Collider other)
    {
        if (!canInteractWith)
            return;
        
        if (other.GetComponent<IInteract>()!=null)
        {
            iInteractInRangeToInteract = true;

            IInteract iInteract = other.GetComponent<IInteract>();
            if (iInteract != null)
            {
                iInteract.SetNearbyInteractable(this);
                OnIInteractEntered(other.transform);
            }
        }
    }
    
    protected virtual void OnIInteractEntered(Transform other)
    {
        iInteractTransform = other;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteract>()!=null)
        {
            iInteractInRangeToInteract = false;

            IInteract iInteract = other.GetComponent<IInteract>();
            if (iInteract != null)
            {
                CloseInteractable(iInteract);
            }
        }
    }

    public void CloseInteractable(IInteract iInteract)
    {
        iInteract.SetNearbyInteractable(null);
    }
}