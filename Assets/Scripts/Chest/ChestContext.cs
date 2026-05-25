using UnityEngine;

public class ChestContext : MonoBehaviour
{
    private bool isOpen = false;
    [SerializeField] private GameObject chestLid;

    void Awake()
    {
        chestLid.SetActive(isOpen);
    }

    public void OnInteraction()
    {
        isOpen = !isOpen;
        UpdateChest();
    }

    private void UpdateChest()
    {
        chestLid.SetActive(isOpen);
    }
}
