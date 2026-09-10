using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem notificationSystem;
    [SerializeField] private GameObject content;
    private void OnEnable()
    {
        notificationSystem = this;
        InventorySystem.AddedItem += PickedUpItem;
        QuestProgress.completedObjective += CompletedObjective;
        QuestProgress.progressedObjective += ProgressedObjective;
    }

    private void OnDisable()
    {
        InventorySystem.AddedItem -= PickedUpItem;
        QuestProgress.completedObjective -= CompletedObjective;
        QuestProgress.progressedObjective -= ProgressedObjective;
    }

    private void PickedUpItem(ItemDataSO data)
    {
        AddNotification("Added item: " + data.itemName);
    }

    private void ProgressedObjective(string itemName, int hasAmount, int neededAmount)
    {
        AddNotification(string.Format("{0}: ({1}/{2})", itemName, hasAmount, neededAmount));
    }

    private void CompletedObjective(QuestObjective objective)
    {
        AddNotification("Completed: " + objective.Name);
    }

    public void AddNotification(string notificationText)
    {
        GameObject notificationObj = Instantiate((GameObject)Resources.Load("UI/PopUpSmall"), content.transform.position, content.transform.rotation, content.transform);
        PopUpSmall notification = notificationObj.GetComponent<PopUpSmall>();
        notification.SetPopUpBanner(notificationText);
    }
}
