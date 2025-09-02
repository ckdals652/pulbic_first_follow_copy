using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private Vector3 previousPosition;
    private List<Transform> playersOnPlatform = new List<Transform>();

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        Vector3 delta = transform.position - previousPosition;

        foreach (Transform player in playersOnPlatform)
        {
            player.position += delta;
        }

        previousPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playersOnPlatform.Contains(collision.transform))
        {
            playersOnPlatform.Add(collision.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && playersOnPlatform.Contains(collision.transform))
        {
            playersOnPlatform.Remove(collision.transform);
        }
    }
}
