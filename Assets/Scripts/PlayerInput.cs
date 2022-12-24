using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WildBall.Inputs
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour
    {
        private PlayerMovement playerMv;
        private float movementVert, movementHor;

        void Start()
        {
            playerMv = GetComponent<PlayerMovement>();
        }

        void Update()
        {
            InputsCheck();
        }

        private void FixedUpdate()
        {
            playerMv.PlayerMove(movementVert, movementHor);
        }

        private void InputsCheck()
        {
            if (Input.GetButtonDown(GlobalStringVars.JUMP_BUTTON))
            {
                StartCoroutine(playerMv.Jump());
            }
            movementVert = Input.GetAxis(GlobalStringVars.VERTICAL_AXIS);
            movementHor = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
        }
    }
}