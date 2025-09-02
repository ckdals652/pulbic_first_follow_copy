using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondButtonController : MonoBehaviour
{
    public GameObject secondButtonClick;
    public GameObject secondButtonUnclick;
    public bool isClicked = false;

    private void Start()
    {
        secondButtonUnclick.SetActive(true);
        secondButtonClick.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isClicked = true;
            secondButtonUnclick.SetActive(false);
            secondButtonClick.SetActive(true);
        }
    }
}
