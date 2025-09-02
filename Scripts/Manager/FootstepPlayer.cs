using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    public string clipName = "foot";
    [Range(0f, 1f)]
    public float volume = 0.8f;

    // 애니메이션 이벤트나 코드에서 이 함수만 호출하면 됨
    public void PlayFootstep()
    {
        SFXManager.Instance.PlayNamedSfx(clipName, volume);
    }
}
