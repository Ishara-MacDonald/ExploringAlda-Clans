using System;
using UnityEngine;

public enum InteractionType
{
    Dialogue,
    Action,
    PickUp,
    Treasure,
    Inventory
}

public abstract class Interactable : MonoBehaviour, IInteractable
{
    protected InteractionType type;

    public static event Action<string> interacted;
    public static event Action<ItemDataSO> interactedItem;
    [SerializeField] protected string interactableName = "Interactable";
    [SerializeField] protected string action = "Interact";
#nullable enable
    [SerializeField] protected QuestTrigger? questTrigger = null;
#nullable disable

    public string GetName => interactableName;
    public string GetAction => action;
    protected void SetAction(string newAction)
    {
        action = newAction;
    }
    protected void InvokeInteracted(string parameter)
    {
        interacted?.Invoke(parameter);
    }
    protected void InvokeInteracted(ItemDataSO parameter)
    {
        interactedItem?.Invoke(parameter);
    }
    public abstract void Interact(GameObject interactor);

    public void CheckQuest()
    {
        questTrigger?.OnQuestTrigger();
        questTrigger = null;
    }
}