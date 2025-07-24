using UnityEngine;

public class LevelGeneratorController : MonoBehaviour
{
    public PlayerInputHandler playerInputs;

    public LevelGenerator levelGenerator;

    void Start()
    {
        playerInputs.AnnounceInteract += TryPlace;
        playerInputs.AnnounceMoveVector2 += Move;
        playerInputs.AnnounceScroll += Scroll;
        playerInputs.AnnounceSprint += Rotate;
    }
    private void Rotate(bool b)
    {
        if (b)
            levelGenerator.Rotate();
    }

    private void Scroll(Vector2 input)
    {
        levelGenerator.ScrollSelectedPart(input);
    }

    private void Move(Vector2 input)
    {
        if (input != Vector2.zero)
        {
            levelGenerator.SetSpawnPosition(input);
        }
    }
    private void TryPlace(bool obj)
    {
        if(obj)
            levelGenerator.SpawnLevelPart();
    }

    void OnDisable()
    {
        playerInputs.AnnounceInteract -= TryPlace;
        playerInputs.AnnounceMoveVector2 -= Move;
        playerInputs.AnnounceScroll -= Scroll;
    }
}
