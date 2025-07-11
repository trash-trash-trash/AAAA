using System;
using System.Collections;
using UnityEngine;

public class NormalDoor : Interactable
{
    public bool opensInwards = false;
    public bool open = false;
    public bool isOpening = false;
    private bool canInteract = true;

    public Transform pivot;
    public Quaternion initialRotation;

    public float doorRotation = 90f;
    public float doorMoveTime = 1f;

    void Start()
    {
        initialRotation = pivot.rotation;
    }

    protected override void OnPlayerEntered(Transform player)
    {
        //flips opening inwards depending on which side player entered
        Vector3 toPlayer = player.position - transform.position;
        float dot = Vector3.Dot(transform.right, toPlayer);
        opensInwards = (dot < 0);
    }

    public void OpenCloseDoor()
    {
        StopCoroutine(OpenCloseDoorCoro());
        StartCoroutine(OpenCloseDoorCoro());
    }

    IEnumerator OpenCloseDoorCoro()
    {
        isOpening = true;
        canInteract = false;

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
        canInteract = true;
    }

    public override void Interact()
    {
        if (canInteract)
            OpenCloseDoor();
    }

    public override string InteractString()
    {
        if (!open) 
            return "OPEN DOOR";

        return "CLOSE DOOR";
    }
}