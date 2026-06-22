using UnityEngine;

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

    public int RemoveAmount(int _amount)
    {
        amount -= _amount;
        if (amount < 0) amount = 0;
        return amount;
    }
}
