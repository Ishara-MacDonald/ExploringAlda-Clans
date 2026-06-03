using UnityEngine;

public class Grinder : MonoBehaviour
{
    [SerializeField] private Transform materials;
    [SerializeField] private Pestle pestle;
    private CraftingMaterial material;
    private bool isGrabbed = false;
    private bool isUsed = false;
    private Transform parent;

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
        if (!isUsed) return;
        if (material == null && other.CompareTag("Drag"))
        {
            if (other.transform == null) return;
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

    public bool OnGrab()
    {
        if (!pestle.IsBeingUsed)
        {
            isGrabbed = true;
            gameObject.GetComponent<Collider>().enabled = false;
            pestle.ToGrinder(this);
            return true;
        }
        else return false;
    }

    public void OnSetUse(GameObject locationObj)
    {
        parent = locationObj.transform;

        if (locationObj.TryGetComponent<CraftingGear>(out var gear))
        {
            isUsed = false;
            gear.PutBack(GearType.mortarPestle, gameObject);
        }
        else
        {
            isUsed = true;
            pestle.PutBack();
            transform.parent = parent;
        }

        transform.localPosition = new(0, 0, 0);
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public void PutBackEmpty()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        transform.localPosition = new(0, 0, 0);
    }
}
