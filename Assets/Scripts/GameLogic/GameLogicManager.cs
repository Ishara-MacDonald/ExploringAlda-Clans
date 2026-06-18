
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    CraftingManager craftingManager;
    InventoryManager inventoryManager;
    void Awake()
    {
        craftingManager = new();
        inventoryManager = new();
    }

}