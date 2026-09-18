using UnityEngine;

// Bridges InventorySystem.AddedItem to LogicManager, so LogicManager never subscribes directly.
public class InventorySystemManager : SingletonManager<InventorySystemManager>
{
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
