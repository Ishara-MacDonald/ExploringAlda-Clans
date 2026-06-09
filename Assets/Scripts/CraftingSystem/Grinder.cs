using System;
using UnityEngine;

public class Grinder : CraftingGear
{
    [SerializeField] private Transform originalSpot;
    [SerializeField] private Transform materials;
    [SerializeField] private Pestle pestle;
    [SerializeField] private GearType type;
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
            other.transform.position = new(materials.position.x, materials.position.y, materials.position.z);
            other.attachedRigidbody.useGravity = false;
            other.attachedRigidbody.isKinematic = true;
            other.GetComponent<Collider>().enabled = false;
            other.tag = "Untagged";
            material = other.gameObject.GetComponent<CraftingMaterial>();
        }
    }

    void GrindedItems()
    {
        if (material == null) return;
        Debug.Log(material.ItemName + " grinded!");
    }

    public override bool OnGrab()
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

    public override void OnUse()
    {
        InvokeOnUse(type);
    }

    public override void OnMove(GameObject locationObj)
    {
        parent = locationObj.transform;

        if (locationObj.CompareTag("CraftingBench"))
        {
            isUsed = false;
            MoveOriginalSpot(type, originalSpot);
        }
        else
        {
            isUsed = true;
            pestle.MoveOriginalSpot();
            transform.parent = parent;
        }

        transform.localPosition = new(0, 0, 0);
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public override void OnPutBack()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        transform.localPosition = new(0, 0, 0);
    }
}
