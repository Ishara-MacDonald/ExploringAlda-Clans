
using System.Collections.Generic;
using UnityEngine;

public class VisualLogicManager : MonoBehaviour
{
    public static VisualLogicManager visualManager;

    GameLogicManager logicManager;

    CraftingVisualManager craftingManager;
    InventoryVisualManager inventoryManager;
    void Awake()
    {
        visualManager = this;
        inventoryManager = new(this);
        craftingManager = new(visualManager);
        logicManager = GameLogicManager.logicManager;
    }

    public void OnProcessItem(List<OCraftingMaterial> materials, CraftingMethod method)
    {
        visualManager.OnProcessItem(materials, method);
    }

    public ItemDataSO GetItemData(ItemDataSO itemID)
    {
        return GameLogicManager.logicManager.GetItemData(itemID);
    }

    public void RemoveItemFromInventory(ItemDataSO item)
    {
        GameLogicManager.logicManager.RemoveItem(item);
    }

}