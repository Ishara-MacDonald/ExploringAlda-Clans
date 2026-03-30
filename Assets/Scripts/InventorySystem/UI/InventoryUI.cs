using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySystem currentSystem;
    [SerializeField] private Transform content;
    [SerializeField] private ItemInfoUI itemInfoUI;
    private List<InventorySlotUI> slots;

    public void OnOpenInventory(InventorySystem newSystem)
    {
        currentSystem = newSystem;
        slots = new();
        List<InventorySlot> invSlots = currentSystem.GetSlots();
        if (invSlots == null || invSlots.Count == 0) { return; }
        foreach (InventorySlot invSlot in invSlots)
        {
            GameObject uiSlot = Instantiate((GameObject)Resources.Load("UI/InventorySlot"), content.position, content.rotation, content);
            uiSlot.GetComponent<InventorySlotUI>().SetInventorySlotUI(invSlot.GetItem, invSlot.GetAmount);
        }
        ShowItem(invSlots[0].GetItem);
    }

    public void ShowItem(InventoryItemData item)
    {
        itemInfoUI.SetInventorySlotUI(item);
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
