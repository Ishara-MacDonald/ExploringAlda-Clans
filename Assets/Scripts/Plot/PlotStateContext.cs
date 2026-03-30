
using UnityEngine;

public class PlotStateContext : MonoBehaviour
{

    void Awake()
    {
    }

    void Update()
    {
    }


    public PlotStateContext()
    {

    }

    public string GetInteractionName()
    {
        return "hi";
    }

    public void OnInteraction()
    {
        Debug.Log("Set Next State");
        // stateMachine.SetNextState();
    }

}
