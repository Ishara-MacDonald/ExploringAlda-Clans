using System;
using UnityEngine;

// Owns grab/drag gameplay rules: tag dispatch, drag position, use/place/put-back. Input lives in CraftingVisualManager.
public class Grabber : MonoBehaviour
{
    private GameObject selectedObject = null;
    private Pestle selectedPestle = null;
    private bool selectedIsGear = false;
    public static event Action OnLetGoItem;

    [SerializeField] private float dragHoverHeight = .25f;
    [SerializeField] private float gearHoverHeight = .125f;

    public bool HasSelection => selectedObject != null;

    public void TryQuickGrab(GameObject hit)
    {
        if (hit == null) return;

        if (hit.CompareTag(Tags.Drag))
        {
            selectedObject = hit;
            selectedObject.GetComponent<Collider>().enabled = false;
        }
        else if (hit.CompareTag(Tags.Pestle))
        {
            selectedObject = hit;
            selectedPestle = selectedObject.GetComponent<Pestle>();
            selectedIsGear = true;
            selectedPestle.OnGrab();
        }
        else if (hit.CompareTag(Tags.CraftingGear))
        {
            CraftingMaterial grabbed = hit.GetComponent<CraftingGear>().OnGrab();
            if (grabbed != null)
            {
                selectedObject = grabbed.gameObject;
                selectedIsGear = true;
            }
        }
    }

    public void TryLongGrab(GameObject hit)
    {
        if (hit == null) return;
        if (hit.CompareTag(Tags.CraftingGear))
        {
            if (hit.GetComponent<CraftingGear>().OnLongGrab())
            {
                selectedObject = hit;
                selectedIsGear = true;
            }
        }
    }

    public void ReleaseSelected(GameObject putBackHit)
    {
        if (selectedObject == null) return;

        if (selectedObject.CompareTag(Tags.Pestle)) selectedObject.GetComponent<Pestle>().OnLetGo();
        else if (selectedObject.CompareTag(Tags.CraftingGear))
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
        selectedPestle = null;
        selectedIsGear = false;
    }

    public void SecondaryAction()
    {
        if (selectedObject == null) return;
        if (selectedObject.CompareTag(Tags.CraftingGear)) selectedObject.GetComponent<CraftingGear>().OnUse();
        else if (selectedObject.CompareTag(Tags.Pestle)) selectedObject.GetComponent<Pestle>().TryStartGrinding();
    }

    public void Drag(Vector3 targetPosition)
    {
        if (selectedObject == null) return;

        bool canMove = selectedPestle == null || selectedPestle.CanMove();

        if (canMove)
        {
            targetPosition.y += selectedIsGear ? gearHoverHeight : dragHoverHeight;
            selectedObject.transform.position = targetPosition;
        }
    }
}
