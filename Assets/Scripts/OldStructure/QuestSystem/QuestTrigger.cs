using System;
using UnityEngine;

[Serializable]
public class QuestTrigger
{
    private bool isStart;

    [SerializeField] private QuestLine line;

    public void OnQuestTrigger()
    {
        if (line == null) return;
        GameManager.manager.OnQuestTrigger(line);
    }

}