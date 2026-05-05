
using UnityEngine;

public class QuestProgress
{
    [SerializeField] private QuestLine questLine;
    private Quest currentQuest;
    private QuestObjective currentObjective;
    private int currentAmount = 0;
    private bool isChanged = false;
    public QuestProgress(QuestLine _questLine)
    {
        questLine = _questLine;
        currentQuest = questLine.GetQuest(0);
        currentObjective = currentQuest.GetObjective();

        if (currentObjective.Type == QuestObjectiveType.Collect)
        {
            InventorySystem.pickUpItem += PickedUpItem;
        }
    }

    public string QuestLine => questLine.QuestLineName;
    public int CurrentAmount => currentAmount;
    public bool IsChanged => isChanged;

    public void OnUpdated()
    {
        isChanged = false;
    }

    public Quest GetCurrentQuest()
    {
        return currentQuest;
    }

    private void PickedUpItem(InventoryItemData data)
    {
        if (currentObjective.Item.itemName == data.itemName)
        {
            currentAmount++;
            isChanged = true;
        }
    }

    public QuestObjective GetCurrentObjective()
    {
        return currentObjective;
    }
}
