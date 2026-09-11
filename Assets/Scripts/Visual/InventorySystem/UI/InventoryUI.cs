using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySystem currentSystem;
    [SerializeField] private Transform content;
    [SerializeField] private ItemInfoUI itemInfoUI;
    [SerializeField] private bool isSimple = false;
    [SerializeField] private GameObject inventoryDisplay;
    private List<InventorySlotUI> uiSlots;

    void Awake()
    {
        uiSlots = new();
    }

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
            uiSlots.Add(uiSlot.GetComponent<InventorySlotUI>());
        }
        if (!isSimple) InteractItem(invSlots[0].GetItem, 0);
    }

    public void InteractItem(ItemDataSO item, int _)
    {
        if (!isSimple) itemInfoUI.SetItemInfoUI(item);
    }

    public void UpdateUI()
    {
        UpdateItems();
        UpdateAmount();
    }

    private void UpdateItems()
    {
        List<ItemDataSO> uiItems = uiSlots.Select(slot => slot.Item).ToList();
        List<ItemDataSO> invItems = currentSystem.InventorySlots.Select(slot => slot.GetItem).ToList();

        if (!Helpers.CompareLists(uiItems, invItems))
        {
            List<ItemDataSO> onlyInUIItem = uiItems.Except(invItems).ToList();
            if (onlyInUIItem.Count > 0)
            {
                foreach (ItemDataSO item in onlyInUIItem)
                {
                    InventorySlotUI uiSlot = uiSlots.Find(uiSlot => uiSlot.Item.Equals(item));
                    uiSlots.Remove(uiSlot);
                    Destroy(uiSlot.gameObject);
                }
            }

            List<ItemDataSO> onlyInDataItems = invItems.Except(uiItems).ToList();

            if (onlyInDataItems.Count > 0)
            {
                foreach (ItemDataSO item in onlyInDataItems)
                {
                    InventorySlot invSlot = currentSystem.InventorySlots.Find(invSlot => invSlot.GetItem.Equals(item));
                    GameObject uiSlot = Instantiate((GameObject)Resources.Load("UI/InventorySlot"), content.position, content.rotation, content);
                    uiSlot.GetComponent<InventorySlotUI>().SetInventorySlotUI(invSlot.GetItem, invSlot.GetAmount);
                    uiSlots.Add(uiSlot.GetComponent<InventorySlotUI>());
                }
            }
        }
    }

    private void UpdateAmount()
    {
        foreach (InventorySlot slot in currentSystem.InventorySlots)
        {
            InventorySlotUI uiSlot = uiSlots.Find(uiSlot => uiSlot.Item == slot.GetItem);
            if (uiSlot == null) continue;
            // Subtract anything already staged for the current craft, without touching
            // the player's real inventory data — it's only actually removed on a
            // successful craft (see CraftingSystem.ReleaseStagedItem).
            int displayAmount = slot.GetAmount - InventoryVisualManager.Instance.GetCraftingStagedAmount(slot.GetItem);
            if (uiSlot.Amount == displayAmount) continue;
            uiSlot.SetAmount(displayAmount);
        }
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
