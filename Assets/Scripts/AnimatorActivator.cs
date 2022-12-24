using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorActivator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Activate(bool act)
    {
        animator.SetBool("Activated", act);
    }
}
