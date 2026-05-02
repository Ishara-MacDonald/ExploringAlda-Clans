using UnityEngine;

public class ChestInteract : Interactable
{

    [SerializeField] private InventoryItemData item;
    private void Awake()
    {
        type = InteractionType.Inventory;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public override void Interact(GameObject interactor)
    {
        CheckQuest();
    }
}
