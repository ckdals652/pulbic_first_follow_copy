using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOrbit : MonoBehaviour
{
    [Header("카메라 설정")] public Transform target; // 카메라가 바라볼 대상 (플레이어 머리 위의 빈 오브젝트)
    public Vector3 offset = new Vector3(0, -7.8f, -10.5f); // 카메라가 타겟으로부터 떨어질 기본 위치
    public float sensitivity = 0.2f; // 마우스 회전 감도
    public float minY = -30f, maxY = 60f; // 상하 회전 제한 각도
    public float collisionOffset = 0.2f; // 벽에 닿았을 때 카메라가 앞으로 당겨질 거리
    public LayerMask collisionMask; // 카메라가 충돌을 감지할 레이어 설정

    private float yaw = 0f; // 마우스 좌우 이동 누적값 (수평 회전)
    private float pitch = 20f; // 마우스 상하 이동 누적값 (수직 회전)

    //임창민
    private Vector2 mouseValue;

    public float rotationSpeed = 0.1f;
    public Transform root;
    public float stomachOffset;
    public ConfigurableJoint hipJoint, stomachJoint;

    private float mouseX,mouseY;
    public float mouseMinY=-25f, mouseMaxY=25f;


    private void Start()
    {
        // 게임 시작 시 마우스 커서를 중앙에 고정하고 보이지 않게 설정
        Cursor.lockState = CursorLockMode.Locked; // 커서를 화면 중앙에 고정
        Cursor.visible = false; // 커서를 숨김
    }

    private void LateUpdate()
    {
        if (Time.timeScale == 0f) return; //게임이 일시정지 상태라면? 카메라 업뎃 중지

        MouseAim();
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0f) return; //게임이 일시정지 상태라면? 카메라 업뎃 중지

        BodyRotation();
    }

    //나중에 객체 바꾸면서 한번 수정 해보자(임창민)
    private void BodyRotation()
    {
        Quaternion rootRotation = Quaternion.Euler(mouseY, mouseX, 0f);
        root.rotation = rootRotation;

        hipJoint.targetRotation = Quaternion.Euler(0f, -mouseX, 0f);
        stomachJoint.targetRotation = Quaternion.Euler(-mouseY + stomachOffset, 0f, 0f);
    }

    //임창민
    public void OnMouseValue(InputAction.CallbackContext context)
    {
        mouseValue = context.ReadValue<Vector2>();

        // 마우스 입력을 기반으로 회전 각도 누적 (위치 이동 임창민)
        yaw += mouseValue.x * sensitivity; // 마우스 좌우 이동 → 수평 회전 (Yaw) // 임창민 수정 (mouseValue.x,)
        pitch -= mouseValue.y * sensitivity; // 마우스 상하 이동 → 수직 회전 (Pitch) // 임창민 수정 (mouseValue.y)
        pitch = Mathf.Clamp(pitch, minY, maxY); // Pitch 값은 위아래 제한 범위 내에서만 적용

        //임창민
        mouseX += mouseValue.x * rotationSpeed;
        mouseY -= mouseValue.y * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, mouseMinY, mouseMaxY);
    }

    //임창민 수정(그냥 함수로 뺌~ 나머지 위치 이동 수정(OnMouseValue로))
    private void MouseAim()
    {
        // 회전 각도를 바탕으로 새로운 카메라 위치 계산
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0); // 회전값 생성
        Vector3 desiredCameraPos = target.position + rotation * offset; // 회전된 offset 위치를 적용한 목표 카메라 위치

        // 충돌 검사: 카메라가 벽이나 지형에 닿는 경우 거리 줄이기
        Vector3 direction = desiredCameraPos - target.position; // 타겟 → 카메라 방향 벡터
        float distance = offset.magnitude; // 기본 거리 값

        if (Physics.Raycast(target.position, direction.normalized, out RaycastHit hit, distance, collisionMask))
        {
            // 충돌한 경우, 벽에 붙지 않도록 약간 앞쪽으로 당긴 위치를 카메라 위치로 설정
            desiredCameraPos = hit.point - direction.normalized * collisionOffset;
        }

        // 계산된 위치로 카메라 이동
        transform.position = desiredCameraPos;

        // 타겟을 바라보도록 회전
        transform.LookAt(target.position);
    }
}
