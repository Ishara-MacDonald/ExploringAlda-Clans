using UnityEngine;

[RequireComponent(typeof(PlotStateContext))]
public class PlotInteract : Interactable
{
    [SerializeField] private ItemDataSO objectRequired;
    private bool isEnabled;
    private void Awake()
    {
        type = InteractionType.Action;
        SetAction("Plant");
    }

    public override void Interact(GameObject interactor)
    {
        if (!isEnabled)
        {
            if (interactor.GetComponent<InventorySystem>().HasItem(objectRequired))
            {
                isEnabled = true;
            }
            else
            {
                if (!objectRequired) return;
                LogicManager.manager.ShowNotification("Missing " + objectRequired.itemName);
                return;
            }
        }

        InvokeInteracted(interactableName);
        GetComponent<PlotStateContext>().OnInteraction();
    }
}
