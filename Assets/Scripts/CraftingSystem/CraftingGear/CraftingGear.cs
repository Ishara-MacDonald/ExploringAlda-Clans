using UnityEngine;

public abstract class CraftingGear : MonoBehaviour
{
    public void MoveOriginalSpot(CraftingMethod gearType, Transform originalSpot)
    {
        switch (gearType)
        {
            case CraftingMethod.Grinding:
                transform.parent = originalSpot;
                break;
        }
    }

    public abstract CraftingMaterial OnGrab();
    public abstract bool OnLongGrab();
    public abstract void OnUse();
    public abstract void OnPlaceDown(GameObject locationObj);
    public abstract void OnPutBack();
}
