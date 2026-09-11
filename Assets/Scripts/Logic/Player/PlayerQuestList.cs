using UnityEngine;

public class PlayerQuestList : MonoBehaviour
{
    private QuestSystem questSystem;

    void Awake()
    {
        questSystem = new();
    }

    public QuestSystem GetQuestSystem => questSystem;
}
