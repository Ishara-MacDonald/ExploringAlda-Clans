using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestLineData", menuName = "Scriptable Objects/QuestLineData")]
public class QuestLine : ScriptableObject
{
    [SerializeField] private string questLineName;
    [SerializeField] private string questAchievement;
    [SerializeField] private string questSubAchievement;
    [SerializeField] private List<Quest> quests;

    public string QuestLineName => questLineName;
    public string QuestAchievement => questAchievement;
    public string QuestSubAchievement => questSubAchievement;

    public Quest GetQuest(int index)
    {
        return quests[index];
    }

    public Quest GetNextQuest(int nextIndex)
    {
        if (nextIndex >= quests.Count) return null;
        return quests[nextIndex];
    }
}
