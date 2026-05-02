using System.Collections.Generic;
using UnityEngine;

public class QuestSystem
{
    [SerializeField] List<QuestProgress> questsProgress;

    public QuestSystem()
    {
        questsProgress = new();
    }

    public List<QuestProgress> GetQuests()
    {
        return questsProgress;
    }

    public void AddQuestLine(QuestLine questLine)
    {
        questsProgress.Add(new QuestProgress(questLine));
    }
}
