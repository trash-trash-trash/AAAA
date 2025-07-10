using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerInteract playerInteract;

    public GameObject interactTextObj;

    void Awake()
    {
        playerInteract.AnnounceNearbyInteractable += FlipInteractTextOnOff;
    }

    private void FlipInteractTextOnOff(bool input)
    {
        interactTextObj.SetActive(input);
    }
}
