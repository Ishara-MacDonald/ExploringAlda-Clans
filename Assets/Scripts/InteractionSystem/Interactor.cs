using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
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

    public void SetShown(bool newValue)
    {
        shown = newValue;
        interactPrompt.SetActive(newValue);
    }

    void Awake()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        interactPrompt.SetActive(false);
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

            if (interactable)
            {
                if (!canInteract || (canInteract && !interactable.GetAction.Equals(currentInteract)))
                {
                    canInteract = true;
                    currentInteract = interactable.GetAction;
                    interactPrompt.TryGetComponent(out InteractPrompt prompt);
                    if (prompt == null) return;
                    prompt.SetAction(interactable.GetAction);
                }
            }
            else if (!interactable && canInteract)
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
            if (interactable) interactable.Interact(gameObject);
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
        Vector3 tempCamera = Camera.main.transform.forward;
        tempCamera.y = 0;
        cameraForward = tempCamera.normalized;

        rayPosition = transform.position;

        RaycastHit[] hits = new RaycastHit[5];

        Physics.SphereCastNonAlloc(rayPosition, raycastRadius, cameraForward, hits, distance, layerMask, QueryTriggerInteraction.UseGlobal);

        if (hits.Length != 0)
        {
            RaycastHit[] validHits = hits.Where(hit => hit.collider != null && hit.collider.gameObject.CompareTag("Interactable")).ToArray();
            if (validHits.Length == 0) return null;

            RaycastHit[] sortedHits = validHits.OrderBy(hit => Vector3.Distance(transform.position, hit.collider.transform.position)).ToArray();

            if (sortedHits[0].collider.gameObject.TryGetComponent(out Interactable interactable))
            {
                return interactable;
            }
        }
        return null;
    }
}