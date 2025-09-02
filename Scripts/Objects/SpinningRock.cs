using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningRock : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(180, 0, 0);
    private Rigidbody rockRigidbody;
    public float force = 100f;

    public float bounceForce = 10f;
    private float cooldown = 0f;
    private bool canBounce = true;

    private void Start()
    {
        rockRigidbody = GetComponent<Rigidbody>();
        rockRigidbody.AddForce(Vector3.forward * force, ForceMode.Impulse);
    }

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && canBounce)
        {
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 contactNormal = other.contacts[0].normal;
                Vector3 bounceDirection = Vector3.Reflect(playerRigidbody.velocity.normalized, contactNormal);
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

                StartCoroutine(BounceCooldown());
            }
        }
    }

    private IEnumerator BounceCooldown()
    {
        canBounce = false;
        yield return new WaitForSeconds(cooldown);
        canBounce = true;
    }
}
