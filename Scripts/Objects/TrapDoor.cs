using System.Collections;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public Transform leftDoorPivot;
    public Transform rightDoorPivot;

    bool isOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        isOpened = true;

        float elapsedTime = 0f;
        float duration = 1f;

        Quaternion initialLeftRot = leftDoorPivot.rotation;
        Quaternion targetLeftRot = initialLeftRot * Quaternion.Euler(0, 0, -90);

        Quaternion initialRightRot = rightDoorPivot.rotation;
        Quaternion targetRightRot = initialRightRot * Quaternion.Euler(0, 0, 90);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            leftDoorPivot.rotation = Quaternion.Slerp(initialLeftRot, targetLeftRot, t);
            rightDoorPivot.rotation = Quaternion.Slerp(initialRightRot, targetRightRot, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        leftDoorPivot.rotation = targetLeftRot;
        rightDoorPivot.rotation = targetRightRot;
    }
}
