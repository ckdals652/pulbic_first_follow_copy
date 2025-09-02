using UnityEngine;

namespace Objects
{
    public class Cannon : MonoBehaviour
    {
        public float cannonForce;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "CannonBall")
            {
                Rigidbody cannonRigidbody = other.gameObject.GetComponent<Rigidbody>();
                cannonRigidbody.AddForce(transform.up * cannonForce);
            }
        }
    }
}
