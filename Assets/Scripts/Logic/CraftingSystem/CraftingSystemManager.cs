using UnityEngine;

// Landing point for crafting-table drag intents. Lives on Crafting Table, unlike stateless InteractionSystemManager.
[RequireComponent(typeof(Grabber))]
public class CraftingSystemManager : MonoBehaviour
{
    public static CraftingSystemManager Instance;

    private Grabber grabber;

    void Awake()
    {
        Instance = this;
        grabber = GetComponent<Grabber>();
    }

    public bool HasSelection => grabber.HasSelection;
    public void TryQuickGrab(GameObject hit) => grabber.TryQuickGrab(hit);
    public void TryLongGrab(GameObject hit) => grabber.TryLongGrab(hit);
    public void ReleaseSelected(GameObject putBackHit) => grabber.ReleaseSelected(putBackHit);
    public void Drag(Vector3 targetPosition) => grabber.Drag(targetPosition);
    public void SecondaryAction() => grabber.SecondaryAction();

    // Bridges the recipe/staging singleton — a different concern, but still Crafting's.
    public void AddItem(ItemDataSO item, int amount) => CraftingSystem.craftingSystem.AddItem(item, amount);
    public void OnResetItems() => CraftingSystem.craftingSystem.OnResetItems();
    public void OnCraftingTableClose() => CraftingSystem.craftingSystem.OnCraftingTableClose();
    public int GetStagedAmount(ItemDataSO item) => CraftingSystem.craftingSystem.GetStagedAmount(item);
}
