using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestLineData", menuName = "Scriptable Objects/QuestLineData")]
public class QuestLine : ScriptableObject
{
    [SerializeField] private string questLineName;
    [SerializeField] private List<Quest> quests;
    private bool started;

    void Awake()
    {
        quests = new();
        started = false;
    }

    public Quest GetQuest(int index)
    {
        return quests[index];
    }
}