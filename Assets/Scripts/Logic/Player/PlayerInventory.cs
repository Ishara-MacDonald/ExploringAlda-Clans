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
        slots = new();
        inventoryAction = InputSystem.actions.FindAction("Inventory");
    }

    private void OnEnable()
    {
        inventoryAction.Enable();

        inventoryAction.started += OnInventoryOpen;
        OverworldItem.ItemPickUp += AddItem;
        FlowerInteract.pickedUp += AddItem;
    }

    private void OnDisable()
    {
        inventoryAction.started -= OnInventoryOpen;
        inventoryAction.Disable();

        OverworldItem.ItemPickUp -= AddItem;
        FlowerInteract.pickedUp -= AddItem;
    }

    private void OnInventoryOpen(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (VisualManager.inventoryUIOpen) VisualManager.manager.CloseInventory();
            else VisualManager.manager.OpenInventory(this);
        }
    }

}
