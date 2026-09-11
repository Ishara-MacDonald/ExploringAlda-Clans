using System.Collections.Generic;
using UnityEngine;

// Logic-side top hub. Owns non-visual gameplay state (quest system, crafting system, the
// player reference) and is the only Logic-side class allowed to call into VisualManager.
public class LogicManager : MonoBehaviour
{
    public static LogicManager manager;

    private GameObject player;
    private PlayerManager playerManager;

    void Awake()
    {
        manager = this;
        player = GameObject.FindGameObjectWithTag("Player");
        new CraftingSystem();
        playerManager = player.GetComponent<PlayerManager>();
    }

    // Called by InventorySystemManager/QuestSystemManager, which own the actual
    // event subscriptions — LogicManager never subscribes to a Logic class's event
    // directly, only its SystemManagers do.
    public void OnItemAdded(ItemDataSO item) => VisualManager.manager.ShowItemAddedNotification(item);
    public void OnQuestObjectiveCompleted(QuestObjective objective) => VisualManager.manager.ShowObjectiveCompletedNotification(objective);
    public void OnQuestObjectiveProgressed(string itemName, int hasAmount, int neededAmount) => VisualManager.manager.ShowObjectiveProgressedNotification(itemName, hasAmount, neededAmount);
    public void ShowNotification(string text) => VisualManager.manager.ShowNotification(text);

    public void SetMovementEnabled(bool newValue) => playerManager.SetMovementEnabled(newValue);
    public void SetPlayerMoveInput(Vector2 moveInput) => playerManager.SetMoveInput(moveInput);
    public void SetPlayerSprintEnabled(bool enabled) => playerManager.SetSprintEnabled(enabled);
    public void TryPlayerJump() => playerManager.TryJump();
    public InventorySystem GetPlayerInventorySystem() => playerManager.GetInventorySystem();
    public void OnInteract(Interactable interactable) => InteractionSystemManager.Instance.Interact(interactable, player);

    public bool HasCraftingSelection() => CraftingSystemManager.Instance.HasSelection;
    public void OnCraftingQuickGrab(GameObject hit) => CraftingSystemManager.Instance.TryQuickGrab(hit);
    public void OnCraftingLongGrab(GameObject hit) => CraftingSystemManager.Instance.TryLongGrab(hit);
    public void OnCraftingRelease(GameObject putBackHit) => CraftingSystemManager.Instance.ReleaseSelected(putBackHit);
    public void OnCraftingDrag(Vector3 targetPosition) => CraftingSystemManager.Instance.Drag(targetPosition);
    public void OnCraftingSecondaryAction() => CraftingSystemManager.Instance.SecondaryAction();

    public List<QuestProgress> GetQuests() => QuestSystemManager.Instance.GetQuests();
    public void ShowCompletedBanner(QuestLine questLine) => VisualManager.manager.ShowCompletedBanner(questLine);

    public void OnQuestListToggle()
    {
        VisualManager.manager.OnQuestListToggle();
    }

    public void OnCraftingToggle(Unity.Cinemachine.CinemachineClearShot craftingCam)
    {
        VisualManager.manager.OnCraftingToggle(craftingCam);
    }

    public void OnAddProcessItem(ItemDataSO item) { VisualManager.manager.OnAddProcessItem(item); }
    public void OnRemoveItem(ItemDataSO item) { VisualManager.manager.OnRemoveItem(item); }
    public void OnCraftingItemsStaged() => VisualManager.manager.OnCraftingItemsStaged();

    public void OnCraftingItemClicked(ItemDataSO item, int amount) => CraftingSystemManager.Instance.AddItem(item, amount);
    public void OnCraftingReset() => CraftingSystemManager.Instance.OnResetItems();
    public void OnCraftingTableClosed() => CraftingSystemManager.Instance.OnCraftingTableClose();
    public int GetCraftingStagedAmount(ItemDataSO item) => CraftingSystemManager.Instance.GetStagedAmount(item);
}
