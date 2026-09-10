using System;
using UnityEngine;

[Serializable]
public class QuestTrigger
{
    [SerializeField] private QuestLine line;

    public void OnQuestTrigger()
    {
        if (line == null) return;
        LogicManager.manager.OnQuestTrigger(line);
    }

}