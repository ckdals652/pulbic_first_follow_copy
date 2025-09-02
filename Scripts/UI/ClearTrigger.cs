using UnityEngine;

public class ClearTrigger : MonoBehaviour
{
    public GameObject clearBG;
    public UIManager uiManager;
    
    private void Start()
    {
        if (clearBG != null)
            clearBG.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var uiManager = FindObjectOfType<UIManager>();
            
            if (uiManager != null)
            {
                uiManager.ShowClearScreen(); // 클리어 상태 전환
                
            }

        }
    }
}
