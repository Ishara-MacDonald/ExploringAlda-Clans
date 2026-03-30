

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

    public abstract void Interact(GameObject interactor);
}