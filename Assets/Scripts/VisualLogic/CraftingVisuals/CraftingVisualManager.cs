
using System;
using UnityEngine;

public class CraftingVisualManager : MonoBehaviour
{
    public VisualLogicManager visualManager;
    CraftingTableManager tableManager;

    public CraftingVisualManager(VisualLogicManager _visualManager)
    {
        visualManager = _visualManager;
    }

    public void RemoveItemFromInventory(ItemDataSO item)
    {
        visualManager.RemoveItemFromInventory(item);
    }
}