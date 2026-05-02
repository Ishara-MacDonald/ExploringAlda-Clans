using System.Collections.Generic;
using UnityEngine;

public class QuestSystemUI : MonoBehaviour
{
    [SerializeField] private Transform content;

    [SerializeField] private QuestInfoUI questInfoUI;
    private List<QuestProgress> questsProgress;

    void Awake()
    {
        questsProgress = new();
    }

    public void OnOpenQuestList(List<QuestProgress> quests)
    {
        if (quests == null || quests.Count == 0 || quests.Count == questsProgress.Count) { return; }

        foreach (QuestProgress questProgress in quests)
        {
            if (questsProgress.Contains(questProgress)) continue;
            GameObject questPreview = Instantiate((GameObject)Resources.Load("UI/QuestPreview"), content.position, content.rotation, content);
            questPreview.GetComponent<QuestPreviewUI>().SetQuestPreviewUI(questProgress);
            questsProgress.Add(questProgress);
        }

        ShowQuestLine(questsProgress[0]);
    }

    public void ShowQuestLine(QuestProgress line)
    {
        questInfoUI.SetQuestInfoUI(line);
    }
}
