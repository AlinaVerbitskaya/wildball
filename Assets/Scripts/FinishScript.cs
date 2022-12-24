using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody RB = collision.gameObject.GetComponent<Rigidbody>();
            RB.useGravity = false;
            RB.isKinematic = true;
            anim.SetTrigger("Collected");
        }
    }

    private void FinishReached()
    {
        EventManager.OnLevelFinished?.Invoke();
    }
}
