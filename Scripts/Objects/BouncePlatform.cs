using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem.HID;

public class BouncePlatform : MonoBehaviour
{
    public GameObject hip;
    public float bounceForce = 10f;
    private float cooldown = 0f;
    private bool canBounce = true;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && canBounce)
        {
            Rigidbody playerRigidbody = hip.GetComponent<Rigidbody>();
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
