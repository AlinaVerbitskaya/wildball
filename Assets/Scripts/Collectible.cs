using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField, Header("Particle effect when collected")] 
    private ParticleSystem collectedEffect;

    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Collect()
    {
        anim.SetTrigger("Collected");
        collectedEffect.Play();
        Destroy(gameObject, 0.3f);
    }
}
