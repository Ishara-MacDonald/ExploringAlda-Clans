using System;
using UnityEngine;

public class OverworldItem : Interactable
{
    public static event Action<ItemDataSO, int> ItemPickUp;
    [SerializeField] private ItemDataSO currentItem;

    public ItemDataSO CurrentItem => currentItem;

    void Awake()
    {
        type = InteractionType.PickUp;
        gameObject.tag = "Interactable";

        gameObject.AddComponent<SphereCollider>().isTrigger = true;
        if (transform.childCount <= 0) Instantiate((GameObject)Resources.Load("Gatherables/InteractSign"), transform.position, transform.rotation, transform);
        if (currentItem != null) Instantiate(currentItem.worldObject, transform.position, transform.rotation, transform);
        SetAction("Pick up");
    }

    public void SetDetails(ItemDataSO _item, string newAction)
    {
        currentItem = _item;
        SetAction(newAction);
        Instantiate(currentItem.worldObject, transform.position, transform.rotation, transform);
    }

    public override void Interact(GameObject interactor)
    {
        CheckQuest();
        ItemPickUp?.Invoke(currentItem, 1);
        Destroy(gameObject);
    }
}
