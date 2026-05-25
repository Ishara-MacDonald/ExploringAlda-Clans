using System.Collections;
using TMPro;
using UnityEngine;

public class PopUpSmall : MonoBehaviour
{
    private IEnumerator coroutine;

    [SerializeField] private TextMeshProUGUI bannerTxt;
    public void SetPopUpBanner(string bannerText)
    {
        bannerTxt.SetText(bannerText);
        coroutine = WaitAndHide(5f);

        StartCoroutine(coroutine);
    }

    IEnumerator WaitAndHide(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
