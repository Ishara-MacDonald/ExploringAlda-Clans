using UnityEngine;

// Visual-side per-system manager for the main player inventory UI. Lives on the
// same GameObject as InventoryUI ("InventorySystem" — NOT "SimpleInventorySystem",
// which is CraftingSystemUI's embedded inventory) and is the only path VisualManager
// uses to reach it.
public class InventoryVisualManager : MonoBehaviour
{
    public static InventoryVisualManager Instance;

    // Serialized rather than GetComponent-in-Awake: this GameObject starts inactive,
    // and Unity never calls Awake() on an inactive object's components, so a
    // GetComponent fetch here would stay null until something else activates it first
    // — but activating it is exactly this manager's job. A serialized reference is
    // valid immediately regardless of active state or Awake timing.
    [SerializeField] private InventoryUI inventoryUI;

    void Awake()
    {
        Instance = this;
    }

    public void ShowPanel() => inventoryUI.gameObject.SetActive(true);
    public void HidePanel() => inventoryUI.gameObject.SetActive(false);
    public void OpenInventory(InventorySystem system) => inventoryUI.OnOpenInventory(system);
    public void CloseInventory() => inventoryUI.OnCloseInventory();

    // Cross-system query — Inventory needs Crafting's staged-amount data, so it goes
    // through the top hub as broker, same pattern as Logic-side per-system managers
    // reaching LogicManager for cross-system needs.
    public int GetCraftingStagedAmount(ItemDataSO item) => VisualManager.manager.GetCraftingStagedAmount(item);
}
