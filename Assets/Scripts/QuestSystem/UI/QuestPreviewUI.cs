

using TMPro;
using UnityEngine;

public class QuestPreviewUI : MonoBehaviour
{
    private QuestProgress progress;
    [SerializeField] private TextMeshProUGUI title;

    public void SetQuestPreviewUI(QuestProgress _progress)
    {
        progress = _progress;
        title.SetText(_progress.GetCurrentQuest().QuestName);
    }

    public void ShowQuestDetails()
    {
        GameManager.manager.ShowQuestDetails(progress);
    }
}