using System;
using System.Collections;
using UnityEngine;

public class NormalDoor : Door
{
    public bool opensInwards = false;
    public bool open = false;
    public bool isOpening = false;

    public Transform pivot;
    public Quaternion initialRotation;

    public float doorRotation = 90f;
    public float doorMoveTime = 1f;

    void Start()
    {
        initialRotation = pivot.rotation;
    }

    protected override void OnIInteractEntered(Transform other)
    {
        base.OnIInteractEntered(other);
        //flips opening inwards depending on which side player entered
        Vector3 rotationToOther = other.position - transform.position;
        float dot = Vector3.Dot(transform.right, rotationToOther);
        opensInwards = (dot < 0);
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

        Quaternion targetRotation;
        if (!open)
        {
            float yRotate = opensInwards ? doorRotation : -doorRotation;
            targetRotation = initialRotation * Quaternion.Euler(0, yRotate, 0);
        }
        else
        {
            targetRotation = initialRotation;
        }

        float time = 0f;
        float duration = doorMoveTime;
        Quaternion startRotation = pivot.rotation;

        while (time < duration)
        {
            pivot.rotation = Quaternion.Slerp(startRotation, targetRotation, time / duration);
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        pivot.rotation = targetRotation;

        if (pivot.rotation == initialRotation)
            open = false;
        else
            open = true;

        isOpening = false;
        canInteractWith = true;
    }

    public override void Interact()
    {
        if (canInteractWith)
            OpenCloseDoor();
    }

    public override string InteractString()
    {
        if (!locked && canUnlock)
        {
            if (!open)
                interactString = "E: OPEN DOOR";

            else
                interactString = "E: CLOSE DOOR";
        }

        return interactString;
    }

    public override void ResetDoor()
    {
        StopAllCoroutines();
        base.ResetDoor();
        pivot.rotation = initialRotation;
        open = false;
        isOpening = false;
    }
}