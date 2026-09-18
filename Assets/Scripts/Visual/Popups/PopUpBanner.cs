using TMPro;
using UnityEngine;

public class PopUpBanner : TimedPopup
{
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private TextMeshProUGUI subtitleTxt;

    public void SetPopUpBanner(QuestLine line)
    {
        titleTxt.SetText(line.QuestAchievement);
        subtitleTxt.SetText(line.QuestSubAchievement);

        StartHideTimer();
    }

    protected override void OnHide() => gameObject.SetActive(false);
}
