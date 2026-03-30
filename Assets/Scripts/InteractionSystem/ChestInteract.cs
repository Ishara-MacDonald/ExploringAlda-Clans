using UnityEngine;

public class ChestInteract : Interactable
{

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
        Debug.Log("Opening Chest!");
    }
}
