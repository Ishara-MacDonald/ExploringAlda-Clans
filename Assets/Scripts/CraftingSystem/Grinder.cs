using UnityEngine;

public class Grinder : MonoBehaviour
{
    [SerializeField] private Transform materials;
    private CraftingMaterial material;

    void OnEnable()
    {
        Pestle.Grinded += GrindedItems;
    }

    void OnDisable()
    {
        Pestle.Grinded -= GrindedItems;
    }

    void OnTriggerEnter(Collider other)
    {
        if (material == null && other.CompareTag("Drag"))
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            other.transform.SetParent(materials);
            other.transform.position = new(materials.position.x, materials.position.y + 0.1f, materials.position.z);
            other.tag = "Untagged";
            material = other.gameObject.GetComponent<CraftingMaterial>();
        }
    }

    void GrindedItems()
    {
        if (material == null) return;
        Debug.Log(material.ItemName + " grinded!");
    }
}
