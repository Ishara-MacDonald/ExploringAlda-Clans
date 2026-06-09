using System;
using UnityEngine;

public enum GearType
{
    mortarPestle
}

public abstract class CraftingGear : MonoBehaviour
{
    public static event Action<CraftingGear> OnUseGear;

    public void MoveOriginalSpot(GearType gearType, Transform originalSpot)
    {
        Debug.Log("hi");
        Debug.Log(originalSpot);
        switch (gearType)
        {
            case GearType.mortarPestle:
                transform.parent = originalSpot;
                break;
        }
    }

    public void InvokeOnUse(GearType type)
    {
        OnUseGear?.Invoke(this);
    }

    public abstract bool OnGrab();
    public abstract void OnUse();
    public abstract void OnMove(GameObject locationObj);
    public abstract void OnPutBack();
}
