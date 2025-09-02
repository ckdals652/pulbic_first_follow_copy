using System.Collections;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public GameObject hip;
    public float jumpForce = 2f;
    private float cooldown = 0.5f;
    private bool canJump = true;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && canJump)
        {
            Rigidbody playerRigidbody = hip.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(JumpCooldown());
        }
    }

    private IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(cooldown);
        canJump = true;
    }
}
