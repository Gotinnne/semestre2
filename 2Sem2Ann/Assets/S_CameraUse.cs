using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_CameraUse : MonoBehaviour
{
    public bool cameraActive;
    public GameObject CameraMain;
    public GameObject CameraCam;

    private void Update()
    {
        // Input E pour activer ou désactiver "cameraGameObject"
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (cameraActive == true)
            {
                CameraMain.GetComponent<Camera>().enabled = false;
                CameraCam.GetComponent<Camera>().enabled = true;
                cameraActive = false;
            }
            else
            {
                CameraMain.GetComponent<Camera>().enabled = true;
                CameraCam.GetComponent<Camera>().enabled = false;
                cameraActive = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

        }
    }

}
