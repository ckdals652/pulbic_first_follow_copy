using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    public AudioClip[] clips;

    private Dictionary<string, AudioClip> clipDict;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        clipDict = new Dictionary<string, AudioClip>();

        
        foreach (var clip in clips)
        {
            if (clip != null && !clipDict.ContainsKey(clip.name))
                clipDict.Add(clip.name, clip);
        }
    }

    public void PlayNamedSfx(string clipName, float volume = 1f)
    {
        if (clipDict.TryGetValue(clipName, out var clip))
        {
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning($"[SFXManager] '{clipName}' 클립을 찾을 수 없습니다. clips 배열에 드래그했나요?");
        }
    }


}

