using UnityEngine;
using UnityEngine.InputSystem;

// Visual-side per-system manager for crafting-table dragging. Owns all mouse input
// reading and camera/mouse raycasting for grab/drag/release/use gestures (aiming is
// camera-driven — same reasoning as InteractionVisualManager), plus cursor visibility
// while dragging. Tag dispatch and gameplay state (what's selected, whether it can
// move, what a gesture means) stay in Logic's Grabber; this only reports discrete/
// continuous gesture events up through VisualManager -> LogicManager.
public class CraftingVisualManager : MonoBehaviour
{
    public static CraftingVisualManager Instance;

    [SerializeField] private LayerMask draggableLayer;
    [SerializeField] private LayerMask putBackLayer;
    [SerializeField] private LayerMask isContainable;
    [SerializeField] private float longPressTime = 0.5f;

    private bool active = false;
    private float lastPressedTime;
    private bool isPressed = false;
    private bool isLongPressed = false;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        InventorySlotUI.ItemInventoryInteracted += OnItemClicked;
    }

    void OnDisable()
    {
        InventorySlotUI.ItemInventoryInteracted -= OnItemClicked;
    }

    private void OnItemClicked(ItemDataSO item, int amount)
    {
        if (!active) return;
        VisualManager.manager.OnCraftingItemClicked(item, amount);
    }

    // Called by VisualManager when the crafting table opens/closes — replaces the old
    // Grabber.enabled toggle from CraftingTable.ToggleCollisions. Deliberately does not
    // touch Cursor.visible: VisualManager.InMenu() (called right after this, same frame)
    // is the sole authority for menu-driven cursor visibility.
    public void SetActive(bool newValue)
    {
        active = newValue;
        isPressed = false;
        isLongPressed = false;
    }

    void Update()
    {
        if (!active) return;

        if (!VisualManager.manager.HasCraftingSelection())
        {
            HandleSelectionInput();
        }
        else if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit = GetHit(putBackLayer);
            VisualManager.manager.OnCraftingRelease(hit.collider != null ? hit.collider.gameObject : null);
            UpdateCursor();
        }
        else if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            VisualManager.manager.OnCraftingSecondaryAction();
        }
        else
        {
            RaycastHit hit = GetHit(isContainable);
            VisualManager.manager.OnCraftingDrag(hit.point);
        }
    }

    private void HandleSelectionInput()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isPressed = true;
            lastPressedTime = Time.time;
        }

        if (isPressed && !isLongPressed)
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                if (Time.time - lastPressedTime < longPressTime) TryQuickGrab();
                isPressed = false;
            }
            else if (Time.time - lastPressedTime > longPressTime)
            {
                isLongPressed = true;
                isPressed = false;
                TryLongGrab();
            }
        }

        if (isLongPressed && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isLongPressed = false;
        }
    }

    private void TryQuickGrab()
    {
        RaycastHit hit = GetHit(draggableLayer);
        VisualManager.manager.OnCraftingQuickGrab(hit.collider != null ? hit.collider.gameObject : null);
        UpdateCursor();
    }

    private void TryLongGrab()
    {
        RaycastHit hit = GetHit(draggableLayer);
        VisualManager.manager.OnCraftingLongGrab(hit.collider != null ? hit.collider.gameObject : null);
        UpdateCursor();
    }

    private void UpdateCursor()
    {
        Cursor.visible = !VisualManager.manager.HasCraftingSelection();
    }

    private RaycastHit GetHit(LayerMask layerMask)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 screenMousePosNear = new(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
        Vector3 screenMousePosFar = new(mousePosition.x, mousePosition.y, Camera.main.farClipPlane);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out RaycastHit hit, 100f, layerMask);
        return hit;
    }
}
