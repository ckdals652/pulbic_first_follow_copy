using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    public bool[] isUnlocked;  //스테이지가 열려있는지 잠겨있는지 체크해주는거
    public Color lockedColor = new Color(0.2f, 0.2f, 0.2f);  // 진한 회색
    public Color normalColor = new Color(0.7f, 0.7f, 0.7f);  // 연한 회색
    public Color selectedColor = Color.white;
    public GameObject[] stageImages; //스테이지 오브젝트를 직접 넣어 배열을 추가해주는거
    private int currentIndex = 0;    //선택된 스테이지 번호를 저장
    public string[] sceneNames;     //씬 이름을 받는 배열
    //========================================================================================================= 클래스 변수들
    void Start()
    {
        LoadUnlockedStages();   // 저장된 클리어 정보로 해금 여부 설정
        UpdateBackGround();     // //선택한 스테이지에 알맞게 배경색과 선택되어 있는지를 초기화하라
    }
    public void LoadUnlockedStages()
    {
        isUnlocked[0] = true; // Stage 0 (Tutorial)는 항상 열려있다고 가정       
        for (int i = 1; i < isUnlocked.Length; i++)
        {
            isUnlocked[i] = PlayerPrefs.GetInt("StageCleared_" + i, 0) == 1;
        }
    }

    public void MoveRight()//오른쪽 화살표를 눌러서 움직이는거
    {
        int nextIndex = currentIndex + 1; //스테이지를 선택할때 0~4 까지 있으니까 1씩 더하여 스테이지의 할당하는 숫자로 만들어줌

        //옆에 스테이지가 열려있는지 확인하며 열려있으면 오른쪽으로 감
        if (nextIndex < stageImages.Length && isUnlocked[nextIndex])
        {
            currentIndex = nextIndex;
            UpdateBackGround();
        }
    }
//==========================================================================================================
    public void MoveLeft() //화살표 움직이면서 선택하는거
    {
        int nextIndex = currentIndex - 1; //스테이지를 선택할때 0~4 까지 있으니까 1씩 빼면서 스테이지의 할당하는 숫자로 만들어줌

        //옆에 스테이지가 열려있는지 확인하며 열려있으면 왼쪽으로 감
        if (nextIndex >= 0 && isUnlocked[nextIndex])
        {
            currentIndex = nextIndex;
            UpdateBackGround();
        }
    }
    public string GetSelectedSceneName()//씬 이름 맞는지 확인
    {
        return sceneNames[currentIndex];
    }

    
    public void UpdateBackGround() //잠긴 스테이지 배경색 지정,선택불가하게 해줌
    {
        for (int i = 0; i < stageImages.Length; i++)
        {
            Image img = stageImages[i].GetComponent<Image>();
            Button btn = stageImages[i].GetComponent<Button>();

            if (!isUnlocked[i])
            {
                img.color = lockedColor;                    //잠긴 배경
                if (btn != null) btn.interactable = false;  //클릭 불가
            }
            else
            {
                if (i == currentIndex)
                {
                    img.color = selectedColor;              //선택됨
                }
                else
                {
                    img.color = normalColor;                //열렸지만 선택 안됨
                }

                if (btn != null) btn.interactable = true;   //클릭 가능
            }
        }
    }

    public void SelectStageIndex(int index)
    {
        if (index >= 0 && index < sceneNames.Length && isUnlocked[index])
        {
            currentIndex = index;
            UpdateBackGround();
        }
    }
    public void UnlockNextStage()
    {
        int nextIndex = currentIndex + 1;

        if (nextIndex < sceneNames.Length)
        {
            isUnlocked[nextIndex] = true;
            PlayerPrefs.SetInt("StageCleared_" + nextIndex, 1);
            PlayerPrefs.Save();
            UpdateBackGround();

        }
    }

}
