using UnityEngine;

// popupBanner is serialized, not GetComponent, since its GameObject starts inactive (Awake won't fire).
public class PopupsVisualManager : MonoBehaviour
{
    public static PopupsVisualManager Instance;

    [SerializeField] private PopUpBanner popupBanner;

    void Awake()
    {
        Instance = this;
        popupBanner.gameObject.SetActive(false);
    }

    public void ShowCompletedBanner(QuestLine questLine)
    {
        popupBanner.gameObject.SetActive(true);
        popupBanner.SetPopUpBanner(questLine);
    }

    public void ShowNotification(string text) => NotificationSystem.notificationSystem.AddNotification(text);
    public void ShowItemAddedNotification(ItemDataSO item) => NotificationSystem.notificationSystem.AddNotification("Added item: " + item.itemName);
    public void ShowObjectiveCompletedNotification(QuestObjective objective) => NotificationSystem.notificationSystem.AddNotification("Completed: " + objective.Name);
    public void ShowObjectiveProgressedNotification(string itemName, int hasAmount, int neededAmount) => NotificationSystem.notificationSystem.AddNotification(string.Format("{0}: ({1}/{2})", itemName, hasAmount, neededAmount));
}
