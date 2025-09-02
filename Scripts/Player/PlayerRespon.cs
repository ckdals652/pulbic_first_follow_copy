using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;

public class PlayerRespon : MonoBehaviour
{
    private Rigidbody[] ragdollBodies;
    public GameObject parent;

    private Vector3 savePosition;

    void Start()
    {
        //GameObject parentObject = transform.parent.gameObject;
        ragdollBodies = parent.GetComponentsInChildren<Rigidbody>();
        savePosition = parent.transform.position;
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)enumLayer.Layer.SavePlayerPositionArea)
        {
            //Debug.Log("세이브 위치 저장" + other.gameObject.name);
            savePosition = other.gameObject.transform.position + Vector3.up * 10;
        }

        if (other.gameObject.layer == (int)enumLayer.Layer.ReturnPlayerArea)
        {
            //Debug.Log("세이브 위치 이동" + other.gameObject.name);
            TeleportRagdoll(savePosition);
        }
    }
}
