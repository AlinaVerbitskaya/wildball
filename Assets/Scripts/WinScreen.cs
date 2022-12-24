using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particles;
    public void PlayParticles()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}
