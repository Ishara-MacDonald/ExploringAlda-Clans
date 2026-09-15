using UnityEngine;
using UnityEngine.InputSystem;

// Owns target detection, prompt UI, and Interact input; reports presses up to LogicManager.
public class InteractionVisualManager : SingletonManager<InteractionVisualManager>
{
    private InputAction interactAction;
    [SerializeField] private float raycastRadius;
    [SerializeField] private float distance = 10f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject interactPrompt;
    private bool canInteract = false;
    private bool shown = true;

    private string currentInteract;
    private Vector3 cameraForward;
    private Vector3 rayPosition;
    private readonly RaycastHit[] hitsBuffer = new RaycastHit[5];
    private Camera mainCamera;

    public void SetShown(bool newValue)
    {
        shown = newValue;
        interactPrompt.SetActive(newValue);
    }

    protected override void Awake()
    {
        base.Awake();
        interactAction = InputSystem.actions.FindAction("Interact");
        interactPrompt.SetActive(false);
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        interactAction.Enable();
        interactAction.started += OnInteract;
    }

    void Update()
    {
        if (canInteract != interactPrompt.activeSelf && shown)
            interactPrompt.SetActive(canInteract);
    }

    void FixedUpdate()
    {
        if (shown)
        {
            Interactable interactable = CheckInteraction();

            if (interactable != null)
            {
                if (!canInteract || (canInteract && !interactable.ActionLabel.Equals(currentInteract)))
                {
                    canInteract = true;
                    currentInteract = interactable.ActionLabel;
                    interactPrompt.TryGetComponent(out InteractPrompt prompt);
                    if (prompt == null) return;
                    prompt.SetAction(interactable.ActionLabel);
                }
            }
            else if (interactable == null && canInteract)
            {
                canInteract = false;
            }
        }
    }

    private void OnDisable()
    {
        interactAction.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Interactable interactable = CheckInteraction();
            if (interactable != null) VisualManager.manager.OnInteract(interactable);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.purple;
        Debug.DrawLine(rayPosition, rayPosition + cameraForward * distance);
        Gizmos.DrawWireSphere(rayPosition + cameraForward * distance, raycastRadius);
    }

    private Interactable CheckInteraction()
    {
        Vector3 tempCamera = mainCamera.transform.forward;
        tempCamera.y = 0;
        cameraForward = tempCamera.normalized;

        rayPosition = transform.position;

        int hitCount = Physics.SphereCastNonAlloc(rayPosition, raycastRadius, cameraForward, hitsBuffer, distance, layerMask, QueryTriggerInteraction.UseGlobal);

        Collider closestCollider = null;
        float closestDistance = float.MaxValue;
        for (int i = 0; i < hitCount; i++)
        {
            Collider collider = hitsBuffer[i].collider;
            if (collider == null || !collider.gameObject.CompareTag(Tags.Interactable)) continue;

            float sqrDistance = (collider.transform.position - rayPosition).sqrMagnitude;
            if (closestCollider != null && sqrDistance >= closestDistance) continue;

            closestCollider = collider;
            closestDistance = sqrDistance;
        }

        if (closestCollider != null && closestCollider.TryGetComponent(out Interactable interactable))
        {
            return interactable;
        }
        return null;
    }
}
