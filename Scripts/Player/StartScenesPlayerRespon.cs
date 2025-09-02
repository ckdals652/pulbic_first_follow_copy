using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;

public class StartScenesPlayerRespon : MonoBehaviour
{
    Rigidbody[] ragdollBodies;
    public GameObject parent;
    void Awake()
    {
        ragdollBodies = parent.GetComponentsInChildren<Rigidbody>();

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "RepeatRegion")
        {
            TeleportRagdoll(Vector3.zero);
        }
    }


    public void TeleportRagdoll(Vector3 targetPosition)
    {
        // 1. Kinematic으로 전환 (물리 제어 끄기)
        foreach (var rb in ragdollBodies)
        {
            rb.isKinematic = true;
        }

        // 2. 현재 위치 차이 계산
        Vector3 offset = targetPosition - transform.position;

        // 3. 모든 본 위치 이동
        foreach (var rb in ragdollBodies)
        {
            rb.transform.position += offset;
        }

        // 4. Kinematic 해제 (다시 물리 적용)
        foreach (var rb in ragdollBodies)
        {
            rb.isKinematic = false;
        }
    }
}
