using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Sci_Balise_Gnip : MonoBehaviour
{
    private Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();
    //timer Ping balise
    private float timePing = 0;
    public float maxTimePing;

    //timer temps restant balise
    private float timeRemaining = 0;
    public float maxTime;

    //destination balise
    public Transform destination;
    public float distanceDestination = 2;

    //couleur balise
    public enum TypeAgentBalise
    {
        Bleu, Rouge
    };
    public TypeAgentBalise BaliseTag;

    public GameObject Balise1;
    public GameObject Balise2;
    public GameObject Balise3;
    public GameObject Balise4;
    public GameObject Balise5;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == BaliseTag.ToString())
        {
            if (!objectsInTrigger.ContainsKey(other.GetInstanceID()))
            {
                objectsInTrigger.Add(other.GetInstanceID(), other.gameObject);
                if(other.GetComponent<Sci_Individu_Gnip>() != null)
                {
                    other.GetComponent<Sci_Individu_Gnip>().baliseVerif = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //permet d'arreter set destination si individu sort de la zone
        if (objectsInTrigger.ContainsKey(other.GetInstanceID()))
        {
            objectsInTrigger.Remove(other.GetInstanceID());
        }
    }

    void Update()
    {
        timeRemaining = timeRemaining + Time.deltaTime;
        timePing = timePing + Time.deltaTime;
        if (timePing >= maxTimePing)
        {
            IdleBalise();
            timePing = 0;
        }
        if (timeRemaining >= maxTime)
        {
            IdleBalise();
            Destroy(gameObject);
        }
        if (timeRemaining > maxTime * 0.2)
        {
            Balise1.GetComponent<SpriteRenderer>().enabled = false;
            Balise2.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (timeRemaining > maxTime * 0.4)
        {
            Balise2.GetComponent<SpriteRenderer>().enabled = false;
            Balise3.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (timeRemaining > maxTime * 0.6)
        {
            Balise3.GetComponent<SpriteRenderer>().enabled = false;
            Balise4.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (timeRemaining > maxTime * 0.8)
        {
            Balise4.GetComponent<SpriteRenderer>().enabled = false;
            Balise5.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    void IdleBalise()
    {
        List<int> objectsToRemove = new List<int>(); // Pour stocker les IDs des objets à supprimer
        foreach (var obj in objectsInTrigger.Values)
        {
            if (obj != null)
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

        // Supprimer les objets marqués pour suppression
        foreach (int id in objectsToRemove)
        {
            objectsInTrigger.Remove(id);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.transform.position, 5);
    }
}