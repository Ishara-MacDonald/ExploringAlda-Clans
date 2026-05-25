using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    [SerializeField] private ItemDataSO item;
    [SerializeField] private int amount;

    public ItemDataSO GetItem => item;
    public int GetAmount => amount;

    public InventorySlot(ItemDataSO _item)
    {
        item = _item;
        amount = 1;
    }

    public void AddAmount(int _amount)
    {
        amount += _amount;
    }
}
