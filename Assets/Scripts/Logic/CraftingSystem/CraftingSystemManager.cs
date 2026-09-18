using System.Collections.Generic;
using UnityEngine;

// Landing point for crafting-table drag intents. Lives on Crafting Table, unlike stateless InteractionSystemManager.
[RequireComponent(typeof(Grabber))]
public class CraftingSystemManager : SingletonManager<CraftingSystemManager>
{
    private Grabber grabber;

    protected override void Awake()
    {
        base.Awake();
        grabber = GetComponent<Grabber>();
    }

    public bool HasSelection => grabber.HasSelection;
    public void TryQuickGrab(GameObject hit) => grabber.TryQuickGrab(hit);
    public void TryLongGrab(GameObject hit) => grabber.TryLongGrab(hit);
    public void ReleaseSelected(GameObject putBackHit) => grabber.ReleaseSelected(putBackHit);
    public void Drag(Vector3 targetPosition) => grabber.Drag(targetPosition);
    public void SecondaryAction() => grabber.SecondaryAction();

    // Bridges the recipe/staging singleton — a different concern, but still Crafting's.
    public void AddItem(ItemDataSO item, int amount) => CraftingSystem.Instance.AddItem(item, amount);
    public void OnResetItems() => CraftingSystem.Instance.OnResetItems();
    public void OnCraftingTableClose() => CraftingSystem.Instance.OnCraftingTableClose();
    public Dictionary<ItemDataSO, int> GetStagedAmounts() => CraftingSystem.Instance.GetStagedAmounts();
}
