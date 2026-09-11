using UnityEngine;
using UnityEngine.InputSystem;

// Reads all player input and pushes it as intents through VisualManager to LogicManager.
public class PlayerVisualManager : MonoBehaviour
{
    public static PlayerVisualManager Instance;

    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction jumpAction;
    private InputAction inventoryAction;
    private InputAction questListAction;

    void Awake()
    {
        Instance = this;
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("Jump");
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        questListAction = InputSystem.actions.FindAction("Quests");
    }

    private void OnEnable()
    {
        moveAction.Enable();
        sprintAction.Enable();
        jumpAction.Enable();
        inventoryAction.Enable();
        questListAction.Enable();

        sprintAction.started += OnSprintToggle;
        sprintAction.canceled += OnSprintToggle;
        jumpAction.started += OnJump;
        inventoryAction.started += OnInventoryToggle;
        questListAction.performed += OnQuestListToggle;
    }

    private void OnDisable()
    {
        sprintAction.started -= OnSprintToggle;
        sprintAction.canceled -= OnSprintToggle;
        jumpAction.started -= OnJump;
        inventoryAction.started -= OnInventoryToggle;
        questListAction.performed -= OnQuestListToggle;

        moveAction.Disable();
        sprintAction.Disable();
        jumpAction.Disable();
        inventoryAction.Disable();
        questListAction.Disable();
    }

    private void Update()
    {
        VisualManager.manager.SetPlayerMoveInput(moveAction.ReadValue<Vector2>());
    }

    private void OnSprintToggle(InputAction.CallbackContext context)
    {
        if (context.started) VisualManager.manager.SetPlayerSprintEnabled(true);
        else if (context.canceled) VisualManager.manager.SetPlayerSprintEnabled(false);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) VisualManager.manager.TryPlayerJump();
    }

    private void OnInventoryToggle(InputAction.CallbackContext context)
    {
        if (context.started) VisualManager.manager.OnInventoryToggle();
    }

    private void OnQuestListToggle(InputAction.CallbackContext context)
    {
        if (context.performed) VisualManager.manager.OnQuestListToggle();
    }
}
