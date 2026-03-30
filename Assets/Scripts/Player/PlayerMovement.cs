
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private InputAction moveAction;
    private Vector2 moveInput;
    private CharacterController characterController;
    private Vector3 appliedMovement;
    private Vector3 cameraRelativeMovement;

    private bool isMovementEnabled;

    [SerializeField] private float rotationPerFrame;
    [SerializeField] private int moveSpeed;
    [SerializeField] private float gravity;
    private float velocity;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        isMovementEnabled = true;
    }

    public void ToggleMovementEnabled()
    {
        isMovementEnabled = !isMovementEnabled;
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void Update()
    {
        if (isMovementEnabled)
        {
            HandleCharacterRotation();

            moveInput = moveAction.ReadValue<Vector2>();
            appliedMovement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
            HandleGravity();
            cameraRelativeMovement = ConvertToCameraSpace(appliedMovement);
            characterController.Move(Time.deltaTime * cameraRelativeMovement);
        }
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

    private void HandleGravity()
    {
        if (characterController.isGrounded && velocity < 0.0f)
            velocity = -1.0f;
        else
            velocity += gravity * Time.deltaTime;

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
