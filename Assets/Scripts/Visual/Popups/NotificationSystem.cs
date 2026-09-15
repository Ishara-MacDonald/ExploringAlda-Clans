using UnityEngine;

public class NotificationSystem : SingletonManager<NotificationSystem>
{
    [SerializeField] private GameObject content;

    public void AddNotification(string notificationText)
    {
        GameObject notificationObj = Instantiate((GameObject)Resources.Load("UI/PopUpSmall"), content.transform.position, content.transform.rotation, content.transform);
        PopUpSmall notification = notificationObj.GetComponent<PopUpSmall>();
        notification.SetPopUpBanner(notificationText);
    }
}
