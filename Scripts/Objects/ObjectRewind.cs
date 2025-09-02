using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class ObjectRewind : MonoBehaviour
{
    private Vector3 orignPosition;

    private void Start()
    {
        orignPosition = gameObject.GetComponent<Transform>().position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)enumLayer.Layer.ReturnPlayerArea)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.transform.position = orignPosition + Vector3.up * 5;
        }
    }
}
