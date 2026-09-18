using TMPro;
using UnityEngine;

public class PopUpSmall : TimedPopup
{
    [SerializeField] private TextMeshProUGUI bannerTxt;

    public void SetPopUpBanner(string bannerText)
    {
        bannerTxt.SetText(bannerText);

        StartHideTimer();
    }

    protected override void OnHide() => Destroy(gameObject);
}
