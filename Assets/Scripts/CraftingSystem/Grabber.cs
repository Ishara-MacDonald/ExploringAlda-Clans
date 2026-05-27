using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabber : MonoBehaviour
{
    private GameObject selectedObject = null;
    [SerializeField] private LayerMask draggableLayer;
    [SerializeField] private LayerMask putBackLayer;
    [SerializeField] private LayerMask isContainable;
    public Vector2 mousePosition;
    [SerializeField] private GameObject test;

    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (selectedObject == null)
            {
                RaycastHit hit = GetHit(draggableLayer);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Drag"))
                    {
                        selectedObject = hit.collider.gameObject;
                        Cursor.visible = false;
                    }
                    else if (hit.collider.CompareTag("Pestle"))
                    {
                        selectedObject = hit.collider.gameObject;
                        Cursor.visible = false;
                        selectedObject.GetComponent<Pestle>().OnGrab();
                    }
                    else if (hit.collider.CompareTag("Mortar"))
                    {
                        if (hit.collider.GetComponent<Grinder>().OnGrab())
                        {
                            selectedObject = hit.collider.gameObject;
                            // Cursor.visible = false;
                        }
                    }
                    return;
                }
            }
            else
            {
                if (selectedObject.CompareTag("Pestle")) selectedObject.GetComponent<Pestle>().OnLetGo();
                else if (selectedObject.CompareTag("Mortar"))
                {
                    RaycastHit hit = GetHit(putBackLayer);
                    if (hit.collider != null)
                    {
                        selectedObject.GetComponent<Grinder>().OnSetUse(hit.collider.gameObject);
                    }
                    else
                    {
                        selectedObject.GetComponent<Grinder>().PutBackEmpty();
                    }
                }
                selectedObject = null;
                Cursor.visible = true;
            }
        }

        if (selectedObject != null)
        {
            bool canMove = true;
            if (selectedObject.CompareTag("Pestle"))
                canMove = selectedObject.GetComponent<Pestle>().CanMove();

            if (canMove)
            {
                RaycastHit hit = GetHit(isContainable);

                Vector3 newPosition = hit.point;
                if (!selectedObject.CompareTag("Mortar"))
                    newPosition.y += .25f;
                selectedObject.transform.position = newPosition;
            }
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
