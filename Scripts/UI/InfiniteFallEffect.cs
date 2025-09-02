using UnityEngine;

public class InfiniteFallEffect : MonoBehaviour
{
    public Transform Haed;      // 플레이어
    public Transform mapRoot;     // 맵 루트

    public float triggerY = -50f;       // 기준선
    public float mapOffset = 100f;      // 맵을 얼마나 더 아래로 이동시킬지

    private bool isShifting = false;

    private UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }


    void Update()
    {
        if (uiManager != null && uiManager.isCleared)
            return; //클리어 상태면 더 이상 작동하지 않음

        if (!isShifting && Haed.position.y < triggerY && mapRoot.position.y > Haed.position.y)
        {
            isShifting = true;

            mapRoot.position = new Vector3(
                mapRoot.position.x,
                Haed.position.y - mapOffset,
                mapRoot.position.z
            );
        }
    }
   

}
