using System;
using UnityEngine;

[Serializable]
public class Interactable : MonoBehaviour, IInteractable
{
    public Transform iInteractTransform;

    public string interactString="";

    public bool canInteractWith = true;
    public bool iInteractInRangeToInteract = false;

    private bool triggerStay = false;

    private bool active = false;

    public virtual void Interact()
    {
    }

    public virtual string InteractString()
    {
        return interactString;
    }
    
    public virtual void OnTriggerEnter(Collider other)
    {
        if (!canInteractWith) return;
        if (other.GetComponent<IInteract>()!=null)
        {
            iInteractInRangeToInteract = true;

            IInteract iInteract = other.GetComponent<IInteract>();
            if (iInteract != null)
            {
                OnIInteractEntered(other.transform);
                iInteract.SetNearbyInteractable(this);
            }
        }
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
                OnIInteractEntered(other.transform);
                iInteract.SetNearbyInteractable(this);
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

            active = false;
        }
    }

    public void CloseInteractable(IInteract iInteract)
    {
        iInteract.SetNearbyInteractable(null);
    }
}