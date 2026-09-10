using UnityEngine;

// Thin placeholder for the Quest system's Visual-side per-system manager.
// Not yet wired into the call chain — phase 2 will move QuestSystemUI's
// coordination logic here and have it talk to VisualManager.
public class QuestVisualManager : MonoBehaviour
{
    public static QuestVisualManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
