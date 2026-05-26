using UnityEngine;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public static bool questUIOpen = false;
    public static bool inventoryUIOpen = false;
    public static bool craftingUIOpen = false;

    private CinemachineInputAxisController mainCamera;

    [SerializeField] private GameObject[] cameras;

    [SerializeField] private InventoryUI inventory;
    [SerializeField] private DialogueUI dialogue;
    [SerializeField] private QuestSystemUI questListUI;
    [SerializeField] private CraftingSystemUI craftingUI;

    [SerializeField] private QuestSystem questSystem;
    [SerializeField] private PopUpBanner popupBanner;
    private CraftingSystem craftingSystem;
    private GameObject player;
    private GameObject playerCam;
    private GameObject tempCam;

    void Awake()
    {
        manager = this;
        mainCamera = GameObject.FindGameObjectWithTag("FreeLookCamera").GetComponent<CinemachineInputAxisController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = GameObject.FindGameObjectWithTag("FreeLookCamera");
        questSystem = player.GetComponent<PlayerQuestList>().GetQuestSystem;
        craftingSystem = new();
        popupBanner.gameObject.SetActive(false);
        InMenu();
    }

    private void InMenu()
    {
        bool isInMenu = questUIOpen || inventoryUIOpen || craftingUIOpen;
        Cursor.visible = isInMenu;
        Cursor.lockState = isInMenu ? CursorLockMode.None : CursorLockMode.Locked;
        mainCamera.enabled = !isInMenu;

        player.GetComponent<PlayerMovement>().SetMovementEnabled(!isInMenu);
    }

    #region PopUpBanner
    public void QuestLineCompleted(QuestProgress progress)
    {
        questSystem.RemoveQuestLine(progress);
        popupBanner.gameObject.SetActive(true);
        popupBanner.SetPopUpBanner(progress.QuestLine);
    }
    #endregion

    #region Quests
    public void OnQuestTrigger(QuestLine questLine)
    {
        questSystem.AddQuestLine(questLine);
        OnQuestListToggle();
    }

    public void OnQuestListToggle()
    {
        if (craftingUIOpen) return;
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
        if (craftingUIOpen) return;
        if (questUIOpen)
        {
            OnQuestListToggle();
        }
        inventory.gameObject.SetActive(true);
        inventory.OnOpenInventory(system);
        inventoryUIOpen = true;
        InMenu();
    }

    public void CloseInventory()
    {
        if (craftingUIOpen) return;
        inventory.OnCloseInventory();
        inventory.gameObject.SetActive(false);
        inventoryUIOpen = false;
        InMenu();
    }
    #endregion

    #region Crafting

    public void OnCraftingToggle(CinemachineClearShot craftingCam)
    {
        if (!craftingUIOpen)
        {
            OnCraftingOpen(craftingCam);
        }
        else
        {
            OnCraftingClose();
        }
    }

    private void OnCraftingOpen(CinemachineClearShot craftingCam)
    {
        tempCam = craftingCam.gameObject;
        craftingUIOpen = true;
        player.GetComponent<Interactor>().SetShown(false);
        playerCam.GetComponent<CinemachineVirtualCameraBase>().Priority = 1;
        craftingCam.Priority = 90;
        craftingUI.gameObject.SetActive(true);
        if (inventoryUIOpen) CloseInventory();
        if (questUIOpen) OnQuestListToggle();
        craftingUI.OnOpenCraftingSystem(player.GetComponent<InventorySystem>());
        InMenu();
    }

    public void OnCraftingClose()
    {
        craftingUIOpen = false;
        player.GetComponent<Interactor>().SetShown(true);
        playerCam.GetComponent<CinemachineVirtualCameraBase>().Priority = 90;
        tempCam.GetComponent<CinemachineClearShot>().Priority = 1;
        InMenu();
    }

    #endregion

}
