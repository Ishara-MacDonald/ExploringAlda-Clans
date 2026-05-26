using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySystem currentSystem;
    [SerializeField] private Transform content;
    [SerializeField] private ItemInfoUI itemInfoUI;
    [SerializeField] private bool isSimple = false;
    [SerializeField] private GameObject inventoryDisplay;

    void OnEnable()
    {
        InventorySlotUI.ItemInventoryInteracted += InteractItem;
    }

    void OnDisable()
    {
        InventorySlotUI.ItemInventoryInteracted -= InteractItem;
    }

    public void OnOpenInventory(InventorySystem newSystem)
    {
        currentSystem = newSystem;
        List<InventorySlot> invSlots = currentSystem.InventorySlots;
        if (invSlots == null || invSlots.Count == 0) { return; }
        foreach (InventorySlot invSlot in invSlots)
        {
            GameObject uiSlot = Instantiate((GameObject)Resources.Load("UI/InventorySlot"), content.position, content.rotation, content);
            uiSlot.GetComponent<InventorySlotUI>().SetInventorySlotUI(invSlot.GetItem, invSlot.GetAmount);
        }
        if (!isSimple) InteractItem(invSlots[0].GetItem, 0);
    }

    public void InteractItem(ItemDataSO item, int _)
    {
        if (!isSimple) itemInfoUI.SetItemInfoUI(item);
    }

    public void ToggleInventoryDisplay()
    {
        bool isActive = inventoryDisplay.activeSelf;
        inventoryDisplay.SetActive(!isActive);
    }

    public void OnCloseInventory()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        currentSystem = null;
    }
}
