using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IInteract
{
    public PlayerInputHandler playerInputs;

    public Interactable nearbyInteractable;

    [SerializeField]
    private bool canInteract = true;

    //might need to turn off the ability to Interact if the player is doing something else
    
    public bool CanInteract
    {
        get => canInteract;
        set => canInteract = value;
    }

    public event Action<Interactable> AnnounceNearbyInteractable;

    public void SetNearbyInteractable(Interactable interactable)
    {
        nearbyInteractable = interactable;

        bool canNowInteract = interactable != null;
        
        FlipCanInteract(canNowInteract);
        if(canNowInteract && interactable.canInteractWith)
            AnnounceNearbyInteractable?.Invoke(interactable);
        else
            AnnounceNearbyInteractable?.Invoke(null);
    }

    public void FlipCanInteract(bool input)
    {
        if (input)
        {
            canInteract = true;
            playerInputs.AnnounceInteract += TryInteract;
        }
        else
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
            
            if(nearbyInteractable.canInteractWith)
                AnnounceNearbyInteractable?.Invoke(nearbyInteractable);
            else
                AnnounceNearbyInteractable?.Invoke(null);
        }
    }
}