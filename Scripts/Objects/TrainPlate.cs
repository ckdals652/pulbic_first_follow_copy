using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPlate : MonoBehaviour
{
    public GameObject rock;
    public Transform spawnPoint; // 내가 위치 지정할 수 있는 빈 오브젝트

    private bool isTouched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTouched)
        {
            StartCoroutine(DropRock());
        }
    }

    IEnumerator DropRock()
    {
        isTouched = true;

        GameObject newRock = Instantiate(rock, spawnPoint.position, spawnPoint.rotation);
        Destroy(newRock, 10f);

        yield return new WaitForSeconds(2.0f);

        isTouched = false;
    }
}
