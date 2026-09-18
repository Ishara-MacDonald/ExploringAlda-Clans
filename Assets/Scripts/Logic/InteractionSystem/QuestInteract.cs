using UnityEngine;

public class QuestInteract : Interactable
{
    private void Awake()
    {
        type = InteractionType.Action;
        SetAction("Accept Quest");
    }
    public override void Interact(GameObject interactor)
    {
        CheckQuest();
        Destroy(gameObject);
    }
}
