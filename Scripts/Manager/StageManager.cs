using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int stageIndex; // 1 = Tutorial, 2 = MainScene 등

    private const string StageKeyPrefix = "StageCleared_";
    private bool alreadyCleared = false;

    public void ClearStage()
    {
        if (alreadyCleared) return; // 중복 호출 방지
        alreadyCleared = true;

        SaveStageClear(stageIndex);
        //Debug.Log($"Stage {stageIndex} 클리어 저장됨");

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        var loader = FindObjectOfType<StageLoader>();
        if (loader != null)
        {
            loader.LoadNextStage();
        }
        else
        {
            //Debug.LogWarning("StageLoader가 존재하지 않음");
        }
    }


    private void SaveStageClear(int index)
    {
        PlayerPrefs.SetInt(StageKeyPrefix + index, 1);
        PlayerPrefs.Save();
    }

    public static bool IsStageCleared(int index)
    {
        return PlayerPrefs.GetInt(StageKeyPrefix + index, 0) == 1;
    }
}
