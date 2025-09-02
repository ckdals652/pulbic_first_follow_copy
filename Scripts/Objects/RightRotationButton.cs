using UnityEngine;

namespace Objects
{
    public class RightRotationButton : MonoBehaviour
    {
        public GameObject cannon;
        public float rotationSpeed = 1f;
        private bool isRotating = false;

        private void FixedUpdate()
        {
            if (isRotating)
            {
                cannon.transform.Rotate(0, rotationSpeed, 0);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isRotating = true;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isRotating = false;
            }
        }
    }
}
