using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Sci_Balise_Bouclier_Gnip : MonoBehaviour
{
    private Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();
    //timer Ping balise
    private float timePing = 0;
    public float maxTimePing;

    //nombre de shield restant
    private float shieldRemaining;
    public float numberOfShield;

    //destination balise
    public Transform destination;
    public float distanceDestination = 2;

    //couleur balise
    public enum TypeAgentBalise
    {
        Bleu, Rouge
    };
    public TypeAgentBalise BaliseTag;

    private Sci_Health_Gnip scriptHealth;
    public float amountOfHealthFromShield;

    private void Start()
    {
        shieldRemaining = numberOfShield;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == BaliseTag.ToString())
        {
            scriptHealth = other.GetComponent<Sci_Health_Gnip>();
            // Vérifie si l'objet possède le code health
            if (scriptHealth != null)
            {
                if (scriptHealth.shieldActif == false)
                {
                    if (shieldRemaining != 0)
                    {
                        scriptHealth.shieldActif = true;
                        scriptHealth.shield = amountOfHealthFromShield;
                        shieldRemaining = shieldRemaining - 1;
                    }
                }
            }           
        }
    }

    void Update()
    {
        if(shieldRemaining <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.transform.position, 5);
    }
}