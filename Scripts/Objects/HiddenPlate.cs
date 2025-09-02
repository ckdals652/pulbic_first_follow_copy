using System.Collections;
using UnityEngine;

public class HiddenPlate : MonoBehaviour
{
    public GameObject rock;
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

        Vector3 randomPosition = new Vector3(
            Random.Range(rock.transform.position.x - 5f, rock.transform.position.x + 10f),
            rock.transform.position.y,
            rock.transform.position.z);

        GameObject newRock = Instantiate(rock, randomPosition, rock.transform.rotation);
        Destroy(newRock, 10f);

        yield return new WaitForSeconds(2.5f);

        isTouched = false;
    }
}
