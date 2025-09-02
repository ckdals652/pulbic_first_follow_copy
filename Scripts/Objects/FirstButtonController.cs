using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstButtonController : MonoBehaviour
{
    public GameObject firstButtonClick;
    public GameObject firstButtonUnclick;
    public bool isClicked = false;

    private void Start()
    {
        firstButtonUnclick.SetActive(true);
        firstButtonClick.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isClicked = true;
            firstButtonUnclick.SetActive(false);
            firstButtonClick.SetActive(true);
        }
    }


}
