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
#nullable enable
    [SerializeField] protected QuestTrigger? questTrigger = null;
#nullable disable

    public abstract void Interact(GameObject interactor);

    public void CheckQuest()
    {
        questTrigger?.OnQuestTrigger();
        questTrigger = null;
    }
}