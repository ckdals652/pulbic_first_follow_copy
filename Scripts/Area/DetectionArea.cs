using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DetectionArea : MonoBehaviour
{
    [Tooltip("이 DetectionArea가 속한 번호 (1~7)")]
    public int areaID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 하위 오브젝트(Hips)에서 상위(MovingPlayer)의 PlayerRewind를 찾음
            var rewind = other.GetComponentInParent<PlayerRewind>();
            if (rewind != null)
            {
                rewind.TeleportByAreaID(areaID);
            }
        }
    }
}
