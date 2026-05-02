
using UnityEngine;

public class QuestProgress
{
    [SerializeField] private QuestLine questLine;
    private Quest currentQuest;
    private QuestObjective currentObjective;
    public QuestProgress(QuestLine _questLine)
    {
        questLine = _questLine;
        currentQuest = questLine.GetQuest(0);
        currentObjective = currentQuest.GetObjective();
    }

    public Quest GetCurrentQuest()
    {
        return currentQuest;
    }

    public QuestObjective GetCurrentObjective()
    {
        return currentObjective;
    }
}
