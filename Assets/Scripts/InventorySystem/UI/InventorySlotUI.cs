
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InventorySlotUI : MonoBehaviour
{
    private InventoryItemData item;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI amountTxt;
    public void SetInventorySlotUI(InventoryItemData _item, int _amount)
    {
        item = _item;
        image.sprite = item.itemImage;
        amountTxt.SetText(_amount.ToString());
    }

    public void ShowItemDetails()
    {
        GameManager.manager.ShowItemDetails(item);
    }
}
