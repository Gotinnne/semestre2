using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_RayCast_Var_CamVisibility : MonoBehaviour
{
    Camera cam;
    public GameObject Gb;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1000f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.yellow);

        Gb.GetComponent<Rigidbody>().position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
     }
}
