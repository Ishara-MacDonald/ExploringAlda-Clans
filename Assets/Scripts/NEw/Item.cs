using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;

    public GameObject worldObject;
    public Sprite itemSprite;
    [TextArea] public string itemDescription;
}