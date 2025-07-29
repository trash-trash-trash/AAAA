using UnityEngine;

public interface IInteractable
{
    public void Interact();

    public string InteractString();
}

public interface IInteract
{
    public void FlipCanInteract(bool input);

    public void SetNearbyInteractable(Interactable interactable);

    public bool ReturnCanInteract();
}

public interface IPlayer
{
    public Transform ReturnTransform();
}