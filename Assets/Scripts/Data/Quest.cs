using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{
    [SerializeField] private string questName;
    [SerializeField] private string questDescription;
    [SerializeField] private List<QuestObjective> objectives;

    public string QuestName => questName;
    public string QuestDescription => questDescription;

    public QuestObjective GetObjective()
    {
        return objectives[0];
    }

    public QuestObjective GetNextObjective(int nextIndex)
    {
        if (nextIndex >= objectives.Count) return null;
        return objectives[nextIndex];
    }
}