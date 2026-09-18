using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    public void SetItemInfoUI(ItemDataSO item)
    {
        image.sprite = item.itemSprite;
        nameTxt.SetText(item.itemName);
        descriptionTxt.SetText(item.itemDescription);
    }
}
