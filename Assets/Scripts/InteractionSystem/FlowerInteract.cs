using Unity.VisualScripting;
using UnityEngine;

public class FlowerInteract : Interactable
{
    [SerializeField] private InventoryItemData item;

    private void Awake()
    {
        type = InteractionType.PickUp;
    }

    public override void Interact(GameObject interactor)
    {
        interactor.GetComponent<PlayerInventory>().AddItem(item);
        Destroy(gameObject);
    }
}
