using System.Collections.Generic;
using UnityEngine;

// Visual-side per-system manager for the quest list UI. Lives on the same
// GameObject as QuestSystemUI ("QuestSystem") and is the only path VisualManager
// uses to reach it.
public class QuestVisualManager : MonoBehaviour
{
    public static QuestVisualManager Instance;

    // Serialized rather than GetComponent-in-Awake: this GameObject likely starts
    // inactive like InventorySystem did, and Unity never calls Awake() on an inactive
    // object's components, so a GetComponent fetch here could stay null until
    // something else activates it first — but activating it is this manager's job.
    // A serialized reference is valid immediately regardless of active state.
    [SerializeField] private QuestSystemUI questSystemUI;

    void Awake()
    {
        Instance = this;
    }

    public void SetPanelActive(bool active) => questSystemUI.gameObject.SetActive(active);
    public void PopulateQuestList(List<QuestProgress> quests) => questSystemUI.OnOpenQuestList(quests);
    public void ShowQuestDetails(QuestProgress questLine) => questSystemUI.ShowQuestLine(questLine);
}
