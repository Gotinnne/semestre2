using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sci_PlayerController_Gnip : MonoBehaviour
{ 
    public float speed = 10f;
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
        direction = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");

        // applique directement sur velocity la direction multiplier part la speed
        // prévision problème: en fesant de cette manière la force de collisions du joueur est très puissante et assure des problèmes lors d'utilisation de la physique
        rb.velocity = (direction * speed);

        // permet d'appliquer le IS Grounded (sécurité au cas ou le joueur arrive sauter)
        if (rb.gameObject.transform.position.y >= 0)
        {
            IsGrounded = false;
        }
        if (rb.gameObject.transform.position.y == 0 & IsGrounded == false)
        {
            IsGrounded = true;
        }
        // permet d'appliquer une gravité si le joueur (sécurité au cas ou le joueur arrive sauter)
        if (rb == !IsGrounded)
        {
            rb.AddForce(new Vector3(0, -9.14f, 0), ForceMode.Force);
        }
        if (rb == IsGrounded)
        {
            rb.AddForce(new Vector3(0, 0, 0), ForceMode.Force);
        }
    }

    void PoseBalise()
    {
        Instantiate(Balise, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
    }
}
