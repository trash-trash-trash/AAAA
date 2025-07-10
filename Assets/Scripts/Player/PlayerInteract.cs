using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IInteract
{
    public PlayerInputHandler playerInputs;

    private Interactable nearbyInteractable;

    [SerializeField]
    private bool canInteract = true;

    //might need to turn off the ability to Interact if the player is doing something else
    
    public bool CanInteract
    {
        get => canInteract;
        set => canInteract = value;
    }

    //make it <IInteractable> if needed later
    public event Action<bool> AnnounceNearbyInteractable;

    public void SetNearbyInteractable(Interactable interactable)
    {
        nearbyInteractable = interactable;

        bool canNowInteract = interactable != null;
        FlipCanInteract(canNowInteract);

        AnnounceNearbyInteractable?.Invoke(canNowInteract);
    }

    public void FlipCanInteract(bool input)
    {
        if (input && !canInteract)
        {
            canInteract = true;
            playerInputs.AnnounceInteract += TryInteract;
        }
        else if (!input && canInteract)
        {
            canInteract = false;
            playerInputs.AnnounceInteract -= TryInteract;
        }
    }

    private void TryInteract(bool inputPressed)
    {
        if (inputPressed && nearbyInteractable != null && nearbyInteractable.canInteractWith)
        {
            nearbyInteractable.Interact();
        }
    }
}