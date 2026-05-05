using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public static bool questUIOpen = false;
    public static bool inventoryUIOpen = false;


    [SerializeField] private InventoryUI inventory;
    [SerializeField] private DialogueUI dialogue;
    [SerializeField] private QuestSystemUI questListUI;
    [SerializeField] private QuestSystem questSystem;
    private CinemachineInputAxisController mainCamera;

    void Awake()
    {
        manager = this;
        mainCamera = GameObject.FindGameObjectWithTag("FreeLookCamera").GetComponent<CinemachineInputAxisController>();
        questSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuestList>().GetQuestSystem;
        InMenu();
    }

    private void InMenu()
    {
        bool isInMenu = questUIOpen || inventoryUIOpen;
        Cursor.visible = isInMenu;
        Cursor.lockState = isInMenu ? CursorLockMode.None : CursorLockMode.Locked;
        mainCamera.enabled = !isInMenu;
    }

    #region Quests
    public void OnQuestTrigger(QuestLine questLine)
    {
        questSystem.AddQuestLine(questLine);
    }

    public void OnQuestListToggle()
    {
        questUIOpen = !questUIOpen;
        if (questUIOpen && inventoryUIOpen)
        {
            CloseInventory();
        }
        questListUI.gameObject.SetActive(questUIOpen);
        if (questUIOpen)
            questListUI.OnOpenQuestList(questSystem.GetQuests());

        InMenu();
    }

    public void ShowQuestDetails(QuestProgress questLine)
    {
        questListUI.ShowQuestLine(questLine);
    }
    #endregion

    #region Inventory
    public void OpenInventory(InventorySystem system)
    {
        if (questUIOpen)
        {
            OnQuestListToggle();
        }
        inventory.gameObject.SetActive(true);
        inventory.OnOpenInventory(system);
        inventoryUIOpen = true;
        InMenu();
    }

    public void ShowItemDetails(InventoryItemData item)
    {
        inventory.ShowItem(item);
    }

    public void CloseInventory()
    {
        inventory.OnCloseInventory();
        inventory.gameObject.SetActive(false);
        inventoryUIOpen = false;
        InMenu();
    }
    #endregion


}
