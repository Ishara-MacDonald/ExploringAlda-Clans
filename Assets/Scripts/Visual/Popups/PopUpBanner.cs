using System.Collections;
using TMPro;
using UnityEngine;

public class PopUpBanner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private TextMeshProUGUI subtitleTxt;
    public void SetPopUpBanner(QuestLine line)
    {
        titleTxt.SetText(line.QuestAchievement);
        subtitleTxt.SetText(line.QuestSubAchievement);

        StartCoroutine(WaitAndHide(5f));
    }

    IEnumerator WaitAndHide(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
