using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingRestoreManager : MonoBehaviour
{
    // 전역 Lighting 설정 저장용
    private Material defaultSkybox;
    private float defaultAmbientIntensity;
    private float defaultReflectionIntensity;

    public static LightingRestoreManager Instance { get; private set; }

    private void Awake()
    {
        // 싱글톤 처리
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 현재 전역 조명 설정 저장
        defaultSkybox = RenderSettings.skybox;
        defaultAmbientIntensity = RenderSettings.ambientIntensity;
        defaultReflectionIntensity = RenderSettings.reflectionIntensity;

        // 씬 변경 감지 이벤트 연결
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // DayNightCycle이 없는 씬에서는 초기 설정으로 복구
        if (FindObjectOfType<DayNightCycle>() == null)
        {
            RenderSettings.skybox = defaultSkybox;
            RenderSettings.ambientIntensity = defaultAmbientIntensity;
            RenderSettings.reflectionIntensity = defaultReflectionIntensity;

            DynamicGI.UpdateEnvironment();
            
        }
        
    }
}
