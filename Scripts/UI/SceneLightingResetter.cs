using UnityEngine;

public class SceneLightingResetter : MonoBehaviour
{
    [Header("환경광 초기값")]
    [Tooltip("이 씬에서 사용할 Skybox 머티리얼")]
    public Material skybox;

    [Tooltip("이 씬에서 사용할 Directional Light")]
    public Light sun;

    [Tooltip("환경광 밝기 (보통 1~1.5 사이 권장)")]
    public float ambientIntensity = 1f;

    // 씬 시작 전에 조명 설정을 먼저 초기화
    private void Awake()
    {
        if (skybox != null)
            RenderSettings.skybox = skybox;

        if (sun != null)
            RenderSettings.sun = sun;

        RenderSettings.ambientIntensity = ambientIntensity;
    }

    // GI(Environment lighting)가 반영되도록 갱신
    private void Start()
    {
        DynamicGI.UpdateEnvironment();
    }
}
