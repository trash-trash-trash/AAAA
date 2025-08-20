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
                gameObject.SetActive(false);
                itemSO.AnnounceReset += Reset;
            }
        }
    }

    private void Reset()
    {
        gameObject.SetActive(true);
    }

    public override string InteractString()
    {
        return "E: PICK UP "+itemSO.itemName;
    }
}