using System;
using UnityEngine;

[Serializable]
public class Door : Interactable
{
    public bool locked = false;
    public bool canUnlock = false;

    private bool originalLocked;
    private bool originalCanUnlock;

    public ItemSO key;

    void OnEnable()
    {
        originalLocked = locked;
        originalCanUnlock = canUnlock;
    }
    
    public override void Interact()
    {
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if(iInteractTransform!=null){
            if (locked)
            {
                Inventory inventory = iInteractTransform.GetComponent<Inventory>();
                if (inventory != null)
                {
                    if (inventory.playerItems.Contains(key))
                    {
                        if (inventory.selectedItem == key)
                        {
                            canUnlock = true;
                            interactString = "E: UNLOCK";
                        }
                        else
                        {
                            canUnlock = false;
                            interactString = "KEY NOT EQUIPPED!";
                        }
                    }
                }
            }
        }
    }

    public virtual void ResetDoor()
    {
        locked = originalLocked;
        canUnlock = originalCanUnlock;
    }

    public virtual void TryUnlock()
    {
        if (!canUnlock)
        {
            interactString = "LOCKED! FIND KEY!";
            return;
        }
        
        Inventory inventory = iInteractTransform.GetComponent<Inventory>();
        if (inventory.selectedItem == key)
        {
            locked = false;
            inventory.RemoveItem(key);
        }
    }

    public virtual void OpenCloseDoor()
    {
    }
}
