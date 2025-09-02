using System;

using UnityEngine;

namespace Objects
{
    public class CatapultSpoon : MonoBehaviour
    {
        public Rigidbody playerRigidbody;
        private Rigidbody subObject;
        private float shotPower = 1500f;

        public Catapult catapult;

        private int shotCounter = 0;

        //public float dis;

        // public void OnCollisionEnter(Collision collision)
        // {
        //     Debug.Log("닿긴함");
        //     if ((collision.gameObject.layer == (int)enumLayer.Layer.NoSelfCollision) && (catapult.isPlayerColliding))
        //     {
        //         playerRigidbody.AddForce(catapult.catapultShotVector * shotPower, ForceMode.Impulse);
        //     }
        // }


        // private void Update()
        // {
        //     dis = Vector3.Distance(playerRigidbody.transform.position, transform.position);
        //     //Debug.Log(dis);
        // }

        // public void OnCollisionEnter(Collision collision)
        // {
        //     Debug.Log("닿긴함");
        //     if ((collision.gameObject.layer == (int)enumLayer.Layer.NoSelfCollision) && (catapult.isPlayerColliding))
        //     {
        //         playerRigidbody.AddForce(catapult.catapultShotVector * shotPower, ForceMode.Impulse);
        //     }
        // }

        public void OnTriggerStay(Collider other)
        {
            //Debug.Log(other.GetComponent<Rigidbody>());
            //Debug.Log("닿긴함");
            if (((other.gameObject.layer == (int)enumLayer.Layer.NoSelfCollision)
                 && (catapult.isPlayerColliding)
                 && shotCounter == 0))
            {
                shotCounter++;
                //Debug.Log("쏘기 :" + shotCounter);
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.AddForce(catapult.catapultShotVector * shotPower, ForceMode.Impulse);
            }

            if (other.gameObject.TryGetComponent<Rigidbody>(out subObject)
                && (catapult.isPlayerColliding)
                && other.gameObject.layer != (int)enumLayer.Layer.NoSelfCollision)
            {
                subObject.AddForce(catapult.catapultShotVector * shotPower * (subObject.mass / 30f), ForceMode.Impulse);
                //Debug.Log("쏘기");
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (((other.gameObject.layer == (int)enumLayer.Layer.NoSelfCollision)
                 && (catapult.isPlayerColliding)
                 && shotCounter > 0))
            {
                shotCounter = 0;
                //Debug.Log("초기화 :" + shotCounter);
            }
        }
    }
}
