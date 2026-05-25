using System.Collections.Generic;
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
        if (quests.Count == questsProgresses.Count && changedQuest is null) { return; }

        if (quests.Count == 0 || quests == null) ShowDefault();
        else
        {
            foreach (QuestProgress questProgress in quests)
            {
                if (questsProgresses.Contains(questProgress)) continue;

                GameObject questPreview = Instantiate((GameObject)Resources.Load("UI/QuestPreview"), content.position, content.rotation, content);
                QuestPreviewUI previewUI = questPreview.GetComponent<QuestPreviewUI>();
                previewUI.SetQuestPreviewUI(questProgress);
                questsProgresses.Add(questProgress);
            }

            ShowQuestLine(questsProgresses[0]);
        }
    }

    public void ShowDefault()
    {
        questInfoUI.ShowDefault();
        foreach (Transform child in content)
            Destroy(child.gameObject);
    }

    public void ShowQuestLine(QuestProgress line)
    {
        questInfoUI.SetQuestInfoUI(line);
    }
}
