using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_Balise_Attaque_Gnip : MonoBehaviour
{
    private Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();

    //couleur balise
    public enum TypeAgentBalise
    {
        Bleu, Rouge
    };
    public TypeAgentBalise BaliseTag;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == BaliseTag.ToString())
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
}
