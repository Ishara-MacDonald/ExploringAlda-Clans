using UnityEngine;

public enum GearType
{
    mortarPestle
}

public class CraftingGear : MonoBehaviour
{
    [SerializeField] private Transform locationMortarPestle;

    public void PutBack(GearType gearType, GameObject gameObject)
    {
        switch (gearType)
        {
            case GearType.mortarPestle:
                gameObject.transform.parent = locationMortarPestle;
                break;
        }
    }
}
