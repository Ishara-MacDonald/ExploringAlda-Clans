using UnityEngine;

[RequireComponent(typeof(CraftingTable))]
public class ShedInteract : Interactable
{

    private void Awake()
    {
        type = InteractionType.Inventory;
        SetAction("Check the shed.");
    }

    public override void Interact(GameObject interactor)
    {
        GetComponent<CraftingTable>().OnOpen();
    }
}
