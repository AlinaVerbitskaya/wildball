using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WildBall.Inputs
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerAnimations))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float speed = 0.8f;
        [SerializeField, Range(0f, 10f)] private float rotationSpeed = 3f;
        [SerializeField, Range(0f, 10f)] private float maxVelocity = 3f;
        [SerializeField, Range(0f, 50f)] private float jumpForce = 20f;
        [SerializeField, Space] private GameManager GM;
        private Rigidbody playerRB;
        private PlayerAnimations playerAnims;
        private bool inAir = false;


        void Start()
        {
            playerRB = GetComponent<Rigidbody>();
            playerAnims = GetComponent<PlayerAnimations>();
        }

        public void PlayerMove(float vert, float horiz)
        {
            playerRB.AddRelativeForce(new Vector3(0, 0, vert) * speed, ForceMode.Force);
            transform.Rotate(0, horiz * rotationSpeed, 0);
            float horizSpeed = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z).magnitude;
            if ( horizSpeed > maxVelocity)
            {
                float adjustment = maxVelocity / playerRB.velocity.magnitude;
                playerRB.velocity = new Vector3(playerRB.velocity.x * adjustment, playerRB.velocity.y, playerRB.velocity.z * adjustment);
            }
            playerAnims.RotateRings(horizSpeed);
        }

        public IEnumerator Jump()
        {
            if (!inAir)
            {
                inAir = true;
                playerAnims.Jump();
                yield return new WaitForSeconds(0.15f);
                yield return new WaitForFixedUpdate();
                playerRB.AddForce(Vector3.up * jumpForce);
            }
        }

        /// <summary>
        /// Player death.
        /// </summary>
        public IEnumerator Death()
        {
            playerRB.isKinematic = true;
            playerAnims.PlayerDeath();
            yield return new WaitForSeconds(1f);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            GM.ShowDeathScreen();
            yield return new WaitUntil(() => Input.anyKeyDown);
            GameManager.RestartLevel();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DeathTrigger"))
            {
                StartCoroutine(Death());
            }
            else if (other.CompareTag("ActivationProximityTrigger")) 
            {
                other.gameObject.GetComponent<AnimatorActivator>().Activate(true);
            }
            else if (other.CompareTag("DoorTrigger"))
            {
                other.gameObject.GetComponent<InteractableDoor>().StartCoroutine("ActivateOnButtonPress");
            }
            else if (other.CompareTag("ActivationSwitchTrigger"))
            {
                other.gameObject.GetComponent<ActivationSwitch>().Toggle();
            }
            else if (other.CompareTag("Collectible"))
            {
                other.gameObject.GetComponent<Collectible>().Collect();
                EventManager.OnCrystalCollected?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ActivationProximityTrigger")) 
            {
                other.gameObject.GetComponent<AnimatorActivator>().Activate(false);
            }
            else if (other.CompareTag("DoorTrigger"))
            {
                InteractableDoor ID = other.gameObject.GetComponent<InteractableDoor>();
                ID.StopCoroutine("ActivateOnButtonPress");
                ID.ShowHint(false);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.layer == 8) //"ground"
            {
                inAir = false;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.layer == 8) //"ground"
            {
                inAir = true;
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Reset values")]
        private void ResetValues()
        {
            speed = 0.8f;
            jumpForce = 15f;
        }
#endif
    }
}
