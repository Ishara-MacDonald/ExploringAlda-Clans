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
        Debug.Log("Picked up flower!");
        interactor.GetComponent<PlayerInventory>().AddItem(item);
        Destroy(gameObject);
    }
}
