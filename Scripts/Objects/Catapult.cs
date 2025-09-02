using UnityEngine;

namespace Objects
{
    public class Catapult : MonoBehaviour
    {
        public Vector3 catapultShotVector;

        public bool isPlayerColliding = false;

        public void Start()
        {
            catapultShotVector = (this.transform.up+ (-this.transform.right)).normalized;
        }

        public void CatapultReadyShot()
        {
            isPlayerColliding = true;
            //Debug.Log(isPlayerColliding);
        }

        public void CatapultNoReadyShot()
        {
            isPlayerColliding = false;
            //Debug.Log(isPlayerColliding);
        }
    }
}
