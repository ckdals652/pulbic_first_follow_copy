using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningLog : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(90, -90, -90);
    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
