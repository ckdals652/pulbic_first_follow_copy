using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    public PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer == (int)enumLayer.Layer.Ground)
            || (collision.gameObject.layer == (int)enumLayer.Layer.GrabAndGround))
        {
            playerController.isGrounded = true;
        }
    }
}
