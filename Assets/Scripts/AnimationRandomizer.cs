using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomizer : MonoBehaviour
{
    [SerializeField] private int numberOfAnimations;
    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        RandomizeAnimation();
    }

    private void RandomizeAnimation()
    {
        anim.SetInteger("AnimNumber", Random.Range(1, numberOfAnimations + 1));
    }
}
