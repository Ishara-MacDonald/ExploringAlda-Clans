
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InventorySlotUI : MonoBehaviour
{
    public static event Action<ItemDataSO, int> ItemInventoryInteracted;
    private ItemDataSO item;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI amountTxt;
    private int amount;

    public void SetInventorySlotUI(ItemDataSO _item, int _amount)
    {
        item = _item;
        image.sprite = item.itemSprite;
        amount = _amount;
        amountTxt.SetText(_amount.ToString());
    }

    public void OnClickItem()
    {
        ItemInventoryInteracted?.Invoke(item, amount);
    }
}
