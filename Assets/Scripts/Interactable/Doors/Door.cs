using UnityEngine;

public class Door : Interactable
{
    public bool locked = false;

    public ItemSO key;
    
    public override void Interact()
    {
    }
    

    public virtual void TryUnlock()
    {
        Inventory inventory = iInteractTransform.GetComponent<Inventory>();
        if (inventory.playerItems.Contains(key))
        {
            Debug.Log("Opened!");
            locked = false;
            inventory.RemoveItem(key);
            return;
        }
        
            //chk chk noise
            Debug.Log("locked!");

    }

    public virtual void OpenCloseDoor()
    {
        if (locked)
        {
            //chkchk
            Debug.Log("locked!");
            return;
        }
    }
}
