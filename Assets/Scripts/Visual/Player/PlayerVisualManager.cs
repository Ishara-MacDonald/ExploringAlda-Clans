using UnityEngine;

// Thin placeholder for the Player's Visual-side per-system manager.
// Not yet wired into the call chain — phase 2 will receive the input-driven
// visual behavior split out of PlayerMovement/PlayerInventory/PlayerQuestList
// and talk to VisualManager.
public class PlayerVisualManager : MonoBehaviour
{
    public static PlayerVisualManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
