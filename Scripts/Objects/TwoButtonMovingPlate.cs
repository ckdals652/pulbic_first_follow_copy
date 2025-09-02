using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoButtonMovingPlate : MonoBehaviour
{
    public FirstButtonController firstButtonController;
    public SecondButtonController secondButtonController;
    public Transform movingPlate;

    public float movePlateDistance = 10f;
    private bool isMoving = false;

    void Update()
    {
        if (firstButtonController.isClicked && secondButtonController.isClicked && !isMoving)
        {
            StartCoroutine(MovePlate());
        }
    }

    IEnumerator MovePlate()
    {
        isMoving = true;

        Vector3 startPlatePosition = movingPlate.position;
        Vector3 targetPlatePosition = startPlatePosition + movingPlate.transform.TransformDirection(Vector3.left) * movePlateDistance;

        float elapsedTime = 0f;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            movingPlate.position = Vector3.Lerp(startPlatePosition, targetPlatePosition, elapsedTime / duration);
            yield return null;
        }
    }
}
