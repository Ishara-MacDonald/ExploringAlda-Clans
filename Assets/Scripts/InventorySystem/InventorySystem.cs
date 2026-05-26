using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static event Action<ItemDataSO> PickedUpItem;
    [SerializeField] protected List<InventorySlot> slots;

    public List<InventorySlot> InventorySlots => slots;

    public bool HasItem(ItemDataSO item)
    {
        return slots.Find((slot) => slot.GetItem.Equals(item)) is not null;
    }

    public void AddItem(ItemDataSO item)
    {
        InventorySlot foundSlot = slots.Find(slot => slot.GetItem == item);
        if (foundSlot == null)
            slots.Add(new InventorySlot(item));
        else
            foundSlot.AddAmount(1);

        PickedUpItem?.Invoke(item);
    }
}
