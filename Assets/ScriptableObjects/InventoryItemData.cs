using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "Scriptable Objects/InventoryItemData")]
public class InventoryItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    [TextArea] public string itemDescription;
}
