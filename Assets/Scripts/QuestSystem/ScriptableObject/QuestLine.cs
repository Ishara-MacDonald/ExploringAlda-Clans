using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestLineData", menuName = "Scriptable Objects/QuestLineData")]
public class QuestLine : ScriptableObject
{
    [SerializeField] private string questLineName;
    [SerializeField] private List<Quest> quests;

    public string QuestLineName => questLineName;

    public Quest GetQuest(int index)
    {
        return quests[index];
    }
}