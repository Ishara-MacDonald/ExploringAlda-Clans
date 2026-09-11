using System.Collections.Generic;
using UnityEngine;

// Logic-side per-system manager for Quests. Bridge between the Quest classes
// (QuestTrigger, QuestProgress) and LogicManager. Caches the PlayerQuestList
// component (safe regardless of Awake order) rather than its QuestSystem value,
// so it never depends on PlayerQuestList.Awake() having already run.
public class QuestSystemManager : MonoBehaviour
{
    public static QuestSystemManager Instance;

    private PlayerQuestList playerQuestList;
    private QuestSystem questSystem => playerQuestList.GetQuestSystem;

    void Awake()
    {
        Instance = this;
        playerQuestList = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuestList>();
    }

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
