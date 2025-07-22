using UnityEngine;

public class ItemPickup : Interactable
{
    public ItemSO itemSO;

    public override void Interact()
    {
        if (canInteractWith && iInteractInRangeToInteract)
        {
            Inventory inventory = iInteractTransform.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(itemSO);
                PlayerInteract player = iInteractTransform.GetComponent<PlayerInteract>();
                CloseInteractable(player);
                //  gameObject.SetActive(false);
                //destroying seemsbadman
                Destroy(gameObject);
            }
        }
    }

    public override string InteractString()
    {
        return "PICK UP "+itemSO.itemName;
    }
}