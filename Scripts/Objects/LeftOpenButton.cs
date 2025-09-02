using System.Collections;

using UnityEngine;

namespace Objects
{
    public class LeftOpenButton : MonoBehaviour
    {
        public GameObject Door;
        public float openDoorDistance = 6f;
        private float buttonDepthValue = 0.2f;

        private Vector3 startDoorPosition;
        private Vector3 endDoorPosition;

        private Vector3 startButtonPosition;
        private Vector3 endButtonPosition;

        bool isOpening = false;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player" && !isOpening)
            {
                StartCoroutine(DoorOpen());
                StartCoroutine(ClickButton());
            }
        }

        private IEnumerator DoorOpen()
        {
            isOpening = true;

            startDoorPosition = Door.transform.position;
            endDoorPosition = startDoorPosition + Vector3.left * openDoorDistance;

            float elapsedTime = 0f;
            float duration = 2f;


            while (elapsedTime < duration)
            {
                //경과 시간이 정해진 지속시간이 될때까지 부드럽게 문이 작동
                Door.transform.position = Vector3.Lerp(startDoorPosition, endDoorPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator ClickButton()
        {
            startButtonPosition = transform.position;
            endButtonPosition = startButtonPosition + Vector3.forward * buttonDepthValue;

            float elapsedTime = 0f;
            float buttonDuration = 2f;

            while (elapsedTime < buttonDuration)
            {
                transform.position = Vector3.Lerp(startButtonPosition, endButtonPosition, elapsedTime / buttonDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
