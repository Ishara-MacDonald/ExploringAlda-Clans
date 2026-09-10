
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    #region InputActions
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction jumpAction;
    private Vector2 moveInput;
    #endregion

    [Header("References")]
    [SerializeField] private GameObject Wings;
    [SerializeField] private LayerMask groundLayer;
    private CharacterController characterController;
    private Vector3 appliedMovement;
    private Vector3 cameraRelativeMovement;

    private bool isMovementEnabled;
    private bool isSprintEnabled;
    private bool isGliding;

    #region Speeds
    [Header("Speeds")]
    [SerializeField] private int walkSpeed;
    [SerializeField] private int sprintSpeed;
    [SerializeField] private int fallingSpeed;
    [SerializeField] private int glidingSpeed;
    [SerializeField] private float rotationPerFrame;

    [Header("Jumping & Gravity")]
    [SerializeField] private float groundedBuffer;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpGravity;
    [SerializeField] private float jumpPower;
    [SerializeField] private float glidingVelocity;
    private float velocity;
    private int moveSpeed;
    #endregion

    public bool IsMovementEnabled => isMovementEnabled;
    private bool IsGrounded => characterController.isGrounded || Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, groundedBuffer, groundLayer);
    private bool IsMoving => moveAction.ReadValue<Vector2>().x != 0 || moveAction.ReadValue<Vector2>().y != 0;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        isMovementEnabled = true;
        isSprintEnabled = false;
        isGliding = false;
        moveSpeed = walkSpeed;
    }

    public void ToggleMovementEnabled()
    {
        isMovementEnabled = !isMovementEnabled;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        sprintAction.Enable();
        jumpAction.Enable();

        sprintAction.started += OnSprintToggle;
        sprintAction.canceled += OnSprintToggle;

        jumpAction.started += OnJump;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        sprintAction.Disable();
        jumpAction.Disable();

        sprintAction.started -= OnSprintToggle;
        sprintAction.canceled -= OnSprintToggle;

        jumpAction.started -= OnJump;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, -transform.up * groundedBuffer, Color.red);
        if (isMovementEnabled)
        {
            moveSpeed = HandleMovementSpeed();
            HandleCharacterRotation();

            moveInput = HandleMovementInput();
            appliedMovement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
            HandleGravity();
            cameraRelativeMovement = ConvertToCameraSpace(appliedMovement);
            characterController.Move(Time.deltaTime * cameraRelativeMovement);
        }
    }

    public void SetMovementEnabled(bool newValue)
    {
        isMovementEnabled = newValue;
    }

    private void OnSprintToggle(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSprintEnabled = true;
        }
        else if (context.canceled)
        {
            isSprintEnabled = false;
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Jump();
        }
    }

    private void OnToggleGliding()
    {
        isGliding = !isGliding;
    }

    private void HandleCharacterRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = cameraRelativeMovement.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = cameraRelativeMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);

            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationPerFrame * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, groundedBuffer, groundLayer))
        {
            velocity += jumpPower;
        }
        else
        {
            OnToggleGliding();
        }
    }

    private int HandleMovementSpeed()
    {
        if (isGliding) return glidingSpeed;
        if (!IsGrounded) return fallingSpeed;
        if (isSprintEnabled) return sprintSpeed;
        return walkSpeed;
    }

    private Vector2 HandleMovementInput()
    {
        if (!IsGrounded)
        {
            if (!IsMoving) return moveInput;
        }
        return moveAction.ReadValue<Vector2>();
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded && velocity < 0.0f)
        {
            if (isGliding) isGliding = false;
            velocity = -1.0f;
        }
        else
        {
            if (isGliding) velocity = glidingVelocity;
            else velocity += (velocity > 0.0f ? jumpGravity : gravity) * Time.deltaTime;
        }
        Wings.SetActive(isGliding);

        appliedMovement.y = velocity;
    }

    private Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        float currentYValue = vectorToRotate.y;
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;
        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        vectorRotatedToCameraSpace.y = currentYValue;
        return vectorRotatedToCameraSpace;
    }
}
