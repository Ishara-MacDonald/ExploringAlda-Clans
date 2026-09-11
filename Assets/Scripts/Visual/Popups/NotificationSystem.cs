using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem notificationSystem;
    [SerializeField] private GameObject content;

    private void Awake()
    {
        notificationSystem = this;
    }

    public void AddNotification(string notificationText)
    {
        GameObject notificationObj = Instantiate((GameObject)Resources.Load("UI/PopUpSmall"), content.transform.position, content.transform.rotation, content.transform);
        PopUpSmall notification = notificationObj.GetComponent<PopUpSmall>();
        notification.SetPopUpBanner(notificationText);
    }
}
