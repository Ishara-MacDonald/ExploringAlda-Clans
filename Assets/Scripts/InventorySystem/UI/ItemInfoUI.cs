using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    public void SetItemInfoUI(InventoryItemData item)
    {
        image.sprite = item.itemImage;
        nameTxt.SetText(item.itemName);
        descriptionTxt.SetText(item.itemDescription);
    }
}
