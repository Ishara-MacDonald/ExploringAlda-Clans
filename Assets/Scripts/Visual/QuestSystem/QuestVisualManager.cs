using System.Collections.Generic;
using UnityEngine;

// Visual manager for the quest list UI on "QuestSystem" — VisualManager's only path to it.
public class QuestVisualManager : MonoBehaviour
{
    public static QuestVisualManager Instance;

    // Serialized, not GetComponent: this GameObject may start inactive, so Awake() might not fire.
    [SerializeField] private QuestSystemUI questSystemUI;

    void Awake()
    {
        Instance = this;
    }

    public void SetPanelActive(bool active) => questSystemUI.gameObject.SetActive(active);
    public void PopulateQuestList(List<QuestProgress> quests) => questSystemUI.OnOpenQuestList(quests);
    public void ShowQuestDetails(QuestProgress questLine) => questSystemUI.ShowQuestLine(questLine);
}
