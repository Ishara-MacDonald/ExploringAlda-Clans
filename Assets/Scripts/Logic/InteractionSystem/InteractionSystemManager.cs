using UnityEngine;

// Single entry point for resolving an interaction once Visual detects an Interact press.
public class InteractionSystemManager : SingletonManager<InteractionSystemManager>
{
    public void Interact(Interactable interactable, GameObject interactor) => interactable.Interact(interactor);
}
