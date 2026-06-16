using System;
using Unity.VisualScripting;
using UnityEngine;

public class FlowerInteract : Interactable
{
    public static event Action<ItemDataSO, int> pickedUp;
    [SerializeField] private ItemDataSO item;

    private void Awake()
    {
        type = InteractionType.PickUp;
        SetAction("Pick up");
    }

    public override void Interact(GameObject interactor)
    {
        pickedUp?.Invoke(item, 1);
    }
}
