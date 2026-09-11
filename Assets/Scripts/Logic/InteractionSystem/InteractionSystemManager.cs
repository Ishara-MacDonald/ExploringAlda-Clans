using UnityEngine;

// Single entry point for resolving an interaction once Visual detects an Interact press.
public class InteractionSystemManager : MonoBehaviour
{
    public static InteractionSystemManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void Interact(Interactable interactable, GameObject interactor) => interactable.Interact(interactor);
}
