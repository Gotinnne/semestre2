using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_SpawnTrigger_Gnip : MonoBehaviour
{
    private Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();
    public int MinionsAutour;

    void OnTriggerEnter(Collider other)
    {
        if (!objectsInTrigger.ContainsKey(other.GetInstanceID()))
        {
            Sci_Individu_Gnip sciIndividu = other.GetComponent<Sci_Individu_Gnip>();
            if (sciIndividu != null)
            {
                
            }
            objectsInTrigger.Add(other.GetInstanceID(), other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        //lancer "follow" lorsque individu sort du trigger (a retravailler ?)
        if (objectsInTrigger.ContainsKey(other.GetInstanceID()))
        {

            objectsInTrigger.Remove(other.GetInstanceID());
        }
    }
}
