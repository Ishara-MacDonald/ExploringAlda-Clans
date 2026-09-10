using UnityEngine;

// Thin placeholder for the Interaction system's Visual-side per-system manager.
// Not yet wired into the call chain — phase 2 will move InteractPrompt/
// InteractionSign coordination logic here and have it talk to VisualManager.
public class InteractionVisualManager : MonoBehaviour
{
    public static InteractionVisualManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
