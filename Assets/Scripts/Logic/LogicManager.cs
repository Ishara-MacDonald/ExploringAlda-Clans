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
    }

    public void SetMovementEnabled(bool newValue) => playerManager.SetMovementEnabled(newValue);
    public void SetPlayerMoveInput(Vector2 moveInput) => playerManager.SetMoveInput(moveInput);
    public void SetPlayerSprintEnabled(bool enabled) => playerManager.SetSprintEnabled(enabled);
    public void TryPlayerJump() => playerManager.TryJump();
    public InventorySystem GetPlayerInventorySystem() => playerManager.GetInventorySystem();

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
}
