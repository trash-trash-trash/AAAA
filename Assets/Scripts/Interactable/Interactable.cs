using System;
using UnityEngine;

[Serializable]
public class Interactable : MonoBehaviour, IInteractable
{
    public Transform playerTransform;
    public LayerMask playerLayer;

    public bool canInteractWith = true;
    public bool playerInRangeToInteract = false;

    public virtual void Interact()
    {
    }

    public virtual string InteractString()
    {
        return "";
    }

    //change to ontriggerstay? note this will trigger every frame while overlapped
    public virtual void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            playerInRangeToInteract = true;

            PlayerInteract player = other.GetComponent<PlayerInteract>();
            if (player != null)
            {
                player.SetNearbyInteractable(this);
                OnPlayerEntered(other.transform);
            }
        }
    }
    
    protected virtual void OnPlayerEntered(Transform player)
    {
        playerTransform = player;
    }

    private void OnTriggerExit(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            playerInRangeToInteract = false;

            PlayerInteract player = other.GetComponent<PlayerInteract>();
            if (player != null)
            {
                CloseInteractable(player);
            }
        }
    }

    public void CloseInteractable(PlayerInteract pI)
    {
        pI.SetNearbyInteractable(null);
    }
}