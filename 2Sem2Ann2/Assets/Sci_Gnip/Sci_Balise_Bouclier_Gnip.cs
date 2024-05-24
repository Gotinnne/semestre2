using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Sci_Balise_Bouclier_Gnip : MonoBehaviour
{
    private Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();

    //nombre de shield restant
    private float shieldRemaining;
    public float numberOfShield;

    //couleur balise
    public enum TypeAgentBalise
    {
        Bleu, Rouge
    };
    public TypeAgentBalise BaliseTag;

    private Sci_Health_Gnip scriptHealth;
    public float amountOfHealthFromShield;
    //healEffect
    public GameObject HealEffectGO;
    public float TimeEffectmax = 0.5f;
    private float TimerEffect = 0.0f;
    private bool HealEffectBool = false;

    public GameObject Balise1;
    public GameObject Balise2;
    public GameObject Balise3;
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
                        HealEffect();
                        scriptHealth.BouclierEffect();
                        scriptHealth.Bouclier1GO.GetComponent<SpriteRenderer>().enabled = true;
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
        if (HealEffectBool == true)
        {
            TimerEffect = TimerEffect + Time.deltaTime;
        }
        if (TimerEffect >= TimeEffectmax)
        {
            HealEffectGO.GetComponent<SpriteRenderer>().enabled = false;
            HealEffectBool = false;
            TimerEffect = 0;
        }
        if (shieldRemaining <= 0)
        {
            Destroy(gameObject);
        }
        if (shieldRemaining > numberOfShield * 0.3)
        {
            Balise1.GetComponent<SpriteRenderer>().enabled = false;
            Balise2.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (shieldRemaining > numberOfShield * 0.7)
        {
            Balise2.GetComponent<SpriteRenderer>().enabled = false;
            Balise3.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void HealEffect()
    {
        HealEffectGO.GetComponent<SpriteRenderer>().enabled = true;
        HealEffectBool = true;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.transform.position, 5);
    }
}