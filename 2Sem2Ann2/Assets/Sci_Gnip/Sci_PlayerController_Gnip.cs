using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using static Sci_Inventaire_Gnip;


public class Sci_PlayerController_Gnip : MonoBehaviour
{ 
    //Deplacement player
    public float speed = 5f;
    public Vector3 direction;
    private Rigidbody rb;
    public float rotationSpeed = 10f;

    //Pose balise
    private bool timeBaliseVerif;
    public GameObject Balise1;
    public GameObject Balise2;
    public GameObject Balise3;

    //choix balise
    public KeyCode Cb1;
    public KeyCode Cb2;
    public KeyCode Cb3;

    public GameObject EBalise1;
    public GameObject EBalise2;
    public GameObject EBalise3;

    public GameObject EBalise1Base;
    public GameObject EBalise2Base;
    public GameObject EBalise3Base;

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

    //attack
    public KeyCode Attack = KeyCode.E;
    public int multiplierAttack = 0;
    public bool canAttack;
    private Sci_Health_Gnip scriptHealth;
    public int dgtPlayer;
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

    //healEffect
    public GameObject ModeBaliseEffectGO;
    public float TimeEffectmax = 0.2f;
    private float TimerEffect = 0.0f;
    private bool ModeBaliseEffect = false;


    public GameObject CamBleu;
    public GameObject CamRouge;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        destination = this.gameObject.transform;
        CamBleu = GameObject.Find("Camera_Bleu");
        CamRouge = GameObject.Find("Camera_Rouge");
    }
    void Update()
    {
        if (ModeBaliseEffect == true)
        {
            TimerEffect = TimerEffect + Time.deltaTime;
        }
        if (TimerEffect >= TimeEffectmax && ModeBaliseEffect == true)
        {
            ModeBaliseEffectGO.GetComponent<SpriteRenderer>().enabled = false;
            ModeBaliseEffect = false;
            TimerEffect = 0;
        }

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
        //Lancer timer si balise poser
        if (timeBaliseVerif == false)
        {
            timeBalise = timeBalise + Time.deltaTime;
        }
        //Reset timer
        if (timeBalise > maxTimeBalise)
        {
            timeBalise = 0;
            EBalise1Base.GetComponent<SpriteRenderer>().enabled = true;
            EBalise2Base.GetComponent<SpriteRenderer>().enabled = true;
            EBalise3Base.GetComponent<SpriteRenderer>().enabled = true;
            EBalise1.GetComponent<SpriteRenderer>().enabled = false;
            EBalise2.GetComponent<SpriteRenderer>().enabled = false;
            EBalise3.GetComponent<SpriteRenderer>().enabled = false;
            timeBaliseVerif = true;
        }
        if (Input.GetKeyDown(Cb1) && timeBalise <= maxTimeBalise && timeBaliseVerif == true)
        {
            Instantiate(Balise1, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
            timeBaliseVerif = false;
            EBalise1.GetComponent<SpriteRenderer>().enabled = true;
            EBalise2.GetComponent<SpriteRenderer>().enabled = true;
            EBalise3.GetComponent<SpriteRenderer>().enabled = true;
            EBalise1Base.GetComponent<SpriteRenderer>().enabled = false;
            EBalise2Base.GetComponent<SpriteRenderer>().enabled = false;
            EBalise3Base.GetComponent<SpriteRenderer>().enabled = false;
            timeBalise = timeBalise + Time.deltaTime;
        }
        if (Input.GetKeyDown(Cb2) && timeBalise <= maxTimeBalise && timeBaliseVerif == true)
        {
            Instantiate(Balise2, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
            Balise2 = null;
            timeBaliseVerif = false;
            EBalise1.GetComponent<SpriteRenderer>().enabled = true;
            EBalise2.GetComponent<SpriteRenderer>().enabled = true;
            EBalise3.GetComponent<SpriteRenderer>().enabled = true;
            EBalise1Base.GetComponent<SpriteRenderer>().enabled = false;
            EBalise2Base.GetComponent<SpriteRenderer>().enabled = false;
            EBalise3Base.GetComponent<SpriteRenderer>().enabled = false;

            if (WhoAttack.ToString() == "Rouge")
            {
                CamBleu.GetComponent<Sci_Inventaire_Gnip>().Balise_2_State = Balise_2.Vide;
            }
            if (WhoAttack.ToString() == "Bleu")
            {
                CamRouge.GetComponent<Sci_Inventaire_Gnip>().Balise_2_State = Balise_2.Vide;
            }
            timeBalise = timeBalise + Time.deltaTime;
        }
        if (Input.GetKeyDown(Cb3) && timeBalise <= maxTimeBalise && timeBaliseVerif == true)
        {
            Instantiate(Balise3, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
            Balise3 = null;
            timeBaliseVerif = false;
            EBalise1.GetComponent<SpriteRenderer>().enabled = true;
            EBalise2.GetComponent<SpriteRenderer>().enabled = true;
            EBalise3.GetComponent<SpriteRenderer>().enabled = true;
            EBalise1Base.GetComponent<SpriteRenderer>().enabled = false;
            EBalise2Base.GetComponent<SpriteRenderer>().enabled = false;
            EBalise3Base.GetComponent<SpriteRenderer>().enabled = false;

            if (WhoAttack.ToString() == "Rouge")
            {
                CamBleu.GetComponent<Sci_Inventaire_Gnip>().Balise_3_State = Balise_3.Vide;
            }
            if (WhoAttack.ToString() == "Bleu")
            {
                CamRouge.GetComponent<Sci_Inventaire_Gnip>().Balise_3_State = Balise_3.Vide;
            }
            timeBalise = timeBalise + Time.deltaTime;
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
                HealEffect();
                ModeBalise();
                timePing = 0;
            }
        }
        // obtenir la direction vers laquelle on applique la force, Raw permet de réaliser un ".normalized"
        direction = (Vector3.right * Input.GetAxisRaw("Horizontal Player" + this.tag) + Vector3.forward * Input.GetAxisRaw("Vertical Player" + this.tag)).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
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

    public void HealEffect()
    {
        ModeBaliseEffectGO.GetComponent<SpriteRenderer>().enabled = true;
        ModeBaliseEffect = true;
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
