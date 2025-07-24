using UnityEngine;

public class PlayerMiddleManager : MonoBehaviour
{
    public Inventory inventory;
    public PlayerInteract playerInteract;
    public PlayerMovementHandler playerMovementHandler;

    void Start()
    {
        inventory.AnnounceOpenCloseInventory += StopStartMoveLook;
    }

    private void StopStartMoveLook(bool input)
    {
        if (input)
        {
            playerInteract.CanInteract = false;
            playerMovementHandler.CanLook = false;
            playerMovementHandler.CanMove = false;
            Cursor.visible = true;
        }
        else
        {
            playerInteract.CanInteract = true;
            playerMovementHandler.CanLook = true;
            playerMovementHandler.CanMove = true;
            Cursor.visible = false;
        }
    }

    void OnDisable()
    {
        inventory.AnnounceOpenCloseInventory -= StopStartMoveLook;
    }
}
