using System;
using System.Collections.Generic;

public enum CraftingMethod
{
    Picking,
    Grinding
}

[Serializable]
public class CraftingSystem
{
    public static CraftingSystem craftingSystem;

    private CraftingTable currentTable;
    private readonly List<ItemDataSO> craftingItems;
    private CraftingMethod currentMethod;

    public CraftingSystem()
    {
        if (craftingSystem != null) return;
        craftingSystem = this;
        craftingItems = new();

        CraftingTable.ContextOpened += OnCraftingTableOpen;
        CraftingSystemUI.CloseCraftingSystem += OnCraftingTableClose;
    }

    private void OnCraftingTableOpen(CraftingTable craftingTable)
    {
        InventorySlotUI.ItemInventoryInteracted += AddItem;
        currentTable = craftingTable;
    }

    private void OnCraftingTableClose()
    {
        InventorySlotUI.ItemInventoryInteracted -= AddItem;
        currentTable.OnClose();
    }

    public void AddItem(ItemDataSO newItem, int hasAmount)
    {
        if (craftingItems.Count > 0)
        {
            int currentAmount = craftingItems.FindAll((item) => item.Equals(newItem)).Count;
            if (hasAmount > currentAmount)
            {
                craftingItems.Add(newItem);
                currentTable.AddMaterial(newItem);
            }
        }
        else if (craftingItems.Count == 0)
        {
            craftingItems.Add(newItem);
            currentTable.AddMaterial(newItem);
        }
    }
}
