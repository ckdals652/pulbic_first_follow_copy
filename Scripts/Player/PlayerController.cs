using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private static readonly int _isfestWalk = Animator.StringToHash("IsFestWalk");
    private static readonly int _isWalk = Animator.StringToHash("IsWalk");
    private static readonly int _rightAndLeft = Animator.StringToHash("RightAndLeft");
    private static readonly int _leftArmForword = Animator.StringToHash("LeftArmForword");
    private static readonly int _rightArmForword = Animator.StringToHash("RightArmForword");
    private static readonly int _isBack = Animator.StringToHash("IsBack");

    public float speed;
    public float strafeSpeed;
    public float jumpForce;
    private float subJumpForce;

    bool isShiftPressed = false;

    public Rigidbody bodyRigidBody;
    public bool isGrounded;
    public Animator animator;

    private Vector2 moveInput;

    public Grab leftGrab, rightGrab;

    // 입력 허용 여부
    private bool inputEnabled = true;

    void Start()
    {
        bodyRigidBody = GetComponent<Rigidbody>();

        // 현재 씬이 StartScene이라면 입력 비활성화
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            inputEnabled = false;
        }

        subJumpForce = jumpForce;
    }

    private void FixedUpdate()
    {
        Move();
        rightGrab.ReleaseGrab();
        leftGrab.ReleaseGrab();
        //Debug.Log(rightGrab.isGrabbed);
        //Debug.Log(leftGrab.isGrabbed);

        //leftGrab.GrabObject();
        //rightGrab.GrabObject();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return;

        if (context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();

            // 걷는 중인지?
            bool isWalking = moveInput != Vector2.zero;
            animator.SetBool(_isWalk, isWalking);
            animator.SetBool(_isfestWalk, isWalking && isShiftPressed);

            // 뒤로 걷는 경우 (S)
            if (moveInput.y < 0)
            {
                animator.SetBool(_isWalk, true);
                animator.SetBool(_isfestWalk, false);
                animator.SetBool(_isBack, true);
            }

            // 왼쪽/오른쪽 판단
            if (moveInput.x < -0.1f)
            {
                animator.SetInteger(_rightAndLeft, 2); // 왼쪽 A
            }
            else if (moveInput.x > 0.1f)
            {
                animator.SetInteger(_rightAndLeft, 1); // 오른쪽 D
            }
            else
            {
                animator.SetInteger(_rightAndLeft, 0); // 중립
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
            animator.SetBool(_isWalk, false);
            animator.SetBool(_isfestWalk, false);
            animator.SetBool(_isBack, false);
            animator.SetInteger(_rightAndLeft, 0);
        }
    }

    private void Move()
    {
        //2D입력 → 3D 벡터 변환
        // Vector3 force = new Vector3(moveInput.x, 0f, moveInput.y) * speed;
        //
        // bodyRigidBody.AddForce(force, ForceMode.Force);

        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 force = transform.TransformDirection(moveDir) * speed;
        bodyRigidBody.AddForce(force, ForceMode.Force);
    }

    public void OnShiftInput(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return;

        if (context.performed)
            isShiftPressed = true;
        else if (context.canceled)
            isShiftPressed = false;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return;

        if (isGrounded)
        {
            bodyRigidBody.AddForce(transform.up * jumpForce);
            isGrounded = false;
        }
    }

    public void OnLeftMouseInput(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return;

        if (context.phase == InputActionPhase.Performed)
        {
            animator.SetBool(_leftArmForword, true);
            leftGrab.isReadyToGrab = true;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            animator.SetBool(_leftArmForword, false);
            leftGrab.isGrabbed = false;
            leftGrab.isReadyToGrab = false;
        }
    }

    public void OnRightMouseInput(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return;

        if (context.phase == InputActionPhase.Performed)
        {
            animator.SetBool(_rightArmForword, true);
            rightGrab.isReadyToGrab = true;

            jumpForce = 6000f;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            animator.SetBool(_rightArmForword, false);
            rightGrab.isGrabbed = false;
            rightGrab.isReadyToGrab = false;

            jumpForce = subJumpForce;
        }
    }

    // 씬 전환 후 외부에서 호출하여 입력 다시 활성화
    public void EnableInput()
    {
        inputEnabled = true;
    }
}
