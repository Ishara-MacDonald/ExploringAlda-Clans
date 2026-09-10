using System;

public class QuestProgress
{
    public static event Action<QuestObjective> completedObjective;
    public static event Action<string, int, int> progressedObjective;
    private Quest currentQuest;
    private QuestObjective currentObjective;
    private int objectiveNo = 0;
    private int questNo = 0;
    private int currentAmount = 0;
    private bool isNew = true;
    private bool isChanged = false;
    private bool isCompleted = false;
    private QuestLine questLine;

    public QuestProgress(QuestLine _questLine)
    {
        questLine = _questLine;
        currentQuest = questLine.GetQuest(0);
        currentObjective = currentQuest.GetObjective();
        CheckObjectiveType();
    }

    public string QuestLineName => questLine.QuestLineName;
    public QuestLine QuestLine => questLine;
    public int CurrentAmount => currentAmount;
    public bool IsNew => isNew;
    public bool IsChanged => isChanged;
    public bool IsCompleted => isCompleted;
    public QuestObjective CurrentObjective => currentObjective;

    public void ToggleIsNew()
    {
        isNew = false;
    }

    public void OnUpdated()
    {
        isChanged = false;
    }

    public Quest GetCurrentQuest()
    {
        return currentQuest;
    }

    private void ObjectiveCompleted()
    {
        completedObjective?.Invoke(currentObjective);
        objectiveNo++;
        isChanged = true;
        QuestObjective nextObjective = currentQuest.GetNextObjective(objectiveNo);
        if (nextObjective is null)
        {
            QuestCompleted();
        }
        else
        {
            currentAmount = 0;
            currentObjective = nextObjective;
            CheckObjectiveType();
        }
    }

    private void QuestCompleted()
    {
        questNo++;
        objectiveNo = 0;
        Quest nextQuest = questLine.GetNextQuest(questNo);
        if (nextQuest is null)
        {
            QuestLineCompleted();
        }
        else
        {
            currentQuest = nextQuest;
            CheckObjectiveType();
        }
    }

    private void CheckObjectiveType()
    {
        InventorySystem.AddedItem -= PickedUpItem;
        Interactable.interacted -= Interacted;
        switch (currentObjective.Type)
        {
            case QuestObjectiveType.Collect:
                InventorySystem.AddedItem += PickedUpItem;
                break;
            case QuestObjectiveType.Interact:
                Interactable.interacted += Interacted;
                break;
        }
    }

    private void QuestLineCompleted()
    {
        if (isCompleted) return;
        isCompleted = true;
        LogicManager.manager.QuestLineCompleted(this);

        Interactable.interacted -= Interacted;
        InventorySystem.AddedItem -= PickedUpItem;
    }

    private void PickedUpItem(ItemDataSO data)
    {
        if (currentObjective.Item.itemName == data.itemName)
        {
            currentAmount++;
            isChanged = true;
            if (currentObjective.Amount == currentAmount) { ObjectiveCompleted(); }
            else
            {
                progressedObjective?.Invoke(currentObjective.Name, currentAmount, currentObjective.Amount);
            }
        }
    }

    private void Interacted(string interactionName)
    {
        if (currentObjective.GetInteractable == interactionName)
        {
            ObjectiveCompleted();
        }
    }
}
