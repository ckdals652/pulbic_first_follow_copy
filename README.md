# 🧩 3DProject_F1rst8ollowTeam

> **물리 기반 퍼즐을 중심으로 한 3D 캐주얼 게임**  
> 사용자에게 창의적인 해결 방식과 웃음을 제공하는 3인칭 래그돌 퍼즐 게임!

![Intro1](https://github.com/user-attachments/assets/9d30f8c8-d05f-41dd-98db-7d81235bdcfa)

![Intro2](https://github.com/user-attachments/assets/bb647f7f-9304-4583-8aff-10093d470281)



---

## 🗂️ 참고 링크

- [협업 Notion 링크](https://www.notion.so/1fe21c6668158055a8e7df6774d6bccc?source=copy_link))
- [Figma 링크](https://www.figma.com/board/wOxziCzrpez1OjSDdKQ2Zu/3D-Project-1%EB%8B%A8-8%EB%A1%9C%EC%9A%B0?node-id=0-1&t=l8JpmN8vmAix6ty7-1))
- [발표 PPT 링크](https://www.miricanvas.com/v/14p92jf)

- [사운드 출처](https://pixabay.com/ko/music/))
- [무료 에셋 출처](https://assetstore.unity.com/mega-bundles/must-have-new?utm_source=google&utm_medium=cpc&utm_campaign=as_as_as_apac_kr_ko_pu_sem-pmax_acq_rt_2025-05_assetstore-megabundle_cc3022_x&utm_content=ug-lp_static-single_megabundle_1200x1200_it-inteterst_a_all&utm_term=&gad_source=1&gad_campaignid=22505963504&gbraid=0AAAAADdkVOtyj0kIP36GRjbXsoMpa1gvB&gclid=Cj0KCQjw9O_BBhCUARIsAHQMjS5O90-W8_V_bcEwYINU4_LRVvdgPWP_ITGVYj-9mMdiZPzPDfKL7RMaAoCUEALw_wcB&gclsrc=aw.ds))

## 🎮 프로젝트 개요

- **프로젝트 명**: 3DProject_F1rst8ollowTeam  
- **장르**: 퍼즐 / 캐주얼 / 물리 기반 어드벤처  
- **플랫폼**: PC (Unity 2022.3 LTS)  
- **팀원 구성**: 6인 팀 (기획, 개발, UI/UX, 연출, QA 등 각 역할 분담)

---

## 🧭 개발 목표

- 플레이어가 직접 조작하면서 **물리 퍼즐을 해결하는 재미** 제공
- **혼자서도, 협동으로도** 플레이 가능한 구조 설계 (아직 협동 미구현)
- **캐릭터 조작의 익살스러움과 협동의 변수**를 활용한 창의적 게임플레이 유도
- **스토리/세계관** 기반의 몰입감 제공

---

## 🛠️ 주요 시스템

| 시스템 | 설명 |
|--------|------|
| **래그돌 기반 조작** | 캐릭터가 물리적으로 반응하며 움직이는 형태로 설계 |
| **퍼즐 상호작용** | 상자를 밀기, 손잡이 돌리기, 버튼 누르기 등 환경 상호작용 |
| **카메라 시점 전환** | 3인칭/탑뷰 등 상황에 맞게 조절 가능한 시점 |
| **UI/UX 시스템** | 명확한 피드백과 HUD 제공, 스테이지 선택/클리어 UI 포함 |
| **세이브포인트/리스폰 시스템** | 플레이어 좌표 저장 및 특정 조건에서 위치 복원 |
| **애니메이션 처리** | 사용자 행동에 따른 부드러운 모션 및 리액션 제공 |
| **사운드/연출** | 효과음과 배경음악, 카메라 연출 등을 통해 몰입도 강화 |

---

## 🎨 핵심 기능

- ✅ **움직임, 점프, 팔 뻗기** 등 기본 캐릭터 컨트롤  
- ✅ **오브젝트 붙잡기 & 이동하기** (Grab 시스템)  
- ✅ **CheckPoint 기반 위치 저장 및 복원 (Rewind)**  
- ✅ **클리어 시 연출 및 애니메이션**  
- ✅ **씬 전환 및 로딩 관리**  
- ✅ **Input System 기반 입력 처리**  
- ✅ **GitHub 협업 기반 브랜치 전략 & Issue 관리**

---

## 🧑‍💻 팀 역할 분담

| 이름 (GitHub) | 역할 |
|---------------|------|
| Miyn97 | 팀장, 기획 메인, 연출 서브, UI/UX 서브, QA, GitHub 관리, Camera 시점 |
| ckdals652 | Player 전체 |
| guni0527 | UI/UX 메인, 배경음 및 효과음, 모델링 일부 |
| Won0001 | 오브젝트, 장애물 구현 |
| cheolwoo123 | 연출 메인, StageManager, 씬 전환 및 카메라 |

---

## 📁 폴더 구조 (일부)

📁 Assets

├─ 📁 Scripts

│ ├─ Player

│ ├─ Camera

│ ├─ UI

│ ├─ System

│ ├─ Area

│ └─ Managers

├─ 📁 Prefabs

├─ 📁 Materials

├─ 📁 Scenes

├─ 📁 Audio

└─ 📁 Animations



---

## 🧪 실행 방법

1. Unity Hub에서 Unity 2022.3.24f1 버전으로 프로젝트 열기
2. `StartScene` → `TutorialScene` → `MainScene` 순서로 테스트
3. Input System 사용 중 (PlayerInput 컴포넌트가 붙은 캐릭터 사용)

---

## 📌 개발 중 이슈 관리

- GitHub Issue 및 Project 기능을 활용하여 협업
- 커밋 메시지 규칙: `feat: 기능 추가`, `fix: 버그 수정`, `refactor: 리팩토링` 등
- `.gitignore` 및 `.gitattributes` 구성으로 충돌 최소화

---

## 🗂️ 관련 문서

- [Unity Input System 공식 문서](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/)
- [GitHub 협업 가이드](https://docs.github.com/ko/get-started/quickstart)
- [Unity Ragdoll 세팅](https://docs.unity3d.com/Manual/Ragdoll.html)

---

## 📮 기타

- 게임 이름 미정. 아이디어 공유와 투표를 통해 최종 확정 예정!
- 테스트 플레이 후 피드백 수렴 중입니다. 🔧

---

