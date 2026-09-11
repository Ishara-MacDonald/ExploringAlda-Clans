using System.Collections.Generic;
using UnityEngine;

// Logic-side top hub. Owns non-visual gameplay state (quest system, crafting system, the
// player reference) and is the only Logic-side class allowed to call into VisualManager.
public class LogicManager : MonoBehaviour
{
    public static LogicManager manager;

    private GameObject player;
    private QuestSystem questSystem;
    private PlayerManager playerManager;

    void Awake()
    {
        manager = this;
        player = GameObject.FindGameObjectWithTag("Player");
        new CraftingSystem();
        questSystem = player.GetComponent<PlayerQuestList>().GetQuestSystem;
        playerManager = player.GetComponent<PlayerManager>();

        InventorySystem.AddedItem += OnItemAdded;
        QuestProgress.completedObjective += OnQuestObjectiveCompleted;
        QuestProgress.progressedObjective += OnQuestObjectiveProgressed;
    }

    private void OnItemAdded(ItemDataSO item) => VisualManager.manager.ShowItemAddedNotification(item);
    private void OnQuestObjectiveCompleted(QuestObjective objective) => VisualManager.manager.ShowObjectiveCompletedNotification(objective);
    private void OnQuestObjectiveProgressed(string itemName, int hasAmount, int neededAmount) => VisualManager.manager.ShowObjectiveProgressedNotification(itemName, hasAmount, neededAmount);
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

    public List<QuestProgress> GetQuests()
    {
        return questSystem.GetQuests();
    }

    public void OnQuestTrigger(QuestLine questLine)
    {
        questSystem.AddQuestLine(questLine);
        VisualManager.manager.OnQuestListToggle();
    }

    public void QuestLineCompleted(QuestProgress progress)
    {
        questSystem.RemoveQuestLine(progress);
        VisualManager.manager.ShowCompletedBanner(progress.QuestLine);
    }

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

    public void OnCraftingItemClicked(ItemDataSO item, int amount) => CraftingSystem.craftingSystem.AddItem(item, amount);
    public void OnCraftingReset() => CraftingSystem.craftingSystem.OnResetItems();
    public void OnCraftingTableClosed() => CraftingSystem.craftingSystem.OnCraftingTableClose();
    public int GetCraftingStagedAmount(ItemDataSO item) => CraftingSystem.craftingSystem.GetStagedAmount(item);
}
