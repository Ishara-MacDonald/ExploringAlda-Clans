using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugTPPoint : MonoBehaviour
{
    [SerializeField] private TPPoint tpPointType;
    private InputAction tpAction;
    private GameObject player;

    void Awake()
    {
        string actionName = "TP " + tpPointType.ToString();
        tpAction = InputSystem.actions.FindAction(actionName);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        tpAction.Enable();
        tpAction.started += OnRespawn;
    }

    private void OnDisable()
    {
        tpAction.started -= OnRespawn;
        tpAction.Disable();
    }

    private void OnRespawn(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(Teleport(transform.position));
        }
    }

    IEnumerator Teleport(Vector3 newDestination)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        yield return null;
        player.transform.position = newDestination;
        yield return null;
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}