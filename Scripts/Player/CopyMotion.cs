using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public class CopyMotion : MonoBehaviour
{
    [Tooltip("따라갈 대상 본 (타겟 애니메이션 본)")]
    public Transform targetLimb;

    private ConfigurableJoint configurableJoint;

    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
    }

    void FixedUpdate()
    {
        if (targetLimb == null) return;

        // 타겟의 로컬 회전을 그대로 복사해서 조인트의 타겟 회전으로 설정
        configurableJoint.targetRotation = Quaternion.Inverse(targetLimb.localRotation);
    }
}
