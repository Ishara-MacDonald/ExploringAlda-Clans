using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Grinder : CraftingGear
{
    [SerializeField] private Transform originalSpot;
    [SerializeField] private Transform materials;
    [SerializeField] private Pestle pestle;
    [SerializeField] private CraftingMethod method;
    [SerializeField] private Transform benchMaterials;
    private List<GameObject> materialList;
    private bool isUsed = false;
    private Transform parent;

    void Start()
    {
        materialList = new();
    }

    void OnEnable()
    {
        Pestle.Grinded += OnUse;
    }

    void OnDisable()
    {
        Pestle.Grinded -= OnUse;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isUsed) return;
        if (materialList.Count == 0 && other.CompareTag("Drag"))
        {
            if (other.transform == null) return;

            TransferItem(other.transform, true);
            materialList.Add(other.gameObject);
        }
    }

    private void OnEnableColliders()
    {
        GetComponent<Collider>().enabled = true;
        Grabber.OnLetGoItem -= OnEnableColliders;
    }

    public override GameObject OnGrab()
    {
        if (materialList.Count > 0)
        {
            Grabber.OnLetGoItem += OnEnableColliders;
            GetComponent<Collider>().enabled = false;
            GameObject material = materialList[0];
            materialList.Remove(material);
            TransferItem(material.transform, false);
            return material;
        }
        return null;
    }

    public override bool OnLongGrab()
    {
        if (!pestle.IsBeingUsed)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            pestle.ToGrinder(this);
            return true;
        }
        else return false;

    }

    public override void OnUse()
    {
        if (materialList.Count == 0) return;
        List<CraftingMaterial> craftingMaterials = materialList.Select((material) => material.GetComponent<CraftingMaterial>()).ToList();
        if (CraftingSystem.craftingSystem.ProcessItem(craftingMaterials))
        {
            foreach (GameObject material in materialList)
            {
                Destroy(material);
            }
        }
    }

    public override void OnPlaceDown(GameObject locationObj)
    {
        parent = locationObj.transform;

        if (locationObj.CompareTag("CraftingBench"))
        {
            isUsed = false;
            MoveOriginalSpot(method, originalSpot);
            OnMaterialReset();
            CraftingSystem.craftingSystem.SetCurrentMethod(CraftingMethod.Picking);
        }
        else
        {
            isUsed = true;
            pestle.MoveOriginalSpot();
            transform.parent = parent;
            CraftingSystem.craftingSystem.SetCurrentMethod(method);
        }

        transform.localPosition = new(0, 0, 0);
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public void OnMaterialReset()
    {
        foreach (Transform material in materials)
        {
            TransferItem(material, false);
        }
        materialList = new();

    }

    private void TransferItem(Transform item, bool isIncoming)
    {
        item.GetComponent<Rigidbody>().useGravity = !isIncoming;
        item.GetComponent<Rigidbody>().isKinematic = isIncoming;
        item.GetComponent<Collider>().enabled = !isIncoming;

        item.parent = isIncoming ? materials : benchMaterials;
        item.position = isIncoming ? new(materials.position.x, materials.position.y, materials.position.z) : benchMaterials.position;
        item.tag = isIncoming ? "Untagged" : "Drag";
    }

    public override void OnPutBack()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        transform.localPosition = new(0, 0, 0);
    }
}
