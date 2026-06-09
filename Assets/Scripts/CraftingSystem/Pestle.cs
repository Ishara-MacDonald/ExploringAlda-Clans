using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pestle : MonoBehaviour
{
    private static readonly int IsGrindingHash = Animator.StringToHash("isGrinding");
    public static event Action Grinded;
    private bool isGrabbed = false;
    private bool isGrinding = false;

    public bool IsBeingUsed => isGrabbed || isGrinding;

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
        transform.rotation = Quaternion.Euler(-30, 0, 30);
        isGrabbed = true;
    }

    public void OnLetGo()
    {
        isGrabbed = false;
        if (!isGrinding)
            MoveOriginalSpot();
    }

    private IEnumerator Grinding(float grindTime)
    {
        isGrinding = true;
        animator.SetBool(IsGrindingHash, true);
        yield return new WaitForSeconds(grindTime);
        Grinded?.Invoke();
        isGrinding = false;
        animator.SetBool(IsGrindingHash, false);
        if (!isGrabbed) MoveOriginalSpot();
    }

    public void MoveOriginalSpot()
    {
        transform.tag = "Pestle";
        transform.parent = pestleLocation;
        transform.SetPositionAndRotation(pestleLocation.position, pestleLocation.rotation);
    }

    public void ToGrinder(Grinder grinder)
    {
        transform.parent = grinder.gameObject.transform;
        transform.SetPositionAndRotation(grinder.gameObject.transform.position, Quaternion.Euler(-30, 0, 30));
        transform.tag = "Untagged";
    }

    public bool CanMove()
    {
        return !isGrinding;
    }
}
