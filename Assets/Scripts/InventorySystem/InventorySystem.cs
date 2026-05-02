using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] List<InventorySlot> slots;

    void Awake()
    {
        slots = new();
    }

    public List<InventorySlot> GetSlots()
    {
        return slots;
    }

    public void AddItem(InventoryItemData item)
    {
        InventorySlot foundSlot = slots.Find(slot => slot.GetItem.name == item.name);
        if (foundSlot == null)
            slots.Add(new InventorySlot(item));
        else
            foundSlot.AddAmount(1);
    }

    public void RemoveAmount(InventoryItemData item, int amount)
    {

    }

    public void RemoveItem(InventoryItemData item)
    {

    }
}
