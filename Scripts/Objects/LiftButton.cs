using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftButton : MonoBehaviour
{
    public float liftDistance = 50f;
    public bool isMoving = false;

    private Vector3 startLiftPosition;
    private Vector3 endLiftPosition;

    public Transform player;
    public Transform lift;
    public GameObject clickLiftButton;

    private void Start()
    {
        GetComponent<Renderer>().enabled = true;
        clickLiftButton.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isMoving)
        {
            StartCoroutine(UpLift());
        }
    }

    IEnumerator UpLift()
    {
        isMoving = true;

        clickLiftButton.SetActive(true);
        GetComponent<Renderer>().enabled = false;

        startLiftPosition = lift.transform.position;
        endLiftPosition = startLiftPosition + Vector3.up * liftDistance;

        float elapsedTime = 0f;
        float duration = 5f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            lift.transform.position = Vector3.Lerp(startLiftPosition, endLiftPosition, elapsedTime / duration);
            yield return null;
        }

        yield return new WaitForSeconds(5f);

        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            clickLiftButton.SetActive(false);
            GetComponent<Renderer>().enabled = true;

            elapsedTime += Time.deltaTime;
            lift.transform.position = Vector3.Lerp(endLiftPosition, startLiftPosition, elapsedTime / duration);
            yield return null;
        }

        isMoving = false;
    }
}
