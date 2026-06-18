
using System.Collections.Generic;
using UnityEngine;

public class VisualLogicManager : MonoBehaviour
{
    private static VisualLogicManager visualManager;
    CraftingVisualManager craftingManager;
    InventoryVisualManager inventoryManager;
    void Awake()
    {
        visualManager = this;
        inventoryManager = new();
        craftingManager = new(inventoryManager);
    }

    public void OnProcessItem(List<CraftingMaterial> materials, CraftingMethod method)
    {
        visualManager.OnProcessItem(materials, method);
    }
}