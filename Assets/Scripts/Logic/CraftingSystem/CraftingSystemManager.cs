using UnityEngine;

// Thin placeholder for the Crafting system's Logic-side per-system manager.
// Not yet wired into the call chain — phase 2 will move CraftingSystem's
// coordination logic here and have it talk to LogicManager.
public class CraftingSystemManager : MonoBehaviour
{
    public static CraftingSystemManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
