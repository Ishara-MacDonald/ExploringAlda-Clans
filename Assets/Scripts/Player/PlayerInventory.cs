using UnityEngine.InputSystem;

public enum TPPoint
{
    Home,
    Hill,
    Lake
}

public class PlayerInventory : InventorySystem
{
    private InputAction inventoryAction;

    void Awake()
    {
        inventoryAction = InputSystem.actions.FindAction("Inventory");
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
            if (GameManager.inventoryUIOpen) GameManager.manager.CloseInventory();
            else GameManager.manager.OpenInventory(this);
        }
    }

}
