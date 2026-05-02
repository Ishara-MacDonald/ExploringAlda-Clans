using UnityEngine;

public class InteractionSign : MonoBehaviour
{
    [SerializeField] private GameObject visual;

    void Start()
    {
        visual.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
            visual.SetActive(true);
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
            visual.SetActive(false);
    }
}