using UnityEngine;

public class NotificationSystem : SingletonManager<NotificationSystem>
{
    [SerializeField] private GameObject content;
    private static GameObject popUpSmallPrefab;

    public void AddNotification(string notificationText)
    {
        popUpSmallPrefab ??= (GameObject)Resources.Load("UI/PopUpSmall");
        GameObject notificationObj = Instantiate(popUpSmallPrefab, content.transform.position, content.transform.rotation, content.transform);
        PopUpSmall notification = notificationObj.GetComponent<PopUpSmall>();
        notification.SetPopUpBanner(notificationText);
    }
}
