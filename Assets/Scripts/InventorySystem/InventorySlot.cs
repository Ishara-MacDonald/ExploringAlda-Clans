using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData item;
    [SerializeField] private int amount;

    public InventoryItemData GetItem => item;
    public int GetAmount => amount;

    public InventorySlot(InventoryItemData _item)
    {
        item = _item;
        amount = 1;
    }

    public void AddAmount(int _amount)
    {
        amount += _amount;
    }
}
