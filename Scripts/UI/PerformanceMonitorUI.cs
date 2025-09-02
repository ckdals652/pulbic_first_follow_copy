using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PerformanceMonitorUI : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    private float deltaTime = 0.0f;

    void Update()
    {
        // FPS 계산
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // 메모리 정보
        long totalMemory = System.GC.GetTotalMemory(false); // bytes
        float memoryMB = totalMemory / (1024f * 1024f); // MB로 변환

        // FPS 정보
        float fps = 1.0f / deltaTime;
        //"현재 프레임 하나를 그리는 데 걸린 시간(ms 단위)"
        float msec = deltaTime * 1000.0f;

        // 텍스트 구성
        displayText.text = string.Format(
            "{0:0.0} ms | {1:0.} FPS\nMemory: {2:0.0} MB",
            msec, fps, memoryMB
        );
    }
}
