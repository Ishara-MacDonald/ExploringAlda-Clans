using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CraftingMaterial : MonoBehaviour
{
    private ItemDataSO item;

    public string ItemName => item.itemName;
    public ItemDataSO ItemData => item;

    public void SetItem(ItemDataSO _item)
    {
        item = _item;
        Instantiate(item.worldObject, transform.position, transform.rotation, transform);
        CapsuleCollider savedCollider = item.worldObject.GetComponent<CapsuleCollider>();
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        collider.direction = savedCollider.direction;
        collider.radius = savedCollider.radius;
        collider.height = savedCollider.height;
        collider.center = savedCollider.center;
    }
}
