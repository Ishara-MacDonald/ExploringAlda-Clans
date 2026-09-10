using System.Collections;
using TMPro;
using UnityEngine;

public class PopUpSmall : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bannerTxt;
    public void SetPopUpBanner(string bannerText)
    {
        bannerTxt.SetText(bannerText);

        StartCoroutine(WaitAndHide(5f));
    }

    IEnumerator WaitAndHide(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
