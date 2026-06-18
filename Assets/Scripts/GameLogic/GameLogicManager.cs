
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    private static GameLogicManager logicManager;
    CraftingManager craftingManager;
    InventoryManager inventoryManager;
    void Awake()
    {
        logicManager = this;
        inventoryManager = new();
        craftingManager = new(inventoryManager);
    }

    public void OnProcessItem(List<CraftingMaterial> materials, CraftingMethod method)
    {
        craftingManager.ProcessItem(materials, method);
    }

}