using System.Collections;

using UnityEngine;

public class EarthquakeButton : MonoBehaviour
{
    [Header("으악 지진이야")]
    public Transform mapTransform; // 흔들고 싶은 맵 전체
    public float shakeDuration = 1.5f;
    public float shakeMagnitude = 0.5f;

    [Header("고소 에셋")]
    public GameObject[] objectsToReveal;
    public float fadeDuration = 2f;

    

    private Vector3 originalMapPos;
    private bool triggered = false;

    private void Start()
    {
        originalMapPos = mapTransform.position;

        foreach (GameObject obj in objectsToReveal)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                if (r.material.HasProperty("_Color"))
                {
                    Color c = r.material.color;
                    c.a = 0f;
                    r.material.color = c;
                    SetMaterialToTransparent(r.material);
                }
            }

            obj.SetActive(true); // 창에 켜져 있어야함
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(ShakeThenFadeAll());
            GetComponent<Renderer>().enabled = false;

        }
    }

    IEnumerator ShakeThenFadeAll()
    {
        float elapsed = 0f;

        // 흔들 흔들려
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            float z = Random.Range(-1f, 1f) * shakeMagnitude * 0.3f;

            mapTransform.position = originalMapPos + new Vector3(x, y, z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mapTransform.position = originalMapPos;

        // 어 1,2,3,4 호카게들이 생겨나고 있어
        float fadeTime = 0f;
        while (fadeTime < fadeDuration)
        {
            foreach (GameObject obj in objectsToReveal)
            {
                Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renderers)
                {
                    if (r.material.HasProperty("_Color"))
                    {
                        Color c = r.material.color;
                        c.a = Mathf.Lerp(0f, 1f, fadeTime / fadeDuration);
                        r.material.color = c;
                    }
                }
            }

            fadeTime += Time.deltaTime;
            yield return null;
        }

        // 알파 값 다시 제자리로
        foreach (GameObject obj in objectsToReveal)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                if (r.material.HasProperty("_Color"))
                {
                    Color c = r.material.color;
                    c.a = 1f;
                    r.material.color = c;
                }
            }
        }
    }

    void SetMaterialToTransparent(Material mat)
    {
        mat.SetFloat("_Mode", 3); // Transparent
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }
}
