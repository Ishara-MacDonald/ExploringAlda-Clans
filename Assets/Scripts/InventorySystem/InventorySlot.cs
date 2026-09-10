using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    [SerializeField] private ItemDataSO item;
    [SerializeField] private int amount;

    public ItemDataSO GetItem => item;
    public int GetAmount => amount;

    public InventorySlot(ItemDataSO _item, int _amount = 1)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int _amount)
    {
        amount += _amount;
    }

    public int RemoveAmount(int _amount)
    {
        amount -= _amount;
        if (amount < 0) amount = 0;
        return amount;
    }
}
