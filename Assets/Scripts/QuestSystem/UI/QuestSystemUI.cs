using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestSystemUI : MonoBehaviour
{
    [SerializeField] private Transform content;

    [SerializeField] private QuestInfoUI questInfoUI;
    private List<QuestProgress> questsProgresses;

    void Awake()
    {
        questsProgresses = new();
    }

    public void OnOpenQuestList(List<QuestProgress> quests)
    {
        QuestProgress changedQuest = questsProgresses.Find((progress) => progress.IsChanged);
        bool isQuestsSame = quests.Count == questsProgresses.Count && changedQuest is null;
        if (quests == null || quests.Count == 0 || isQuestsSame) { return; }

        foreach (QuestProgress questProgress in quests)
        {
            if (questsProgresses.Contains(questProgress)) continue;
            GameObject questPreview = Instantiate((GameObject)Resources.Load("UI/QuestPreview"), content.position, content.rotation, content);
            questPreview.GetComponent<QuestPreviewUI>().SetQuestPreviewUI(questProgress);
            questsProgresses.Add(questProgress);
        }

        ShowQuestLine(questsProgresses[0]);
    }

    public void ShowQuestLine(QuestProgress line)
    {
        questInfoUI.SetQuestInfoUI(line);
    }
}
