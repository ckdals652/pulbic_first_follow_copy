using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour
{
    [Tooltip("자동으로 Build Settings의 씬 이름으로 채워집니다")]
    public string[] sceneNames;

    public int currentIndex = 0;

    private void Awake()
    {
        InitializeSceneNames();

        string active = SceneManager.GetActiveScene().name;
        for (int i = 0; i < sceneNames.Length; i++)
        {
            if (sceneNames[i] == active)
            {
                currentIndex = i;
                break;
            }
        }
    }

    private void InitializeSceneNames()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        sceneNames = new string[sceneCount];

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);  // ex) "Assets/Scenes/MainScene.unity"
            string name = System.IO.Path.GetFileNameWithoutExtension(path); // "MainScene"
            sceneNames[i] = name;
        }

        //Debug.Log($"StageLoader 초기화 완료: {string.Join(", ", sceneNames)}");
    }

    private void RestoreTimeAndCursor()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadSelectedStage()
    {
        if (sceneNames.Length == 0) return;

        RestoreTimeAndCursor();
        SceneManager.LoadScene(sceneNames[currentIndex]);
    }

    public void LoadNextStage()
    {
        if (sceneNames.Length == 0)
        {
            //Debug.LogWarning("sceneNames 배열이 비어있습니다!");
            return;
        }

        currentIndex = (currentIndex + 1) % sceneNames.Length;
        //Debug.Log($"▶ LoadNextStage → {sceneNames[currentIndex]}");
        RestoreTimeAndCursor();
        SceneManager.LoadScene(sceneNames[currentIndex]);
    }

    public void LoadPreviousStage()
    {
        if (sceneNames.Length == 0) return;

        currentIndex = (currentIndex - 1 + sceneNames.Length) % sceneNames.Length;
        RestoreTimeAndCursor();
        SceneManager.LoadScene(sceneNames[currentIndex]);
    }
}
