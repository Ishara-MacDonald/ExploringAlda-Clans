using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(OCraftingTable))]
public class CraftingInteract : Interactable
{

    private void Awake()
    {
        type = InteractionType.Inventory;
        SetAction("Craft");
    }

    public override void Interact(GameObject interactor)
    {
        GetComponent<OCraftingTable>().OnOpen();
    }
}
