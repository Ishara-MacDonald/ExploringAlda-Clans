using TMPro;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionTxt;

    public void SetAction(string action)
    {
        actionTxt.SetText(action);
    }
}