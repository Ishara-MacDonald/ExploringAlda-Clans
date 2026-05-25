using UnityEngine;

public class PlotStateContext : MonoBehaviour
{
    private bool isGrowing = false;
    [SerializeField] private GameObject growingGrass;

    void Awake()
    {
        growingGrass.SetActive(isGrowing);
    }

    public void OnInteraction()
    {
        if (!isGrowing) isGrowing = true;
        UpdatePlot();
    }

    private void UpdatePlot()
    {
        growingGrass.SetActive(isGrowing);
    }
}
