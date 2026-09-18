using System.Collections.Generic;
using UnityEngine;

public class QuestSystemUI : MonoBehaviour
{
    [SerializeField] private Transform content;

    [SerializeField] private QuestInfoUI questInfoUI;
    private List<QuestProgress> questsProgresses;
    private static GameObject questPreviewPrefab;

    void Awake()
    {
        questsProgresses = new();
        questPreviewPrefab ??= (GameObject)Resources.Load("UI/QuestPreview");
    }

    public void OnOpenQuestList(List<QuestProgress> quests)
    {
        QuestProgress changedQuest = questsProgresses.Find((progress) => progress.IsChanged);
        if (quests.Count == questsProgresses.Count && changedQuest == null) { return; }
        QuestProgress questLineToShow = null;
        if (changedQuest != null) questLineToShow = changedQuest;

        if (quests == null || quests.Count == 0) ShowDefault();
        else
        {
            foreach (QuestProgress questProgress in quests)
            {
                if (questProgress.IsNew) questLineToShow = questProgress;
                if (questsProgresses.Contains(questProgress)) continue;

                GameObject questPreview = Instantiate(questPreviewPrefab, content.position, content.rotation, content);
                QuestPreviewUI previewUI = questPreview.GetComponent<QuestPreviewUI>();
                previewUI.SetQuestPreviewUI(questProgress);
                questsProgresses.Add(questProgress);

            }

            ShowQuestLine(questLineToShow ?? questsProgresses[0]);
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
        line.ToggleIsNew();
        questInfoUI.SetQuestInfoUI(line);
    }
}
