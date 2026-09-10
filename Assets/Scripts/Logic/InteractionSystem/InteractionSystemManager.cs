using UnityEngine;

// Thin placeholder for the Interaction system's Logic-side per-system manager.
// Not yet wired into the call chain — phase 2 will move Interactor's
// coordination logic here and have it talk to LogicManager.
public class InteractionSystemManager : MonoBehaviour
{
    public static InteractionSystemManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
