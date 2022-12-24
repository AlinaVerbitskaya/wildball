using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 offsetPos, cameraRotOffset;
    Quaternion rotY;

    private void Start()
    {
        offsetPos = playerTransform.position - gameObject.transform.position;
        cameraRotOffset = new Vector3(0, 0.35f, 0);
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    /// <summary>
    /// Moves camera to follow the player.
    /// </summary>
    private void MoveCamera()
    {
        rotY = Quaternion.Euler(0, playerTransform.rotation.eulerAngles.y, 0);
        gameObject.transform.position = playerTransform.position - (rotY * offsetPos);
        gameObject.transform.LookAt(playerTransform.position + cameraRotOffset);
    }
}
