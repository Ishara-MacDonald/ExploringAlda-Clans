
using UnityEngine;

public class CraftingVisualManager : MonoBehaviour
{
    InventoryVisualManager inventoryManager;

    public CraftingVisualManager(InventoryVisualManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;
    }
}