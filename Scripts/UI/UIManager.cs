using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class UIManager : MonoBehaviour
{
    [Header("UI Screen")]
    public GameObject mainScreen;
    public GameObject stageScreen;
    public GameObject loadScreen;
    public GameObject settingScreen;
    public GameObject puaseScreen;
    public GameObject clearScreen;

    public StageSelector stageSelector;
    public bool isCleared { get; private set; } = false;


    public void StartSelectedStage()//직접 씬 이름을 적으면 그 이름으로 넘어감
    {
        string sceneName = stageSelector.GetSelectedSceneName();
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "StartScene" && Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseScreen();
        }
    }

    public void ShowStageSelect()
    {
        if (stageScreen != null) stageScreen.SetActive(true);
        if (mainScreen != null) mainScreen.SetActive(false);
        if (loadScreen != null) loadScreen.SetActive(false);

        // 시작할 때 Tutorial (index 1)을 기본 선택
        if (stageSelector != null)
        {
            stageSelector.LoadUnlockedStages();
            stageSelector.UpdateBackGround();
            stageSelector.SelectStageIndex(1);
        }
    }

    public void ShowMainScreen()//스테이지 선택에서 돌아가기 누르면 메인화면 으로 감
    {
        if (isCleared && stageSelector != null)
        {
            stageSelector.UnlockNextStage();    // ← 추가
            stageSelector.UpdateBackGround();
            isCleared = false;                 // ← 초기화
        }
        if (mainScreen != null) mainScreen.SetActive(true);
        if (stageScreen != null) stageScreen.SetActive(false);
        if (loadScreen != null) loadScreen.SetActive(false);
    }

    public void ShowLoadScreen()//불러오기 누르면 세이브파일 있는쪽으로 감
    {
        if (loadScreen != null) loadScreen.SetActive(true);
        if (stageScreen != null) stageScreen.SetActive(false);
        if (mainScreen != null) mainScreen.SetActive(false);
    }

    public void ShowSettingScreen()//설정하기로 감
    {
        if (settingScreen != null) settingScreen.SetActive(true);
        if (mainScreen != null) mainScreen.SetActive(false);
        if (stageScreen != null) stageScreen.SetActive(false);
        if (loadScreen != null) loadScreen.SetActive(false);
        if (puaseScreen != null) puaseScreen.SetActive(false);
    }

    public void ShowMainFromLoad()
    {
        Time.timeScale = 1f;

        // 씬을 다시 로드하지 않고 UI만 조작
        if (mainScreen != null) mainScreen.SetActive(true);
        if (stageScreen != null) stageScreen.SetActive(false);
        if (settingScreen != null) settingScreen.SetActive(false);
        if (loadScreen != null) loadScreen.SetActive(false);
    }


    public void ShowPauseScreen()//일시정지 오브젝트가 뜨면서 다 멈추게 해줌
    {

        if (puaseScreen != null) puaseScreen.SetActive(true);
        Time.timeScale = 0f; //타임 스또쁘

        //수민
        Cursor.lockState = CursorLockMode.None; //커서 자유롭게 이동 가능
        Cursor.visible = true; //커서를 보이게 함
    }

    public void HidePauseScreen()
    {
        if (puaseScreen != null) puaseScreen.SetActive(false);
        if (settingScreen != null) settingScreen.SetActive(false);
        Time.timeScale = 1f;

        //수민
        Cursor.lockState = CursorLockMode.Locked; //커서를 화면 중앙 고정
        Cursor.visible = false; //커서를 숨김
    }

    public void ShowClearScreen()
    {
        if (mainScreen != null) mainScreen.SetActive(false);
        if (stageScreen != null) stageScreen.SetActive(false);
        if (loadScreen != null) loadScreen.SetActive(false);
        if (settingScreen != null) settingScreen.SetActive(false);
        if (puaseScreen != null) puaseScreen.SetActive(false);

        isCleared = true; // 먼저 설정
        if (clearScreen != null) clearScreen.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //InfiniteFallEffect 비활성화
        InfiniteFallEffect effect = FindObjectOfType<InfiniteFallEffect>();
        if (effect != null)
            effect.enabled = false;

    }

    public void LoadNextStage()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        int idx = SceneManager.GetActiveScene().buildIndex;
        int last = SceneManager.sceneCountInBuildSettings - 1;
        if (idx < last)
        {
           
            SceneManager.LoadScene(idx + 1);
        }
        else
        {
            GoToMainMenu();
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("StartScene");//StartScene 수정부탁
    }
    //public void GoToMainMenu()
    //{
    //    Time.timeScale = 1f;

    //    // 씬 로드 제거
    //    if (mainScreen != null) mainScreen.SetActive(true);
    //    if (stageScreen != null) stageScreen.SetActive(false);
    //    if (settingScreen != null) settingScreen.SetActive(false);
    //    if (loadScreen != null) loadScreen.SetActive(false);
    //}

    public void OnClickNextStage()
    {
        {
            if (stageSelector != null)
            {
                stageSelector.UnlockNextStage();
            }

         
            LoadNextStage();
        }
    }

    public void ContinueAndLoadNextStage()
    {
        // 일시정지 해제
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 다음 씬으로 이동
        var loader = FindObjectOfType<StageLoader>();
        if (loader != null)
        {
            loader.LoadNextStage();
        }
        else
        {
            //Debug.LogWarning("StageLoader가 존재하지 않아서 다음 씬으로 이동할 수 없습니다.");
        }
    }
    public void QuitGame()
    {

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else       
        Application.Quit();
#endif
    }


}
