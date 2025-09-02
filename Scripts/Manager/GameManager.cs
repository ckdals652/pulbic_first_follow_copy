using UnityEngine;

// GameManager 클래스는 전체 게임의 흐름(상태)을 관리하는 중심 컨트롤러
public sealed class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스 선언: GameManager.Instance로 접근 가능
    public static GameManager Instance { get; private set; }

    // 게임의 상태를 나타내는 열거형(enum)
    public enum GameState
    {
        GameStart,   // 게임 시작 전 대기 상태
        Playing,     // 실제 게임 플레이 중 상태
        GameOver,    // 게임 오버 상태
        Paused       // 일시정지 상태
    }

    // 현재 게임의 상태를 저장하는 변수
    public GameState CurrentState { get; private set; }

    // 게임 오브젝트 초기화 (게임 실행 시 가장 먼저 호출됨)
    private void Awake()
    {
        // 싱글톤 중복 방지: 이미 존재하면 자기 자신 제거
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복된 GameManager 제거
            return;
        }

        // 현재 인스턴스를 싱글톤으로 등록
        Instance = this;

        // 씬 전환 시에도 GameManager 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);
    }

    // 게임 시작 후 자동으로 실행되는 초기 진입 지점
    private void Start()
    {
        // 초기 게임 상태를 GameStart로 설정
        ChangeState(GameState.GameStart);
    }

    // 게임 상태 변경을 처리하는 메서드
    public void ChangeState(GameState newState)
    {
        // 새로운 상태로 현재 상태 업데이트
        CurrentState = newState;

        // 상태에 따라 전용 핸들러 호출
        switch (newState)
        {
            case GameState.GameStart:
                HandleGameStart(); // 게임 시작 처리
                break;

            case GameState.Playing:
                HandleGamePlaying(); // 플레이 시작 처리
                break;

            case GameState.GameOver:
                HandleGameOver(); // 게임 오버 처리
                break;

            case GameState.Paused:
                HandleGamePaused(); // 일시정지 처리
                break;
        }
    }

    // 게임 시작 처리 로직
    private void HandleGameStart()
    {
        Time.timeScale = 1f; // 시간 흐름 재개 (혹시 멈춰있을 수도 있으므로 초기화)

        // UI 매니저에게 시작 UI 출력 요청
        //UIManager.Instance?.ShowGameStartUI();

        // Stage 초기화 (장애물, 배경 등 초기 상태 세팅)
        //StageManager.Instance?.InitStage();

        // 플레이어 조작 활성화
        //PlayerHandler handler = FindObjectOfType<PlayerHandler>();
        //handler?.enabled = true;
    }

    // 플레이 중 상태 진입 시 처리
    private void HandleGamePlaying()
    {
        Time.timeScale = 1f; // 시간 흐름 정상화
        //UIManager.Instance?.ShowInGameUI(); // 인게임 UI 표시
    }

    // 게임 오버 처리 로직
    private void HandleGameOver()
    {
        Time.timeScale = 0f; // 게임 정지
        //UIManager.Instance?.ShowGameOverUI(); // 게임오버 UI 표시
        //SoundManager.Instance?.PlayGameOverSound(); // 사운드 출력
    }

    // 일시정지 처리 로직
    private void HandleGamePaused()
    {
        Time.timeScale = 0f; // 시간 정지
        //UIManager.Instance?.ShowPauseUI(); // 일시정지 UI 표시
    }

    // 외부에서 GameOver를 요청할 때 사용하는 공개 메서드
    public void TriggerGameOver()
    {
        ChangeState(GameState.GameOver);
    }

    // 현재 게임이 진행 중인지 외부에서 확인할 수 있도록 제공하는 속성
    public bool IsPlaying => CurrentState == GameState.Playing;
}
