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
    private static GameObject inventorySlotPrefab;

    void Awake()
    {
        uiSlots = new();
        inventorySlotPrefab ??= (GameObject)Resources.Load("UI/InventorySlot");
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
        uiSlots.Clear();
        List<InventorySlot> invSlots = currentSystem.InventorySlots;
        if (invSlots == null || invSlots.Count == 0) { return; }
        foreach (InventorySlot invSlot in invSlots)
        {
            CreateSlot(invSlot);
        }
        if (!isSimple) InteractItem(invSlots[0].Item, 0);
    }

    private void CreateSlot(InventorySlot invSlot)
    {
        GameObject uiSlot = Instantiate(inventorySlotPrefab, content.position, content.rotation, content);
        uiSlot.GetComponent<InventorySlotUI>().SetInventorySlotUI(invSlot.Item, invSlot.Amount);
        uiSlots.Add(uiSlot.GetComponent<InventorySlotUI>());
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
        List<ItemDataSO> invItems = currentSystem.InventorySlots.Select(slot => slot.Item).ToList();

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
                    InventorySlot invSlot = currentSystem.InventorySlots.Find(invSlot => invSlot.Item.Equals(item));
                    CreateSlot(invSlot);
                }
            }
        }
    }

    private void UpdateAmount()
    {
        Dictionary<ItemDataSO, int> stagedAmounts = InventoryVisualManager.Instance.GetCraftingStagedAmounts();
        foreach (InventorySlot slot in currentSystem.InventorySlots)
        {
            InventorySlotUI uiSlot = uiSlots.Find(uiSlot => uiSlot.Item == slot.Item);
            if (uiSlot == null) continue;
            // Subtract staged-for-crafting amount; real inventory only changes on a successful craft.
            stagedAmounts.TryGetValue(slot.Item, out int staged);
            int displayAmount = slot.Amount - staged;
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
        uiSlots.Clear();
        currentSystem = null;
    }
}
