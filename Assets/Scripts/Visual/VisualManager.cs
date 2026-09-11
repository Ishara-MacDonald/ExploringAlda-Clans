using Unity.Cinemachine;
using UnityEngine;

// Visual-side top hub. Owns UI panel refs, camera refs, and all "is a menu open" state.
// Only Visual-side class allowed to call into LogicManager.
public class VisualManager : MonoBehaviour
{
    public static VisualManager manager;
    public static bool questUIOpen = false;
    public static bool inventoryUIOpen = false;
    public static bool craftingUIOpen = false;

    private CinemachineInputAxisController mainCamera;

    [SerializeField] private InventoryUI inventory;
    [SerializeField] private QuestSystemUI questListUI;
    [SerializeField] private CraftingSystemUI craftingUI;
    [SerializeField] private PopUpBanner popupBanner;
    [SerializeField] private CraftingVisualManager craftingVisualManager;

    private GameObject player;
    private GameObject playerCam;
    private GameObject tempCam;
    private InteractionVisualManager interactionVisualManager;

    void Awake()
    {
        manager = this;
        mainCamera = GameObject.FindGameObjectWithTag("FreeLookCamera").GetComponent<CinemachineInputAxisController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = GameObject.FindGameObjectWithTag("FreeLookCamera");
        interactionVisualManager = player.GetComponent<InteractionVisualManager>();
        popupBanner.gameObject.SetActive(false);
    }

    void Start()
    {
        // Deferred to Start: InMenu() calls into LogicManager, and Unity doesn't guarantee
        // Awake() order across different components — LogicManager.manager could still be
        // null if this ran from Awake(). All Awake() calls are guaranteed to finish before
        // any Start() call, so this is safe here.
        InMenu();
    }

    private void InMenu()
    {
        bool isInMenu = questUIOpen || inventoryUIOpen || craftingUIOpen;
        Cursor.visible = isInMenu;
        Cursor.lockState = isInMenu ? CursorLockMode.None : CursorLockMode.Locked;
        mainCamera.enabled = !isInMenu;
        LogicManager.manager.SetMovementEnabled(!isInMenu);
    }

    public void ShowCompletedBanner(QuestLine questLine)
    {
        popupBanner.gameObject.SetActive(true);
        popupBanner.SetPopUpBanner(questLine);
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
            questListUI.OnOpenQuestList(LogicManager.manager.GetQuests());

        InMenu();
    }

    public void ShowQuestDetails(QuestProgress questLine)
    {
        questListUI.ShowQuestLine(questLine);
    }

    public void SetPlayerMoveInput(Vector2 moveInput) => LogicManager.manager.SetPlayerMoveInput(moveInput);
    public void SetPlayerSprintEnabled(bool enabled) => LogicManager.manager.SetPlayerSprintEnabled(enabled);
    public void TryPlayerJump() => LogicManager.manager.TryPlayerJump();
    public void OnInteract(Interactable interactable) => LogicManager.manager.OnInteract(interactable);

    public bool HasCraftingSelection() => LogicManager.manager.HasCraftingSelection();
    public void OnCraftingQuickGrab(GameObject hit) => LogicManager.manager.OnCraftingQuickGrab(hit);
    public void OnCraftingLongGrab(GameObject hit) => LogicManager.manager.OnCraftingLongGrab(hit);
    public void OnCraftingRelease(GameObject putBackHit) => LogicManager.manager.OnCraftingRelease(putBackHit);
    public void OnCraftingDrag(Vector3 targetPosition) => LogicManager.manager.OnCraftingDrag(targetPosition);
    public void OnCraftingSecondaryAction() => LogicManager.manager.OnCraftingSecondaryAction();

    public void OnInventoryToggle()
    {
        if (craftingUIOpen) return;
        if (inventoryUIOpen) CloseInventory();
        else OpenInventory();
    }

    private void OpenInventory()
    {
        if (craftingUIOpen) return;
        if (questUIOpen)
        {
            OnQuestListToggle();
        }
        inventory.gameObject.SetActive(true);
        inventory.OnOpenInventory(LogicManager.manager.GetPlayerInventorySystem());
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

    public void OnAddProcessItem(ItemDataSO item) { craftingUI.AddProcessedItem(item); }
    public void OnRemoveItem(ItemDataSO item) { craftingUI.RemoveItem(item); }
    public void OnCraftingItemsStaged() => craftingUI.RefreshInventoryDisplay();

    public void OnCraftingItemClicked(ItemDataSO item, int amount) => LogicManager.manager.OnCraftingItemClicked(item, amount);
    public void OnCraftingReset() => LogicManager.manager.OnCraftingReset();
    public void OnCraftingTableClosed() => LogicManager.manager.OnCraftingTableClosed();
    public int GetCraftingStagedAmount(ItemDataSO item) => LogicManager.manager.GetCraftingStagedAmount(item);

    private void OnCraftingOpen(CinemachineClearShot craftingCam)
    {
        tempCam = craftingCam.gameObject;
        craftingUIOpen = true;
        interactionVisualManager.SetShown(false);
        craftingVisualManager.SetActive(true);
        playerCam.GetComponent<CinemachineVirtualCameraBase>().Priority = 1;
        craftingCam.Priority = 90;
        craftingUI.gameObject.SetActive(true);
        if (inventoryUIOpen) CloseInventory();
        if (questUIOpen) OnQuestListToggle();
        craftingUI.OnOpenCraftingSystem(LogicManager.manager.GetPlayerInventorySystem());
        InMenu();
    }

    public void OnCraftingClose()
    {
        craftingUIOpen = false;
        interactionVisualManager.SetShown(true);
        craftingVisualManager.SetActive(false);
        playerCam.GetComponent<CinemachineVirtualCameraBase>().Priority = 90;
        tempCam.GetComponent<CinemachineClearShot>().Priority = 1;
        InMenu();
    }
}
