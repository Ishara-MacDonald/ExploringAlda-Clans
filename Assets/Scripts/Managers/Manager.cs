using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
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
        InMenu(false);
    }

    private void InMenu(bool isInMenu)
    {
        Cursor.visible = isInMenu;
        Cursor.lockState = isInMenu ? CursorLockMode.None : CursorLockMode.Locked;
        mainCamera.enabled = !isInMenu;
    }

    #region Quests
    public void OnQuestTrigger(QuestLine questLine)
    {
        questSystem.AddQuestLine(questLine);
    }
    public void ToggleQuestList()
    {
        bool isQuestActive = questListUI.gameObject.activeSelf;
        questListUI.gameObject.SetActive(true);
        questListUI.OnOpenQuestList(questSystem.GetQuests());
        InMenu(isQuestActive);
    }

    public void OpenQuestList(QuestSystem questSystem)
    {
        questListUI.gameObject.SetActive(true);
        if (inventory.gameObject.activeSelf) inventory.gameObject.SetActive(false);
        questListUI.OnOpenQuestList(questSystem.GetQuests());
        InMenu(true);
    }

    public void CloseQuestList()
    {
        questListUI.gameObject.SetActive(false);
        InMenu(false);
    }


    public void ShowQuestDetails(QuestProgress questLine)
    {
        questListUI.ShowQuestLine(questLine);
    }
    #endregion

    #region Inventory
    public void OpenInventory(InventorySystem system)
    {
        if (questListUI.gameObject.activeSelf) questListUI.gameObject.SetActive(false);
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
    #endregion


}
