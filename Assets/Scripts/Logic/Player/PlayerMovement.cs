using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 rawMoveInput;
    private Vector2 moveInput;

    [Header("References")]
    [SerializeField] private GameObject Wings;
    [SerializeField] private LayerMask groundLayer;
    private CharacterController characterController;
    private Vector3 appliedMovement;
    private Vector3 cameraRelativeMovement;

    private bool isMovementEnabled;
    private bool isSprintEnabled;
    private bool isGliding;

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

    public bool IsMovementEnabled => isMovementEnabled;
    private bool IsGrounded => characterController.isGrounded || Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, groundedBuffer, groundLayer);
    private bool IsMoving => rawMoveInput.x != 0 || rawMoveInput.y != 0;

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

    public void SetMoveInput(Vector2 newValue)
    {
        rawMoveInput = newValue;
    }

    public void SetSprintEnabled(bool newValue)
    {
        isSprintEnabled = newValue;
    }

    public void TryJump()
    {
        Jump();
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
        return rawMoveInput;
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
