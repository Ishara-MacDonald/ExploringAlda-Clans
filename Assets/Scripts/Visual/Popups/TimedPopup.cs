using System.Collections;
using UnityEngine;

public abstract class TimedPopup : MonoBehaviour
{
    [SerializeField] protected float displayDuration = 5f;

    protected void StartHideTimer() => StartCoroutine(WaitAndHide());

    private IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(displayDuration);
        OnHide();
    }

    protected abstract void OnHide();
}
