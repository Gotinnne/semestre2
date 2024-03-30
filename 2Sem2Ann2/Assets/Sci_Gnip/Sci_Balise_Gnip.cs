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
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == BaliseTag.ToString())
        {
            if (!objectsInTrigger.ContainsKey(other.GetInstanceID()))
            {
                objectsInTrigger.Add(other.GetInstanceID(), other.gameObject);
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

                if (timeRemaining >= maxTime)
                {
                    // Vérifie si l'objet possède un composant Sci_Individu_Gnip
                    Sci_Individu_Gnip sciIndividu = obj.GetComponent<Sci_Individu_Gnip>();
                    if (sciIndividu != null)
                    {
                        // Appeler Follow() peut lancer une NullReferenceException si sciIndividu est null
                        obj.gameObject.GetComponent<Sci_Individu_Gnip>().ChangeState(Sci_Individu_Gnip.State.Follow);
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

}