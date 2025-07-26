using UnityEngine;

public class GameObjectStateManager : MonoBehaviour
{
    private GameObject currentState;

    public void ChangeState(GameObject newState)
    {
        if (newState == currentState)
            return;

        if (currentState != null)
            currentState.SetActive(false);

        newState.SetActive(true);

        currentState = newState;
    }
}