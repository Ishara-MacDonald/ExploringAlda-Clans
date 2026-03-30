using UnityEngine;

[RequireComponent(typeof(PlotStateContext))]
public class PlotInteract : Interactable
{

    private void Awake()
    {
        type = InteractionType.Action;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public override void Interact(GameObject interactor)
    {
        GetComponent<PlotStateContext>().OnInteraction();
    }
}
