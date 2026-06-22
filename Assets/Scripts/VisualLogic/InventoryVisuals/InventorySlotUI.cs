
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{

    public static event Action<ItemDataSO, int> ItemInventoryInteracted;
    private ItemDataSO item;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI amountTxt;
    private int amount;

    public ItemDataSO Item => item;
    public int Amount => amount;
    private InventoryVisualManager manager;

    public void SetInventorySlotUI(ItemDataSO _item, int _amount, InventoryVisualManager _manager)
    {
        manager = _manager;
        item = _item;
        image.sprite = item.itemSprite;
        amount = _amount;
        amountTxt.SetText(_amount.ToString());
    }

    public void SetAmount(int _amount)
    {
        amount = _amount;
        amountTxt.SetText(_amount.ToString());
    }

    public void OnClickItem()
    {
        manager.DisplayInfo(item);
    }
}