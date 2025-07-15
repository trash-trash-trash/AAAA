using UnityEngine;

public class ItemPickup : Interactable
{
    public ItemSO itemSO;

    public override void Interact()
    {
        if (canInteractWith && playerInRangeToInteract)
        {
            Inventory inventory = playerTransform.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(itemSO);
                PlayerInteract player = playerTransform.GetComponent<PlayerInteract>();
                CloseInteractable(player);
                Destroy(gameObject);
            }
        }
    }

    public override string InteractString()
    {
        return "PICK UP "+itemSO.itemName;
    }
}