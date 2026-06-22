using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabber : MonoBehaviour
{
    private GameObject selectedObject = null;
    public static event Action OnLetGoItem;
    [SerializeField] private LayerMask draggableLayer;
    [SerializeField] private LayerMask putBackLayer;
    [SerializeField] private LayerMask isContainable;
    private Vector2 mousePosition;

    [SerializeField] private float longPressTime = 0.5f;
    private float lastPressedTime;
    private bool isPressed = false;
    private bool isLongPressed = false;

    public CraftingTableManager manager;

    public void Setup(CraftingTableManager tableManager)
    {
        manager = tableManager;
    }

    void Update()
    {
        if (selectedObject == null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                mousePosition = Mouse.current.position.ReadValue();
                isPressed = true;
                lastPressedTime = Time.time;
            }

            if (isPressed && !isLongPressed)
            {
                if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    if (Time.time - lastPressedTime < longPressTime) OnQuickPress();
                    isPressed = false;
                }
                else if (Time.time - lastPressedTime > longPressTime)
                {
                    isLongPressed = true;
                    isPressed = false;
                    OnLongPress();
                }
            }

            if (isLongPressed && Mouse.current.leftButton.wasReleasedThisFrame)
            {
                isLongPressed = false;
            }
        }
        else
        {
            if (Mouse.current.leftButton.wasPressedThisFrame) OnPressSelectedObject();
            else MoveSelectedObject();
        }
    }

    private void OnQuickPress()
    {
        RaycastHit hit = GetHit(draggableLayer);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Drag"))
            {
                selectedObject = hit.collider.gameObject;
                selectedObject.GetComponent<Collider>().enabled = false;
                Cursor.visible = false;
            }
            else if (hit.collider.CompareTag("Pestle"))
            {
                selectedObject = hit.collider.gameObject;
                Cursor.visible = false;
                manager.GrabPestle(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("CraftingGear"))
            {
                GameObject grabbed = manager.GrabCraftingGear(hit.collider.gameObject);
                if (grabbed != null)
                {
                    selectedObject = grabbed;
                }
            }
            return;
        }
    }

    private void OnLongPress()
    {
        RaycastHit hit = GetHit(draggableLayer);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("CraftingGear"))
            {
                if (manager.LongGrabCraftingGear(hit.collider.gameObject))
                    selectedObject = hit.collider.gameObject;
            }
        }
    }

    private void OnPressSelectedObject()
    {
        if (selectedObject.CompareTag("Pestle")) manager.LetPestleGo(selectedObject);
        else if (selectedObject.CompareTag("CraftingGear"))
        {
            RaycastHit hit = GetHit(putBackLayer);

            if (hit.collider != null) manager.PlaceGearDown(selectedObject, hit.collider);
            else manager.PutGearBack(selectedObject);
        }
        else
        {
            OnLetGoItem?.Invoke();
            selectedObject.GetComponent<Collider>().enabled = true;
        }

        selectedObject = null;
        Cursor.visible = true;
    }

    private void MoveSelectedObject()
    {
        mousePosition = Mouse.current.position.ReadValue();
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (selectedObject.CompareTag("CraftingGear")) manager.UseGear(selectedObject);
            return;
        }

        bool canMove = true;
        if (selectedObject.CompareTag("Pestle")) canMove = manager.CanMovePestle(selectedObject);

        if (canMove)
        {
            RaycastHit hit = GetHit(isContainable);

            Vector3 newPosition = hit.point;
            if (!selectedObject.CompareTag("CraftingGear"))
                newPosition.y += .25f;
            selectedObject.transform.position = newPosition;
        }
    }

    private RaycastHit GetHit(LayerMask layerMask)
    {
        Vector3 screenMousePosNear = new(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
        Vector3 screenMousePosFar = new(mousePosition.x, mousePosition.y, Camera.main.farClipPlane);

        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);

        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out RaycastHit hit, 100f, layerMask);

        return hit;
    }
}
