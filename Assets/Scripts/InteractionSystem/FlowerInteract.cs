using System;
using Unity.VisualScripting;
using UnityEngine;

public class FlowerInteract : Interactable
{
    public static event Action<ItemDataSO> pickedUp;
    [SerializeField] private ItemDataSO item;

    private void Awake()
    {
        type = InteractionType.PickUp;
        SetAction("Pick up");
    }

    public override void Interact(GameObject interactor)
    {
        Debug.Log("hiii");
        pickedUp?.Invoke(item);
    }
}
