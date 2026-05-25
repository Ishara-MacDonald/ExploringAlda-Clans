using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pestle : MonoBehaviour
{
    private static readonly int IsGrindingHash = Animator.StringToHash("isGrinding");
    private static readonly int IsGrabbingHash = Animator.StringToHash("isGrabbing");
    public static event Action Grinded;
    private bool isGrabbed = false;
    private bool isGrinding = false;

    private IEnumerator grindCoroutine;


    [SerializeField] private Animator animator;
    [SerializeField] private float grindTime = 5f;
    [SerializeField] private Transform pestleLocation;
    void Start()
    {
        grindCoroutine = Grinding(5f);
    }

    void Update()
    {
        if (isGrabbed)
        {
            if (!isGrinding && Mouse.current.rightButton.wasPressedThisFrame)
            {
                Debug.Log("start grinding");
                grindCoroutine = Grinding(grindTime);
                StartCoroutine(grindCoroutine);
            }
        }
    }

    public void OnGrab()
    {
        animator.SetBool(IsGrabbingHash, true);
        isGrabbed = true;
    }

    public void OnLetGo()
    {
        isGrabbed = false;
        animator.SetBool(IsGrabbingHash, false);
        if (!isGrinding)
            PutBack();
    }

    private IEnumerator Grinding(float grindTime)
    {
        isGrinding = true;
        animator.SetBool(IsGrindingHash, true);
        yield return new WaitForSeconds(grindTime);
        Grinded?.Invoke();
        isGrinding = false;
        animator.SetBool(IsGrindingHash, false);
        if (!isGrabbed) PutBack();
    }

    private void PutBack()
    {
        transform.SetPositionAndRotation(pestleLocation.position, pestleLocation.rotation);
    }

    public bool CanMove()
    {
        return !isGrinding;
    }
}
