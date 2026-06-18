
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    public static InventoryManager inventoryManager;
    public InventoryManager()
    {
        if (inventoryManager != null) Debug.LogError("Can only have one Crafting Manager");
        inventoryManager = this;
    }

    public void AddItem(ItemDataSO item)
    {
        Debug.LogWarning("Method not implemented.");
    }

    public void AddItems(List<ItemDataSO> items)
    {
        Debug.LogWarning("Method not implemented.");
    }
}