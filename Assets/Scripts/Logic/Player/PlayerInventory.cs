public enum TPPoint
{
    Home,
    Hill,
    Lake
}

public class PlayerInventory : InventorySystem
{
    void Awake()
    {
        slots = new();
    }

    private void OnEnable()
    {
        OverworldItem.ItemPickUp += AddItem;
        FlowerInteract.pickedUp += AddItem;
    }

    private void OnDisable()
    {
        OverworldItem.ItemPickUp -= AddItem;
        FlowerInteract.pickedUp -= AddItem;
    }
}
