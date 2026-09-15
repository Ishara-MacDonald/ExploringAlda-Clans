using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem Instance;
    [SerializeField] private GameObject content;

    private void Awake()
    {
        Instance = this;
    }

    public void AddNotification(string notificationText)
    {
        GameObject notificationObj = Instantiate((GameObject)Resources.Load("UI/PopUpSmall"), content.transform.position, content.transform.rotation, content.transform);
        PopUpSmall notification = notificationObj.GetComponent<PopUpSmall>();
        notification.SetPopUpBanner(notificationText);
    }
}
