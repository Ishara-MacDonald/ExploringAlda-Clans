using UnityEngine;

// Thin placeholder for the Quest system's Logic-side per-system manager.
// Not yet wired into the call chain — phase 2 will move QuestSystem's
// coordination logic here and have it talk to LogicManager.
public class QuestSystemManager : MonoBehaviour
{
    public static QuestSystemManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
