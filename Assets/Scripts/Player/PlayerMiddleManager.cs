using System;
using UnityEngine;

public class PlayerMiddleManager : MonoBehaviour, IPlayer
{
    public static PlayerMiddleManager Instance { get; private set; }

    public Transform cameraRoot;
    public Inventory inventory;
    public Health health;
    public PlayerInteract playerInteract;
    public PlayerMovementHandler playerMovementHandler;

    void Awake()
    {
        Instance = this;
        inventory.AnnounceOpenCloseInventory += StopStartMoveLook;
        health.AnnounceIsAlive += ResetInventory;
    }

    private void ResetInventory(bool input)
    {
        if (!input)
        {
            inventory.Reset();
        }
    }

    public Transform ReturnCameraRoot()
    {
        return cameraRoot;
    }
    
    private void StopStartMoveLook(bool input)
    {
        if (input)
        {
            playerInteract.interactDisabled = true;
            playerMovementHandler.CanLook = false;
            playerMovementHandler.CanMove = false;
            Cursor.visible = true;
        }
        else
        {
            playerInteract.interactDisabled = false;
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
