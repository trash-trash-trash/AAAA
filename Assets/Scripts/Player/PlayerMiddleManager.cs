using UnityEngine;

public class PlayerMiddleManager : MonoBehaviour, IPlayer
{
    public static PlayerMiddleManager Instance { get; private set; }

    public Transform cameraRoot;
    public Inventory inventory;
    public PlayerInteract playerInteract;
    public PlayerMovementHandler playerMovementHandler;

    void Awake()
    {
        Instance = this;
        inventory.AnnounceOpenCloseInventory += StopStartMoveLook;
    }
    
    public Transform ReturnCameraRoot()
    {
        return cameraRoot;
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

    public Transform ReturnTransform()
    {
        return transform;
    }
}
