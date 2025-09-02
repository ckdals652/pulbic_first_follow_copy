using UnityEngine;

namespace Objects
{
    public class WheelLever : MonoBehaviour
    {
        public Transform cannonMuzzle;
        public float cannonMuzzleMaxAngle = 30f;

        private void FixedUpdate()
        {
            float leverX = transform.localEulerAngles.x;
            if (leverX > 180f) leverX -= 360f;
            float t = Mathf.Sin(leverX * Mathf.Deg2Rad);
            float turretAngle = t * cannonMuzzleMaxAngle;
            cannonMuzzle.localRotation = Quaternion.Euler(turretAngle, 0f, 0f);
        }
    }
}
