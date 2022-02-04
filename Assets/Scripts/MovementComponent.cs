using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    // Start is called before the first frame update

    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");

    Animator playerAnimator;

    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;
    [SerializeField]
    float jumpForce = 5;

    private PlayerController playerController;

    private Transform playerTransform;

    Vector2 inputVector = Vector2.zero;

    Vector3 moveDirection = Vector3.zero;

    Vector2 lookInput = Vector2.zero;

    public float aimSensitivity = 1;
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerTransform = transform;
        playerController = GetComponent<PlayerController>();
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

    }

    public void OnAim(InputValue value)
    {

    }

}
