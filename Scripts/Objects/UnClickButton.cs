using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnClickButton : MonoBehaviour
{
    public GameObject ButtonClick;

    private void Start()
    {
        ButtonClick.SetActive(false);
        this.gameObject.SetActive(true);
    }

    public bool isPressed { get; private set; }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPressed = true;
            ButtonClick.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPressed = false;
        }
    }
}
