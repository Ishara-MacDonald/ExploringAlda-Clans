using UnityEngine;

// Thin placeholder for the Inventory system's Visual-side per-system manager.
// Not yet wired into the call chain — phase 2 will move InventoryUI's
// coordination logic here and have it talk to VisualManager.
public class InventoryVisualManager : MonoBehaviour
{
    public static InventoryVisualManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
