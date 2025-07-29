using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerInteract playerInteract;

    public GameObject interactTextObj;

    public TMP_Text interactText;

    void Awake()
    {
        playerInteract.AnnounceNearbyInteractable += FlipInteractTextOnOff;
    }

    private void FlipInteractTextOnOff(Interactable interactable)
    {
        if(interactable!=null && playerInteract.CanInteract && !playerInteract.interactDisabled)
        {
            interactText.text = interactable.InteractString();
            interactTextObj.SetActive(true);
        }
        else
            interactTextObj.SetActive(false);
    }

    void OnDisable()
    {
        playerInteract.AnnounceNearbyInteractable -= FlipInteractTextOnOff;
    }
}
