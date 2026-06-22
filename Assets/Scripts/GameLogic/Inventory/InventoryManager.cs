
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    protected List<InventorySlot> slots;
    public static InventoryManager inventoryManager;

    public List<InventorySlot> InventorySlots => slots;

    public InventoryManager()
    {
        if (inventoryManager != null) Debug.LogError("Can only have one Crafting Manager");
        inventoryManager = this;
    }

    public ItemDataSO GetItem(ItemDataSO itemID)
    {
        return null;
    }

    public bool HasItem(ItemDataSO item)
    {
        return slots.Find((slot) => slot.GetItem.Equals(item)) is not null;
    }

    public void AddItem(ItemDataSO item, int amount = 1)
    {
        InventorySlot foundSlot = slots.Find(slot => slot.GetItem == item);
        if (foundSlot == null)
            slots.Add(new InventorySlot(item));
        else
            foundSlot.AddAmount(amount);
    }

    public List<InventorySlot> AddItems(List<ItemDataSO> items)
    {
        foreach (ItemDataSO item in items)
        {
            AddItem(item, 1);
        }
        return slots;
    }

    public void RemoveItem(ItemDataSO item, int amount = 1)
    {
        InventorySlot foundSlot = slots.Find(slot => slot.GetItem == item);
        if (foundSlot == null) return;

        slots.Remove(foundSlot);
    }

    public List<InventorySlot> RemoveItems(List<ItemDataSO> items)
    {
        foreach (ItemDataSO item in items)
        {
            RemoveItem(item, 1);
        }

        return slots;
    }
}