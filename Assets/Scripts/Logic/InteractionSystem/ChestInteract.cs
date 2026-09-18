using UnityEngine;

[RequireComponent(typeof(ChestContext))]
public class ChestInteract : Interactable
{
    private void Awake()
    {
        type = InteractionType.Inventory;
        SetAction("Open");
    }
    public override void Interact(GameObject interactor)
    {
        GetComponent<ChestContext>().OnInteraction();
    }
}
