using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static event Action<ItemDataSO> AddedItem;
    [SerializeField] protected List<InventorySlot> slots;

    public List<InventorySlot> InventorySlots => slots;

    public void SetInventorySlot(List<InventorySlot> _slots)
    {
        if (slots.Count < 1)
            slots = _slots;
    }

    public bool HasItem(ItemDataSO item)
    {
        return slots.Find((slot) => slot.GetItem.Equals(item)) is not null;
    }

    public void AddItem(ItemDataSO item, int amount = 1)
    {
        InventorySlot foundSlot = slots.Find(slot => slot.GetItem == item);
        if (foundSlot == null)
            slots.Add(new InventorySlot(item, amount));
        else
            foundSlot.AddAmount(amount);

        AddedItem?.Invoke(item);
    }

    public void RemoveItem(ItemDataSO item, int amount = 1)
    {
        InventorySlot foundSlot = slots.Find(slot => slot.GetItem == item);
        if (foundSlot == null) return;

        if (foundSlot.GetAmount <= amount) slots.Remove(foundSlot);
        else foundSlot.RemoveAmount(amount);
    }
}
