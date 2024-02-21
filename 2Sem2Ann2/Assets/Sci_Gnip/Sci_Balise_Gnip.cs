using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Sci_Balise_Gnip : MonoBehaviour
{
    public Transform destination; 
   
    private Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();

    private float timePing = 0;
    public float maxTimePing;

    public float timeRemaining = 0;
    public float maxTime;

    public float distanceDestination = 5;

    void OnTriggerEnter(Collider other)
    {
        if (!objectsInTrigger.ContainsKey(other.GetInstanceID()))
        {
            objectsInTrigger.Add(other.GetInstanceID(), other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (objectsInTrigger.ContainsKey(other.GetInstanceID()))
        {
            objectsInTrigger.Remove(other.GetInstanceID());
        }
    }

    void Update()
    {
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        timeRemaining = timeRemaining + Time.deltaTime;
        timePing = timePing + Time.deltaTime;
        if (timePing >= maxTimePing)
        {
            IdleBalise();
            timePing = 0;
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.grey;
        }
        if (timeRemaining >= maxTime)
        {
            IdleBalise();
            Destroy(transform.parent.gameObject);
        }
    }

    //void IdleBalise()
    //{
    //    foreach (var obj in objectsInTrigger.Values)
    //    {
    //        if(obj != null)
    //        {
    //            if (obj.GetComponent<Sci_Individu_Gnip>().targetEnnemiVerif == false)
    //            {
    //                NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();
    //                agent.SetDestination(new Vector3(Random.Range(destination.position.x - distanceDestination, destination.position.x + distanceDestination), 0, Random.Range(destination.position.z - distanceDestination, destination.position.z + distanceDestination)));
    //                if (agent == null)
    //                {
    //                    objectsInTrigger.Remove(gameObject.GetInstanceID());
    //                }
    //            }
    //            if (timeRemaining >= maxTime)
    //            {
    //                obj.GetComponent<Sci_Individu_Gnip>().Follow();
    //            }
    //        }
    //    }
    //}
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
                        sciIndividu.Follow();
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