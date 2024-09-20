using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpinningWheel wheel;

    public void activate()
    {
        animator.SetTrigger("activate");
        wheel.Spin();
    }
}
