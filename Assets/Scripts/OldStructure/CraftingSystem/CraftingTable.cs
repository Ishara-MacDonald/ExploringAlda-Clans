using System;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(OGrabber))]
public class OCraftingTable : MonoBehaviour
{
    private bool isCrafting = false;
    [Header("Crafting Table")]
    [SerializeField] private GameObject constraints;
    [SerializeField] private CinemachineClearShot craftingCamera;
    [SerializeField] private GameObject craftingMaterialPrefab;
    [SerializeField] private GameObject materialSpawnPoint;

    [Header("Materials")]
    [SerializeField] private GameObject materials;
    [SerializeField] private GameObject bookPage;
    [SerializeField] private GameObject craftingStation;

    void Awake()
    {
        ToggleCollisions(false);
    }

    public void OnOpen()
    {
        if (!isCrafting)
            Toggle(true);
    }

    public void OnClose()
    {
        Toggle(false);
    }

    private void Toggle(bool newValue)
    {
        isCrafting = newValue;
        if (!newValue) ResetItems();
        GameManager.manager.OnCraftingToggle(craftingCamera);
        ToggleCollisions(newValue);
        UpdateBook();
        if (newValue)
            CraftingSystem.craftingSystem.OnCraftingTableOpen(this);
    }

    public void AddMaterial(ItemDataSO item)
    {
        GameObject newItem = Instantiate(craftingMaterialPrefab, materialSpawnPoint.transform.position, materialSpawnPoint.transform.rotation, materialSpawnPoint.transform);
        // newItem.transform.Rotate(90f, 0f, 0, Space.Self);
        newItem.GetComponent<OCraftingMaterial>().SetItem(item);
    }

    public void ResetItems()
    {
        foreach (Transform material in materials.transform)
        {
            Destroy(material.gameObject);
        }
    }

    private void ToggleCollisions(bool newValue)
    {
        constraints.SetActive(newValue);
        craftingStation.SetActive(newValue);
        materials.SetActive(newValue);
        GetComponent<OGrabber>().enabled = newValue;
    }

    private void UpdateBook()
    {
        bookPage.transform.Rotate(0f, 0f, 180.0f, Space.Self);
    }
}
