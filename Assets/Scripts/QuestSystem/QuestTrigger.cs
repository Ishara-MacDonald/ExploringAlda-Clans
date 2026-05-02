using System;
using UnityEngine;

[Serializable]
public class QuestTrigger
{
    private bool isStart;

    [SerializeField] private QuestLine line;

    public void OnQuestTrigger()
    {
        GameManager.manager.OnQuestTrigger(line);
    }

}