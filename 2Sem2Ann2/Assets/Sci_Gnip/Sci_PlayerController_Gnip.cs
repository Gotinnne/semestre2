using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class Sci_PlayerController_Gnip : MonoBehaviour
{ 
    //Deplacement player
    public float speed = 5f;
    public Vector3 direction;
    private Rigidbody rb;

    //Pose balise
    private bool timeBaliseVerif;
    public KeyCode keyPoseBalise = KeyCode.A;
    public GameObject Balise1;
    public GameObject Balise2;
    public GameObject Balise3;

    //timer Pose balise
    private float timeBalise = 0;
    public float maxTimeBalise;

    //Mode balise
    public KeyCode keyModeBalise = KeyCode.F;
    private Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();
    public Transform destination;
    public float distanceDestination = 2;
    public bool modeBaliseVerif;

    //timer Mode balise
    private float timePing = 0;
    public float maxTimePing;

    //choix balise
    public KeyCode Cb1;
    public KeyCode Cb2;
    public KeyCode Cb3;

    //attack
    public KeyCode Attack = KeyCode.E;
    public int multiplierAttack = 0;
    public bool canAttack;
    private Sci_Health_Gnip scriptHealth;
    public float dgtPlayer;
    public enum TypeAgent
    {
        Bleu, Rouge
    };
    public TypeAgent WhoAttack;

    public enum State
    {
        Balise1,
        Balise2,
        Balise3
    }
    public State choixBalise;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        destination = this.gameObject.transform;
    }
    void Update()
    {
        if (Input.GetKeyDown(Attack) && canAttack == true)
        {
            List<int> objectsToRemove = new List<int>(); // Pour stocker les IDs des objets à supprimer
            foreach (var obj in objectsInTrigger.Values)
            {
                if (obj != null)
                {
                    if (obj.tag == WhoAttack.ToString())
                    {
                        // Vérifie si l'objet possède le code health
                        scriptHealth = obj.GetComponent<Sci_Health_Gnip>();
                        if (scriptHealth != null)
                        {
                            scriptHealth.InflictDgt(dgtPlayer * multiplierAttack);
                        }
                    }
                }
            }
            // Supprimer les objets marqués pour suppression
            foreach (int id in objectsToRemove)
            {
                objectsInTrigger.Remove(id);
            }
            multiplierAttack = 0;
            canAttack = false;
        }
        if (Input.GetKeyDown(keyPoseBalise))
        {
            //timer et vérif pose balise
            if (timeBalise <= maxTimeBalise && timeBaliseVerif == true)
            {
                PoseBalise();
                timeBaliseVerif = false;
                timeBalise = timeBalise + Time.deltaTime;
            }
        }
        //Lancer timer si balise poser
        if (timeBaliseVerif == false)
        {
            timeBalise = timeBalise + Time.deltaTime;
        }
        //Reset timer
        if (timeBalise > maxTimeBalise)
        {
            timeBalise = 0;
            timeBaliseVerif = true;
        }
        if (Input.GetKeyDown(Cb1))
        {
            choixBalise = State.Balise1;
        }
        if (Input.GetKeyDown(Cb2))
        {
            choixBalise = State.Balise2;
        }
        if (Input.GetKeyDown(Cb3))
        {
            choixBalise = State.Balise3;
        }

        if (Input.GetKeyDown(keyModeBalise) && modeBaliseVerif == true)
        {
            //Mode balise desactivation
            modeBaliseVerif = false;
            ModeBalise();
        }
        else if (Input.GetKeyDown(keyModeBalise))
        {
            //Mode balise activation
            modeBaliseVerif = true;
            timePing = 0;
        }
        if (modeBaliseVerif == true)
        {
            //si Mode balie actif, démarrer boucle avec timer (fonctionnement balise)
            timePing = timePing + Time.deltaTime;
            if(timePing > maxTimePing)
            {
                ModeBalise();
                timePing = 0;
            }
        }
        // obtenir la direction vers laquelle on applique la force, Raw permet de réaliser un ".normalized"
        direction = (Vector3.right * Input.GetAxisRaw("Horizontal Player" + this.tag) + Vector3.forward * Input.GetAxisRaw("Vertical Player" + this.tag)).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }
    void PoseBalise()
    {
        switch (choixBalise)
        {
            case State.Balise1:
                if (Balise1 != null)
                {
                    Instantiate(Balise1, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
                }
                break;
            case State.Balise2:
                if (Balise2 != null)
                {
                    Instantiate(Balise2, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
                    Balise2 = null;

                }
                break;
            case State.Balise3:
                if(Balise3 != null)
                {
                    Instantiate(Balise3, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
                    Balise3 = null;
                    
                }
                break;
        }
    }
    void ModeBalise()
    {
        List<int> objectsToRemove = new List<int>(); // Pour stocker les IDs des objets à supprimer
        foreach (var obj in objectsInTrigger.Values)
        {
            if (obj != null)
            {
                if(obj.tag == this.tag)
                {
                    // Vérifie si l'objet possède un NavMeshAgent
                    NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();
                    if (agent != null)
                    {
                        // SetDestination peut lancer une NullReferenceException si agent est null
                        agent.SetDestination(new Vector3(Random.Range(destination.position.x - distanceDestination, destination.position.x + distanceDestination), 0, Random.Range(destination.position.z - distanceDestination, destination.position.z + distanceDestination)));
                    }
                    else
                    {
                        // Si l'agent est null, marquez l'objet pour suppression
                        objectsToRemove.Add(obj.GetInstanceID());
                    }
                }
            }
        }
        // Supprimer les objets marqués pour suppression
        foreach (int id in objectsToRemove)
        {
            objectsInTrigger.Remove(id);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //Objet de la couleur de la balise
            if (!objectsInTrigger.ContainsKey(other.GetInstanceID()))
            {
                objectsInTrigger.Add(other.GetInstanceID(), other.gameObject);
            }
    }
    void OnTriggerExit(Collider other)
    {
        //lancer "follow" lorsque individu sort du trigger (a retravailler ?)
        if (objectsInTrigger.ContainsKey(other.GetInstanceID()))
        {
            Sci_Individu_Gnip sciIndividu = other.GetComponent<Sci_Individu_Gnip>();
            if(sciIndividu != null)
            {
                other.gameObject.GetComponent<Sci_Individu_Gnip>().ChangeState(Sci_Individu_Gnip.State.Follow);
            }
            objectsInTrigger.Remove(other.GetInstanceID());
        }
    }
}
