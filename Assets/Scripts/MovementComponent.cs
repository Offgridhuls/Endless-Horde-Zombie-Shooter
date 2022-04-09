using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    // Start is called before the first frame update

    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");
    public readonly int AimVerticalHash = Animator.StringToHash("AimVertical");


    Animator playerAnimator;

    [SerializeField]
    float walkSpeed = 5;

    [SerializeField]
    float runSpeed = 10;

    [SerializeField]
    float jumpForce = 5;

    [SerializeField]
    float minXLook = 25;

    [SerializeField] 
    float maxXLook = 400;

    float aimAngle = 0;

    public Transform[] spine;

    public GameObject followTarget;

    private PlayerController playerController;

    private Transform playerTransform;

    Vector2 inputVector = Vector2.zero;

    Vector3 moveDirection = Vector3.zero;

    Vector2 lookInput = Vector2.zero;

    public float aimSensitivity = 0.2f;
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerTransform = transform;
        playerController = GetComponent<PlayerController>();

        if (!GameManager.instance.cursorActive)
        {
            AppEvents.InvokeMouseCursorEnable(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (playerController.isJumping) return;
        if(!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);
        transform.position += movementDirection;

    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        playerAnimator.SetFloat(movementXHash, inputVector.x);
        playerAnimator.SetFloat(movementYHash, inputVector.y);

    }



    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        playerAnimator.SetBool(isRunningHash, playerController.isRunning);
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
        
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.x * aimSensitivity, Vector3.up);

        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.y * aimSensitivity, Vector3.right);

        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;
        var angle = followTarget.transform.localEulerAngles.x;


        float min = -60;
        float max = 70.0f;
        float range = max - min;
        float offsetToZero = 0 - min;
        float aimAngle = followTarget.transform.localEulerAngles.x;
        aimAngle = (aimAngle > 180) ? aimAngle - 360 : aimAngle;
        float val = (aimAngle + offsetToZero) / (range);

        //foreach (Transform t in spine)
        //{
        //    t.rotation = Quaternion.LookRotation(new Vector3(0, followTarget.transform.rotation.eulerAngles.y, 0), Vector3.up);
        //}
        //
        playerAnimator.SetFloat(AimVerticalHash, val);

        if (angle > 200 && angle < maxXLook)
        {
            angles.x = maxXLook;
        }
        else if (angle < 180 && angle > minXLook)
        {
            angles.x = minXLook;
        }

        followTarget.transform.localEulerAngles = angles;

        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);

        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    public void OnAim(InputValue value)
    {
        playerController.isAiming = value.isPressed;
    }

}
