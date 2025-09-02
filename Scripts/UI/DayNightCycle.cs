using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time Settings")]
    [HideInInspector] public float time = 0f; // 현재 시간
    public float fullDayLength = 120f;        // 하루 길이
    public float startTime = 0f;              // 시작 시간 (0 = 새벽, 0.5 = 정오)
    private float timeRate;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;
    public float sunStartRotation = 3f;
    public float sunEndRotation = 60f;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;
    public float moonStartRotation = 183f;
    public float moonEndRotation = 240f;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    [Header("Skybox")]
    public Material skyboxMaterial; // 이 씬 전용 스카이박스 지정

    private void Awake()
    {
        // 씬이 시작될 때 강제 초기화
        time = startTime;

        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
            DynamicGI.UpdateEnvironment();
        }
    }

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;

        SetLightRotation(sun, sunStartRotation);
        SetLightRotation(moon, moonStartRotation);
    }

    private void Update()
    {
        UpdateLight(sun, sunColor, sunIntensity, sunStartRotation, sunEndRotation);
        UpdateLight(moon, moonColor, moonIntensity, moonStartRotation, moonEndRotation);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);

        time = (time + timeRate * Time.deltaTime) % 1.0f;
    }

    private void UpdateLight(Light lightSource, Gradient colorGradient, AnimationCurve intensityCurve, float startRot, float endRot)
    {
        if (lightSource == null) return;

        float xRotation = Mathf.Lerp(startRot, endRot, time);
        lightSource.transform.eulerAngles = new Vector3(xRotation, -30f, 0f);

        lightSource.color = colorGradient.Evaluate(time);
        lightSource.intensity = intensityCurve.Evaluate(time);

        GameObject go = lightSource.gameObject;
        bool shouldBeActive = lightSource.intensity > 0f;

        if (shouldBeActive && !go.activeInHierarchy)
            go.SetActive(true);
        else if (!shouldBeActive && go.activeInHierarchy)
            go.SetActive(false);
    }

    private void SetLightRotation(Light lightSource, float xRotation)
    {
        if (lightSource != null)
            lightSource.transform.eulerAngles = new Vector3(xRotation, -30f, 0f);
    }
}
