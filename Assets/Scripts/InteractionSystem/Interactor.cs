using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    private InputAction interactAction;
    [SerializeField] private float raycastRadius;
    [SerializeField] private float distance = 10f;
    [SerializeField] private LayerMask layerMask;

    void Awake()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void OnEnable()
    {
        interactAction.Enable();

        interactAction.started += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.Disable();
    }

    void Update()
    {
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started) CheckInteraction();
    }

    void OnDrawGizmosSelected()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward = cameraForward.normalized;

        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position + cameraForward * distance, raycastRadius);
    }

    private void CheckInteraction()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward = cameraForward.normalized;

        Vector3 rayPosition = transform.position;

        RaycastHit[] hits = new RaycastHit[5];

        Physics.SphereCastNonAlloc(rayPosition, raycastRadius, cameraForward, hits, distance, layerMask, QueryTriggerInteraction.UseGlobal);

        if (hits.Length != 0)
        {
            RaycastHit[] validHits = hits.Where(hit => hit.collider != null && hit.collider.gameObject.CompareTag("Interactable")).ToArray();
            if (validHits.Length == 0) return;

            RaycastHit[] sortedHits = validHits.OrderBy(hit => Vector3.Distance(transform.position, hit.collider.transform.position)).ToArray();

            if (sortedHits[0].collider.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
            {
                interactable.Interact(gameObject);
            }
        }
    }
}