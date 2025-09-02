using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButtonController : MonoBehaviour
{
    public GameObject ButtonClick;
    public GameObject ButtonUnclick;
    public Transform jumpPlate;

    public float jumpPlateDistance = 10f;
    private bool isMoving = false;

    private void Awake()
    {
        ButtonClick.SetActive(false);
        ButtonUnclick.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isMoving)
        {
            ButtonUnclick.SetActive(false);
            ButtonClick.SetActive(true);

            StartCoroutine(MoveJumpPlate());
        }
    }

    IEnumerator MoveJumpPlate()
    {
        isMoving = true;

        Vector3 startPlatePosition = jumpPlate.position;
        Vector3 targetPlatePosition = startPlatePosition + jumpPlate.transform.TransformDirection(Vector3.left) * jumpPlateDistance;

        float elapsedTime = 0f;
        float duration = 3f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            jumpPlate.position = Vector3.Lerp(startPlatePosition, targetPlatePosition, elapsedTime / duration);
            yield return null;
        }
    }
}
