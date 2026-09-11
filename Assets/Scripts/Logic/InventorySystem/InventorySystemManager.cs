using UnityEngine;

// Bridges InventorySystem.AddedItem to LogicManager, so LogicManager never subscribes directly.
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
