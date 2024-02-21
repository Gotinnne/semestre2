using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Sci_Ping_Var_Cam : MonoBehaviour
{
    public Transform destination; // L'objet vers lequel les autres objets se d�placeront
    public KeyCode Ping = KeyCode.Mouse0; // Le bouton qui d�clenchera le mouvement

    private Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();

    public float MaxTimeFollow;
    public float TimeFollow; 

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
        if (Input.GetKeyDown(Ping))
        {
            while (TimeFollow < MaxTimeFollow)
            {
                TimeFollow = TimeFollow + Time.deltaTime;
                SetTargetPLayer();
            }
            TimeFollow = 0;
        }
    }

    void SetTargetPLayer()
    {
 
        NavMeshHit hit;
        foreach (var obj in objectsInTrigger.Values)
        {

            // V�rifier si le point de destination est sur le NavMesh
            if (NavMesh.SamplePosition(destination.position, out hit, 1.0f, NavMesh.AllAreas))
            {
                NavMeshAgent navMeshAgent = obj.GetComponent<NavMeshAgent>();

                if (navMeshAgent != null)
                {
 
                    // D�finir la destination pour chaque objet avec NavMeshAgent
                    navMeshAgent.SetDestination(new Vector3(Random.Range(destination.position.x-1, destination.position.x+1),0, Random.Range(destination.position.z-1, destination.position.z+1)));

                }
            }
        }
    }
}