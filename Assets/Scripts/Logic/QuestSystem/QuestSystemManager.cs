using System.Collections.Generic;
using UnityEngine;

// Bridge between Quest classes and LogicManager. Caches PlayerQuestList, not QuestSystem, to avoid Awake-order issues.
public class QuestSystemManager : SingletonManager<QuestSystemManager>
{
    private PlayerQuestList playerQuestList;
    private QuestSystem questSystem => playerQuestList.QuestSystem;

    protected override void Awake()
    {
        base.Awake();
        playerQuestList = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerQuestList>();
    }

    void OnEnable()
    {
        QuestProgress.completedObjective += OnQuestObjectiveCompleted;
        QuestProgress.progressedObjective += OnQuestObjectiveProgressed;
    }

    void OnDisable()
    {
        QuestProgress.completedObjective -= OnQuestObjectiveCompleted;
        QuestProgress.progressedObjective -= OnQuestObjectiveProgressed;
    }

    private void OnQuestObjectiveCompleted(QuestObjective objective) => LogicManager.manager.OnQuestObjectiveCompleted(objective);
    private void OnQuestObjectiveProgressed(string itemName, int hasAmount, int neededAmount) => LogicManager.manager.OnQuestObjectiveProgressed(itemName, hasAmount, neededAmount);

    public List<QuestProgress> GetQuests() => questSystem.GetQuests();

    public void OnQuestTrigger(QuestLine questLine)
    {
        questSystem.AddQuestLine(questLine);
        LogicManager.manager.OnQuestListToggle();
    }

    public void QuestLineCompleted(QuestProgress progress)
    {
        questSystem.RemoveQuestLine(progress);
        LogicManager.manager.ShowCompletedBanner(progress.QuestLine);
    }
}
