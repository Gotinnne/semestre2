using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sci_PlayerController_Gnip : MonoBehaviour
{ 
    public float speed = 5f;
    public Vector3 direction;
    private Rigidbody rb;
    private bool IsGrounded;

    private float timeBalise = 0;
    public float maxTimeBalise;
    private bool timeBaliseVerif;
    public KeyCode keyPoseBalise = KeyCode.E;

    public GameObject Balise;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyPoseBalise))
        {
            
            if (timeBalise <= maxTimeBalise && timeBaliseVerif == true)
            {
                PoseBalise();
                timeBaliseVerif = false;
                timeBalise = timeBalise + Time.deltaTime;
            }
        }
        if (timeBaliseVerif == false)
        {
            timeBalise = timeBalise + Time.deltaTime;
        }
        if (timeBalise > maxTimeBalise)
        {
            timeBalise = 0;
            timeBaliseVerif = true;
        }

        // obtenir la direction vers laquelle on applique la force, Raw permet de réaliser un ".normalized"
        direction = (Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical")).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }

    void PoseBalise()
    {
        Instantiate(Balise, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
    }
}
