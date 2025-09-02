using System;
using System.Collections;
using UnityEngine;

public class RightOpenButton : MonoBehaviour
{
    public GameObject Door;
    public GameObject unClickButton;

    public float openDoorDistance = 6f;
    // private float buttonDepthValue = 0.2f;

    private Vector3 startDoorPosition;
    private Vector3 endDoorPosition;

    private Vector3 startButtonPosition;
    private Vector3 endButtonPosition;

    bool isOpening = false;

    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
        unClickButton.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !isOpening)
        {
            GetComponent<Renderer>().enabled = true;
            unClickButton.SetActive(false);
            StartCoroutine(DoorOpen());
            // StartCoroutine(ClickButton());
        }
    }

    private IEnumerator DoorOpen()
    {
        isOpening = true;

        startDoorPosition = Door.transform.position;
        endDoorPosition = startDoorPosition + Door.transform.TransformDirection(Vector3.right) * openDoorDistance;

        float elapsedTime = 0f;
        float duration = 2f;


        while (elapsedTime < duration)
        {
            Door.transform.position = Vector3.Lerp(startDoorPosition, endDoorPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // private IEnumerator ClickButton()
    // {
    //     startButtonPosition = transform.position;
    //     endButtonPosition = startButtonPosition + transform.TransformDirection(Vector3.forward) * buttonDepthValue;
    //
    //     float elapsedTime = 0f;
    //     float buttonDuration = 2f;
    //
    //     while (elapsedTime < buttonDuration)
    //     {
    //         transform.position = Vector3.Lerp(startButtonPosition, endButtonPosition, elapsedTime / buttonDuration);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    // }
}
