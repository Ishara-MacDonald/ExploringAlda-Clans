using UnityEngine;

// Logic-side per-system manager for Interaction. Single entry point for actually
// resolving an interaction once Visual has detected an Interact press against a target.
public class InteractionSystemManager : MonoBehaviour
{
    public static InteractionSystemManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void Interact(Interactable interactable, GameObject interactor) => interactable.Interact(interactor);
}
