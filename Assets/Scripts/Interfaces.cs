public interface IInteractable
{
    public void Interact();

    public string InteractString();
}

public interface IInteract
{
    public void FlipCanInteract(bool input);

    public void SetNearbyInteractable(Interactable interactable);
}