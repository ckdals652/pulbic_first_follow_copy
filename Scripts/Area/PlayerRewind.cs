using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRewind : MonoBehaviour
{
    [System.Serializable]
    public class AreaTeleportMapping
    {
        public int areaID;
        public Vector3 teleportPosition;
    }

    [System.Serializable]
    public class BodyPart
    {
        public string name;
        public Transform partTransform;
    }

    [Header("하위 바디 파츠 리스트")]
    [SerializeField] private List<BodyPart> bodyParts = new List<BodyPart>();

    [Header("영역별 텔레포트 위치 매핑")]
    [SerializeField] private List<AreaTeleportMapping> areaMappings = new List<AreaTeleportMapping>();

    private Dictionary<Transform, Vector3> initialOffsets = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> initialRotations = new Dictionary<Transform, Quaternion>();
    private Dictionary<Transform, Vector3> originalConnectedAnchors = new Dictionary<Transform, Vector3>();

    // ⚠️ Awake 대신 Start에서 초기화
    private void OnEnable()
    {
        StartCoroutine(DeferredInit());
    }

    private IEnumerator DeferredInit()
    {
        yield return null; // 1프레임 기다리면 인스펙터 값이 반영됨
        InitializeBodyParts();
    }

    private void InitializeBodyParts()
    {
        if (bodyParts == null || bodyParts.Count == 0)
        {
            //Debug.LogError("[PlayerRewind] BodyParts가 비어 있습니다.");
            return;
        }

        Transform root = bodyParts[0].partTransform;

        foreach (var part in bodyParts)
        {
            if (part.partTransform == null) continue;

            // 위치 오프셋 & 회전 백업
            Vector3 offset = part.partTransform.position - root.position;
            initialOffsets[part.partTransform] = offset;
            initialRotations[part.partTransform] = part.partTransform.rotation;

            if (part.partTransform.TryGetComponent<ConfigurableJoint>(out var joint))
            {
                originalConnectedAnchors[part.partTransform] = joint.connectedAnchor;
            }
        }
    }

    // DetectionArea의 ID에 따라 위치 매핑 텔레포트
    public void TeleportByAreaID(int areaID)
    {
        foreach (var mapping in areaMappings)
        {
            if (mapping.areaID == areaID)
            {
                Vector3 fallStartPos = mapping.teleportPosition + Vector3.up * 30f;
                StartCoroutine(HandleTeleport(fallStartPos));
                return;
            }
        }

        Debug.LogWarning($"[PlayerRewind] areaID {areaID}에 대한 낙하 위치가 설정되지 않았습니다.");
    }

    // 실제 텔레포트 수행 (전달받은 위치 기준)
    private IEnumerator HandleTeleport(Vector3 targetPosition)
    {
        //Debug.Log("[PlayerRewind] 파츠 순간이동 시작 (매핑 기반)");

        // 1. 모든 Rigidbody 정지 및 물리 비활성화
        foreach (var part in bodyParts)
        {
            if (part.partTransform.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = true;
            }

            if (part.partTransform.TryGetComponent<ConfigurableJoint>(out var joint))
            {
                joint.autoConfigureConnectedAnchor = false;
            }
        }

        yield return null;

        // 2. 위치 및 회전 수동 복원
        foreach (var part in bodyParts)
        {
            if (part.partTransform == null) continue;

            if (initialOffsets.TryGetValue(part.partTransform, out var offset))
            {
                part.partTransform.position = targetPosition + offset;
            }

            if (initialRotations.TryGetValue(part.partTransform, out var rot))
            {
                part.partTransform.rotation = rot;
            }
        }

        yield return null;

        // 3. 물리 다시 활성화
        foreach (var part in bodyParts)
        {
            if (part.partTransform.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            if (part.partTransform.TryGetComponent<ConfigurableJoint>(out var joint))
            {
                if (originalConnectedAnchors.TryGetValue(part.partTransform, out var savedAnchor))
                {
                    joint.connectedAnchor = savedAnchor;
                }

                joint.autoConfigureConnectedAnchor = false;
                joint.enablePreprocessing = true;
            }
        }

        //Debug.Log("[PlayerRewind] 매핑된 위치 + 회전 + 조인트 복원 완료!");
    }
}
