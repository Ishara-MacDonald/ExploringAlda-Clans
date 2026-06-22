
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    public static GameLogicManager logicManager;
    CraftingManager craftingManager;
    InventoryManager inventoryManager;
    void Awake()
    {
        logicManager = this;
        inventoryManager = new();
        craftingManager = new(inventoryManager);
    }

    public void OnProcessItem(List<OCraftingMaterial> materials, CraftingMethod method)
    {
        craftingManager.ProcessItem(materials, method);
    }

    public ItemDataSO GetItemData(ItemDataSO item)
    {
        return inventoryManager.GetItem(item);
    }

    public void RemoveItem(ItemDataSO item)
    {
        inventoryManager.RemoveItem(item);
    }
}
