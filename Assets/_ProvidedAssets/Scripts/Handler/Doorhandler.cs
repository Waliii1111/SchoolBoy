using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorhandler : MonoBehaviour
{
    public Transform hingePoint; // Assign the hinge point in the Inspector
    public float openAngle = 90f; // Angle to open
    public float openSpeed = 2f; // Speed of opening
    private bool isOpen = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    void Start()
    {
        if (hingePoint == null)
        {
            return;
        }

        // Save initial rotation & calculate target rotation RELATIVE to hinge
        initialRotation = hingePoint.localRotation;
        targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y + openAngle, initialRotation.eulerAngles.z);
    }

    public void Open()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenDoorRoutine());
        }
        else
        {
            StartCoroutine(CloseDoorRoutine());
        }
    }

    private IEnumerator OpenDoorRoutine()
    {
        isOpen = true;
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * openSpeed;
            hingePoint.localRotation = Quaternion.Slerp(initialRotation, targetRotation, time);
            yield return null;
        }

        hingePoint.localRotation = targetRotation;
        Debug.Log("🚪 Door Opened: " + gameObject.name);
    }
     
    private IEnumerator CloseDoorRoutine()
    {
        isOpen = false;
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * openSpeed;
            hingePoint.localRotation = Quaternion.Slerp(targetRotation, initialRotation, time);
            yield return null;
        }

        hingePoint.localRotation = initialRotation;
        Debug.Log("🚪 Door Closed: " + gameObject.name);
    }
}

