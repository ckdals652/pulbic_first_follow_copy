using System.Collections;

using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 startDoorPosition;
    private Vector3 endDoorPosition;
    private Vector3 curDoorPosition;

    private bool isInContact = false;
    private bool isOpening = false;

    public float doorMoveDistance = 3f;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + Vector3.down * 0.3f;

        startDoorPosition = door.transform.position;
        endDoorPosition = startDoorPosition + Vector3.left * doorMoveDistance;
    }

    private void Update()
    {
        curDoorPosition = door.transform.position;

        if (isInContact)
        {
            transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime * 0.5f);

            if (Vector3.Distance(transform.position, endPosition) < 0.1f && !isOpening)
            {
                StartCoroutine(OpenDoor());
            }
        }

        else if (!isInContact)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * 3f);

            if (Vector3.Distance(transform.position, startPosition) < 0.1f && !isOpening)
            {
                StartCoroutine(CloseDoor());
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Object"))
        {
            isInContact = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Object"))
        {
            isInContact = false;
        }
    }

    private IEnumerator OpenDoor()
    {
        isOpening = true;

        float elapsedTime = 0f;
        float duration = 3f;

        while (elapsedTime < duration)
        {
            door.transform.position = Vector3.Lerp(curDoorPosition, endDoorPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isOpening = false;
    }

    private IEnumerator CloseDoor()
    {
        isOpening = true;

        float elapsedTime = 0f;
        float duration = 3f;

        while (elapsedTime < duration)
        {
            door.transform.position = Vector3.Lerp(curDoorPosition, startDoorPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isOpening = false;
    }
}
