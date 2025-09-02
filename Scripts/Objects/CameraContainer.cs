using UnityEngine;

namespace Objects
{
    public class CameraContainer : MonoBehaviour
    {
        public GameObject mainCamera;
        private Vector3 offset;

        private void Start()
        {
            offset = mainCamera.transform.position - transform.position;
        }

        private void LateUpdate()
        {
            mainCamera.transform.position = transform.position + offset;
        }
    }
}
