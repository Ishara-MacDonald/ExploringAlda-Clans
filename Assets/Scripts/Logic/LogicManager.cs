using System.Collections.Generic;
using UnityEngine;

// Logic-side top hub. Owns non-visual gameplay state (quest system, crafting system, the
// player reference) and is the only Logic-side class allowed to call into VisualManager.
public class LogicManager : MonoBehaviour
{
    public static LogicManager manager;

    private GameObject player;
    private QuestSystem questSystem;

    void Awake()
    {
        manager = this;
        player = GameObject.FindGameObjectWithTag("Player");
        new CraftingSystem();
        questSystem = player.GetComponent<PlayerQuestList>().GetQuestSystem;
    }

    public void SetMovementEnabled(bool newValue)
    {
        player.GetComponent<PlayerMovement>().SetMovementEnabled(newValue);
    }

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
