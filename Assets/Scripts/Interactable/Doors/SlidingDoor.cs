using System.Collections;
using UnityEngine;

public class SlidingDoor : Door
{
    public Transform slideTarget; 
    public Vector3 slideDirection = Vector3.up; 
    public float slideSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isOpening = false;

    void Start()
    {
        closedPosition = slideTarget.localPosition;

        Vector3 direction = slideDirection.normalized;
        float slideAmount = GetSlideAmount(slideTarget, direction);

        openPosition = closedPosition + direction * slideAmount;
    }

    public override void OpenCloseDoor()
    {
        if (locked)
        {
            base.TryUnlock();
        }
        base.OpenCloseDoor();
        if (locked)
            return;
        StopCoroutine(OpenCloseDoorCoro());
        StartCoroutine(OpenCloseDoorCoro());
    }

    IEnumerator OpenCloseDoorCoro()
    {
        isOpening = true;
        canInteractWith = false;

        Vector3 start = slideTarget.localPosition;
        Vector3 end = open ? closedPosition : openPosition;

        float distance = Vector3.Distance(start, end);
        float duration = distance / slideSpeed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            slideTarget.localPosition = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        slideTarget.localPosition = end;

        open = !open;
        isOpening = false;
        canInteractWith = true;
    }

    public override void Interact()
    {
        if (canInteractWith)
            OpenCloseDoor();
    }
    
    private float GetSlideAmount(Transform target, Vector3 direction)
    {
        Collider coll = target.GetComponent<Collider>();

        Vector3 size = coll.bounds.size;
        direction = direction.normalized;

        if (Mathf.Abs(direction.x) > 0.5f)
            return size.x;
        if (Mathf.Abs(direction.y) > 0.5f)
            return size.y;
        if (Mathf.Abs(direction.z) > 0.5f)
            return size.z;

        return 1f; // fallback
    }
    
    public override void ResetDoor()
    {
        StopAllCoroutines();
        base.ResetDoor();
        slideTarget.localPosition = closedPosition;
        open = false;
        isOpening = false;
    }
}
