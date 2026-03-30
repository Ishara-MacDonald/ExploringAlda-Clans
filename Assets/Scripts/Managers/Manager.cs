using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    [SerializeField] private InventoryUI inventory;
    private CinemachineInputAxisController mainCamera;

    void Awake()
    {
        manager = this;
        mainCamera = GameObject.FindGameObjectWithTag("FreeLookCamera").GetComponent<CinemachineInputAxisController>();
        InMenu(false);
    }

    public void OpenInventory(InventorySystem system)
    {
        inventory.gameObject.SetActive(true);
        inventory.OnOpenInventory(system);
        InMenu(true);
    }

    public void ShowItemDetails(InventoryItemData item)
    {
        inventory.ShowItem(item);
    }

    public void CloseInventory()
    {
        inventory.OnCloseInventory();
        inventory.gameObject.SetActive(false);
        InMenu(false);
    }

    private void InMenu(bool isInMenu)
    {
        Cursor.visible = isInMenu;
        Cursor.lockState = isInMenu ? CursorLockMode.None : CursorLockMode.Locked;
        mainCamera.enabled = !isInMenu;
    }
}
