using UnityEngine;

// Thin placeholder for the Crafting system's Visual-side per-system manager.
// Not yet wired into the call chain — phase 2 will move CraftingSystemUI's
// coordination logic here and have it talk to VisualManager.
public class CraftingVisualManager : MonoBehaviour
{
    public static CraftingVisualManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
