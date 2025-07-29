using System.Collections;
using UnityEngine;

public class SourceStyleFlicker : MonoBehaviour
{
    public GameObject targetObject;

    private void Start()
    {
        StartCoroutine(FlickerSequence());
    }

    IEnumerator FlickerSequence()
    {
        while (true)
        {
            // Flicker burst
            SetActiveState(false);
            yield return new WaitForSeconds(0.1f);
            SetActiveState(true);
            yield return new WaitForSeconds(0.05f);
            SetActiveState(false);
            yield return new WaitForSeconds(0.07f);
            SetActiveState(true);
            yield return new WaitForSeconds(0.04f);
            SetActiveState(false);
            yield return new WaitForSeconds(0.2f);
            SetActiveState(true);
            
            // Pause before next flicker burst
            yield return new WaitForSeconds(Random.Range(2.5f, 5f));
        }
    }

    private void SetActiveState(bool state)
    {
        if (targetObject != null)
            targetObject.SetActive(state);
    }
}