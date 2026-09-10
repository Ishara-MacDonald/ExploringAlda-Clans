using UnityEngine;

// Thin placeholder for the Player's Logic-side per-system manager.
// Not yet wired into the call chain — phase 2 will move player coordination
// logic here (and split input-reading out of PlayerMovement/PlayerInventory/
// PlayerQuestList) and have it talk to LogicManager.
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
