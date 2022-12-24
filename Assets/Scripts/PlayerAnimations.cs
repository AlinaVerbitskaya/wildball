using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WildBall.Inputs
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator anim;
        [SerializeField] ParticleSystem particlesDeathEffect;
        [SerializeField] GameObject[] rings;

        void Start()
        {
            anim = gameObject.GetComponent<Animator>();
        }

        public void Jump()
        {
            anim.SetBool("Jump", true);
        }

        public void Land()
        {
            anim.SetBool("Jump", false);
        }

        public void PlayerDeath()
        {
            anim.SetTrigger("Death");
            particlesDeathEffect.Play();
        }

        public void RotateRings(float speed)
        {
            foreach (GameObject ring in rings)
            {
                ring.transform.Rotate(speed, 0, 0);
            }
        }
    }
}