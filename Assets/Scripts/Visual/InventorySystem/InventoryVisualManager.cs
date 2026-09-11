using UnityEngine;

// Visual manager for the main inventory UI, on "InventorySystem" (not "SimpleInventorySystem").
public class InventoryVisualManager : MonoBehaviour
{
    public static InventoryVisualManager Instance;

    // Serialized, not GetComponent: this GameObject starts inactive, so Awake() won't fire.
    [SerializeField] private InventoryUI inventoryUI;

    void Awake()
    {
        Instance = this;
    }

    public void ShowPanel() => inventoryUI.gameObject.SetActive(true);
    public void HidePanel() => inventoryUI.gameObject.SetActive(false);
    public void OpenInventory(InventorySystem system) => inventoryUI.OnOpenInventory(system);
    public void CloseInventory() => inventoryUI.OnCloseInventory();

    // Cross-system query — routes through the top hub, like Logic-side managers reaching LogicManager.
    public int GetCraftingStagedAmount(ItemDataSO item) => VisualManager.manager.GetCraftingStagedAmount(item);
}
