using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CraftingMaterial : MonoBehaviour
{
    private ItemDataSO item;

    public string ItemName => item.itemName;

    public void SetItem(ItemDataSO _item)
    {
        item = _item;
        Instantiate(item.worldObject, transform.position, transform.rotation, transform);
        CapsuleCollider savedCollider = item.worldObject.GetComponent<CapsuleCollider>();
        GetComponent<CapsuleCollider>().direction = savedCollider.direction;
        GetComponent<CapsuleCollider>().radius = savedCollider.radius;
        GetComponent<CapsuleCollider>().height = savedCollider.height;
    }
}
