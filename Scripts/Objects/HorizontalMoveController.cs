using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMoveController : MonoBehaviour
{
    public float liftDistance = 50f;
    public bool isMoving = false;

    private Vector3 startLiftPosition;
    private Vector3 endLiftPosition;

    public GameObject clickButton;
    public GameObject unClickButton;

    public Transform player;
    public Transform lift;

    private void Start()
    {
        clickButton.SetActive(false);
        unClickButton.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isMoving)
        {
            StartCoroutine(HorizontalMove());
        }
    }

    IEnumerator HorizontalMove()
    {
        clickButton.SetActive(true);
        unClickButton.SetActive(false);

        isMoving = true;

        startLiftPosition = lift.transform.position;
        endLiftPosition = startLiftPosition + lift.transform.TransformDirection(Vector3.right) * liftDistance;

        float elapsedTime = 0f;
        float duration = 5f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            lift.transform.position = Vector3.Lerp(startLiftPosition, endLiftPosition, elapsedTime / duration);
            yield return null;
        }

        yield return new WaitForSeconds(5f);

        clickButton.SetActive(false);
        unClickButton.SetActive(true);

        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            lift.transform.position = Vector3.Lerp(endLiftPosition, startLiftPosition, elapsedTime / duration);
            yield return null;
        }

        isMoving = false;
    }
}
