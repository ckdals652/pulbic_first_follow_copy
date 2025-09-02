using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [Header("UI 슬라이더")]
    [SerializeField] private Slider sfxslider;
    [SerializeField] private Slider bgmslider;

    [Header("오디오 소스")]
    [SerializeField] private AudioSource BgmAudio;
    [SerializeField] private AudioSource SfxAudio;

    public static float BgmAudioVolume = 0.5f;
    public static float SfxAudioVolume = 0.5f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // 중복 생성 방지
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if (bgmslider != null) bgmslider.value = BgmAudioVolume;
        if (sfxslider != null) sfxslider.value = SfxAudioVolume;
    }

    private void Update()
    {
        if (bgmslider != null) BgmAudioVolume = bgmslider.value;
        if (sfxslider != null) SfxAudioVolume = sfxslider.value;

        if (BgmAudio != null) BgmAudio.volume = BgmAudioVolume;
        if (SfxAudio != null) SfxAudio.volume = SfxAudioVolume;
    }
}

