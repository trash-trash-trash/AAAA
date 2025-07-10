using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public LayerMask playerLayer;

    public bool canInteractWith = true;
    public bool playerInRangeToInteract = false;

    public virtual void Interact()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            playerInRangeToInteract = true;

            PlayerInteract player = other.GetComponent<PlayerInteract>();
            if (player != null)
            {
                player.SetNearbyInteractable(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            playerInRangeToInteract = false;

            PlayerInteract player = other.GetComponent<PlayerInteract>();
            if (player != null)
            {
                player.SetNearbyInteractable(null);
            }
        }
    }
}