using System.Collections.Generic;

public class QuestSystem
{
    List<QuestProgress> questsProgress;

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

    public void RemoveQuestLine(QuestProgress questLine)
    {
        questsProgress.Remove(questLine);
    }
}
