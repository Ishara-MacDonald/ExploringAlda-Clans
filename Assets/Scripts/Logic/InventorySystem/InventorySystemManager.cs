using UnityEngine;

// Thin placeholder for the Inventory system's Logic-side per-system manager.
// Not yet wired into the call chain — phase 2 will move InventorySystem's
// coordination logic here and have it talk to LogicManager.
public class InventorySystemManager : MonoBehaviour
{
    public static InventorySystemManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
