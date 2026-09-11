using System;
using UnityEngine;

// Owns grab/drag gameplay rules: tag dispatch, drag position, use/place/put-back. Input lives in CraftingVisualManager.
public class Grabber : MonoBehaviour
{
    private GameObject selectedObject = null;
    public static event Action OnLetGoItem;

    [SerializeField] private float dragHoverHeight = .25f;
    [SerializeField] private float gearHoverHeight = .125f;

    public bool HasSelection => selectedObject != null;

    public void TryQuickGrab(GameObject hit)
    {
        if (hit == null) return;

        if (hit.CompareTag("Drag"))
        {
            selectedObject = hit;
            selectedObject.GetComponent<Collider>().enabled = false;
        }
        else if (hit.CompareTag("Pestle"))
        {
            selectedObject = hit;
            selectedObject.GetComponent<Pestle>().OnGrab();
        }
        else if (hit.CompareTag("CraftingGear"))
        {
            CraftingMaterial grabbed = hit.GetComponent<CraftingGear>().OnGrab();
            if (grabbed != null)
            {
                selectedObject = grabbed.gameObject;
            }
        }
    }

    public void TryLongGrab(GameObject hit)
    {
        if (hit == null) return;
        if (hit.CompareTag("CraftingGear"))
        {
            if (hit.GetComponent<CraftingGear>().OnLongGrab())
                selectedObject = hit;
        }
    }

    public void ReleaseSelected(GameObject putBackHit)
    {
        if (selectedObject == null) return;

        if (selectedObject.CompareTag("Pestle")) selectedObject.GetComponent<Pestle>().OnLetGo();
        else if (selectedObject.CompareTag("CraftingGear"))
        {
            if (putBackHit != null) selectedObject.GetComponent<CraftingGear>().OnPlaceDown(putBackHit);
            else selectedObject.GetComponent<CraftingGear>().OnPutBack();
        }
        else
        {
            OnLetGoItem?.Invoke();
            selectedObject.GetComponent<Collider>().enabled = true;
        }

        selectedObject = null;
    }

    public void SecondaryAction()
    {
        if (selectedObject == null) return;
        if (selectedObject.CompareTag("CraftingGear")) selectedObject.GetComponent<CraftingGear>().OnUse();
        else if (selectedObject.CompareTag("Pestle")) selectedObject.GetComponent<Pestle>().TryStartGrinding();
    }

    public void Drag(Vector3 targetPosition)
    {
        if (selectedObject == null) return;

        bool canMove = true;
        if (selectedObject.CompareTag("Pestle")) canMove = selectedObject.GetComponent<Pestle>().CanMove();

        if (canMove)
        {
            if (selectedObject.CompareTag("Pestle") || selectedObject.CompareTag("CraftingGear")) targetPosition.y += gearHoverHeight;
            else targetPosition.y += dragHoverHeight;
            selectedObject.transform.position = targetPosition;
        }
    }
}
