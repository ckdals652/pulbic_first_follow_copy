using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Grab : MonoBehaviour
{
    private PlayerController playerController;
    public Animator animator;
    public GameObject grabedObject;

    private Rigidbody handRigidbody;
    private Rigidbody stopGrabRigidbody;

    public bool alreadyGrabbed = false;

    private FixedJoint fixedJoint;

    public bool isGrabbed = false;
    public bool isReadyToGrab = false;

    void Start()
    {
        handRigidbody = GetComponent<Rigidbody>();
        if (handRigidbody == null)
        {
            Debug.LogError("Rigidbody component is missing from the object that has Grab.cs");
        }

        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    // public void GrabObject()
    // {
    //     if ((grabedObject != null && grabedObject.GetComponents<FixedJoint>().Length<2))
    //     {
    //         //Debug.Log("들기 : " + grabedObject);
    //         fixedJoint = grabedObject.AddComponent<FixedJoint>();
    //         fixedJoint.connectedBody = rigidbody;
    //         fixedJoint.breakForce = 9001;
    //     }
    // }

    public void ReleaseGrab()
    {
        if (isGrabbed == false)
        {
            if (grabedObject != null)
            {
                //Debug.Log("풀기 : " + grabedObject);
                Destroy(fixedJoint);
                if (stopGrabRigidbody != null)
                {
                    Destroy(stopGrabRigidbody);
                }

                grabedObject = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("넣기 : " + other);

        if (((other.gameObject.layer == (int)enumLayer.Layer.GrabObject
              || other.gameObject.layer == (int)enumLayer.Layer.GrabAndGround)
             && grabedObject == null) && isReadyToGrab)
        {
            isGrabbed = true;
            grabedObject = other.gameObject;
            fixedJoint = grabedObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = handRigidbody;
            fixedJoint.breakForce = 9999;
        }
        else if ((isReadyToGrab && (grabedObject == null)) &&
                  ((other.gameObject.layer != (int)enumLayer.Layer.SavePlayerPositionArea)
                  && (other.gameObject.layer != (int)enumLayer.Layer.ReturnPlayerArea)))
        {
            isGrabbed = true;
            grabedObject = other.gameObject;

            stopGrabRigidbody = grabedObject.GetComponent<Rigidbody>();
            if (stopGrabRigidbody == null)
            {
                //Debug.Log("리지드 없어서 추가함");
                stopGrabRigidbody = grabedObject.AddComponent<Rigidbody>();
            }
            else
            {
            }

            stopGrabRigidbody.isKinematic = true;
            fixedJoint = this.gameObject.AddComponent<FixedJoint>();
            fixedJoint.anchor = this.gameObject.transform.position;
            fixedJoint.breakForce = 9999;

            if (playerController.isGrounded == false)
            {
                playerController.isGrounded = true;
            }
        }
    }
}
