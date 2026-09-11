using UnityEngine;

// Logic-side per-system manager for Inventory. Currently just bridges the
// InventorySystem.AddedItem static event (fires for any InventorySystem instance
// adding an item) up through LogicManager, so LogicManager never subscribes to a
// Logic class's event directly.
public class InventorySystemManager : MonoBehaviour
{
    public static InventorySystemManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        InventorySystem.AddedItem += OnItemAdded;
    }

    void OnDisable()
    {
        InventorySystem.AddedItem -= OnItemAdded;
    }

    private void OnItemAdded(ItemDataSO item) => LogicManager.manager.OnItemAdded(item);
}
