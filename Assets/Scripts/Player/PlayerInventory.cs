using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : InventorySystem
{

    private InputAction inventoryAction;
    private bool isInventoryOpen;

    void Awake()
    {
        inventoryAction = InputSystem.actions.FindAction("Inventory");
    }

    void Start()
    {
        isInventoryOpen = false;
    }

    public void ToggleInventoryOpen()
    {
        isInventoryOpen = !isInventoryOpen;
    }

    private void OnEnable()
    {
        inventoryAction.Enable();
        inventoryAction.started += OnInventoryOpen;
    }

    private void OnDisable()
    {
        inventoryAction.started -= OnInventoryOpen;
        inventoryAction.Disable();
    }

    private void OnInventoryOpen(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isInventoryOpen) GameManager.manager.CloseInventory();
            else GameManager.manager.OpenInventory(this);

            isInventoryOpen = !isInventoryOpen;
        }
    }

}
