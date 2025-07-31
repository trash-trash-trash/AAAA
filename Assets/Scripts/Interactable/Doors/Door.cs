using System;
using UnityEngine;

[Serializable]
public class Door : Interactable
{
    public bool locked = false;
    public bool open = false;
    public bool canUnlock = false;
    private bool hasKey = false;

    private bool originalLocked;
    private bool originalCanUnlock;

    public ItemSO key;

    private Inventory inventory;
    
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
        if(iInteractTransform!=null)
            inventory = iInteractTransform.GetComponent<Inventory>();
        // Always do the check dynamically
        hasKey = false;
        canUnlock = false;

        if (locked && inventory != null)
        {
            if (inventory.playerItems.Contains(key))
            {
                hasKey = true;
                canUnlock = inventory.selectedItem == key;
            }
        }
        base.OnTriggerStay(other);
    }
    
    public override string InteractString()
    {
        if (locked)
        {
            if (!hasKey) return "LOCKED. FIND THE KEY";
            if (!canUnlock) return "KEY NOT EQUIPPED!";
            return "E: UNLOCK";
        }

        return open ? "E: CLOSE DOOR" : "E: OPEN DOOR";
    }


    public virtual void ResetDoor()
    {
        locked = originalLocked;
        canUnlock = originalCanUnlock;
    }

    public virtual void TryUnlock()
    {
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
