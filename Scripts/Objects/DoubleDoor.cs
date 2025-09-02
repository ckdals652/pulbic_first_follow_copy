using System;
using System.Collections;
using System.Collections.Generic;

using Objects;

using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    public UnClickButton firstButton;
    public UnClickButton secondButton;
    public GameObject leftDoor;
    public GameObject rightDoor;

    bool isOpening = false;
    // bool isClicked = false;

    private Vector3 leftDoorStartPosition;
    private Vector3 leftDoorEndPosition;
    private Vector3 rightDoorStartPosition;
    private Vector3 rightDoorEndPosition;

    private Vector3 startFirstButtonPosition;
    private Vector3 endFirstButtonPosition;
    private Vector3 startSecondButtonPosition;
    private Vector3 endSecondButtonPosition;

    // private float buttonDepthValue = 0.2f;

    public float openDoorDistance = 3f;

    private void Update()
    {
        if (firstButton.isPressed && secondButton.isPressed && !isOpening)
        {
            StartCoroutine(DoorOpen());
            // StartCoroutine(ClickButton());
        }
    }

    private IEnumerator DoorOpen()
    {
        isOpening = true;

        leftDoorStartPosition = leftDoor.transform.position;
        leftDoorEndPosition = leftDoorStartPosition + leftDoor.transform.TransformDirection(Vector3.left) * openDoorDistance;

        rightDoorStartPosition = rightDoor.transform.position;
        rightDoorEndPosition = rightDoorStartPosition + rightDoor.transform.TransformDirection(Vector3.right) * openDoorDistance;

        float elapsedTime = 0f;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            leftDoor.transform.position = Vector3.Lerp(leftDoorStartPosition, leftDoorEndPosition, elapsedTime / duration);
            rightDoor.transform.position = Vector3.Lerp(rightDoorStartPosition, rightDoorEndPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // private IEnumerator ClickButton()
    // {
    //     isClicked =  true;
    //
    //     startFirstButtonPosition = firstButton.transform.position;
    //     endFirstButtonPosition = startFirstButtonPosition + transform.TransformDirection(Vector3.forward) * buttonDepthValue;
    //     startSecondButtonPosition = secondButton.transform.position;
    //     endSecondButtonPosition = startSecondButtonPosition + transform.TransformDirection(Vector3.forward) * buttonDepthValue;
    //
    //     float elapsedTime = 0f;
    //     float buttonDuration = 2f;
    //
    //     while (elapsedTime < buttonDuration)
    //     {
    //         firstButton.transform.position = Vector3.Lerp(startFirstButtonPosition, endFirstButtonPosition, elapsedTime / buttonDuration);
    //         secondButton.transform.position = Vector3.Lerp(startSecondButtonPosition, endSecondButtonPosition, elapsedTime / buttonDuration);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    // }
}
