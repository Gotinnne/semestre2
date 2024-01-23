using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CameraController : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    void Start()
    {
        // bloquer et cacher le curseur
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        // récupération des mouvements de la souris
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // tourner la caméra dans l'axe X
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        // tourner le personnage dans l'axe y
        player.Rotate(Vector3.up * inputX);

    }
}
