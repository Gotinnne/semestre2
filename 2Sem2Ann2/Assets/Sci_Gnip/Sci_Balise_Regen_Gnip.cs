using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Sci_Balise_Regen_Gnip : MonoBehaviour
{
    private Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();
    //timer Ping balise
    private float timeRegen = 0;
    public float maxTimeRegen;

    //timer temps restant balise
    private float timeRemaining = 0;
    public float maxTime;

    //couleur balise
    public enum TypeAgentBalise
    {
        Bleu, Rouge
    };
    public TypeAgentBalise BaliseTag;

    private Sci_Health_Gnip scriptHealth;
    public float amountRegenHealth;

    //healEffect
    public GameObject HealEffectGO;
    public float TimeEffectmax = 0.5f;
    private float TimerEffect = 0.0f;
    private bool HealEffectBool = false;

    public GameObject Balise1;
    public GameObject Balise2;
    public GameObject Balise3;
    public GameObject Balise4;
    public GameObject Balise5;
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

    void Update()
    {
        timeRemaining = timeRemaining + Time.deltaTime;
        timeRegen = timeRegen + Time.deltaTime;
        if (timeRegen >= maxTimeRegen)
        {
            HealEffect();
            IdleBalise();
            timeRegen = 0;
        }
        if (timeRemaining >= maxTime)
        {
            HealEffect();
            IdleBalise();
            Destroy(gameObject);
        }
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
                // Vérifie si l'objet possède le code health
                scriptHealth = obj.GetComponent<Sci_Health_Gnip>();
                if (scriptHealth != null)
                {
                    if (scriptHealth.health < scriptHealth.maxHealth)
                    {
                        //Debug.Log("regen effectuer");
                        //Debug.Log(obj);
                        scriptHealth.HealEffect();
                        scriptHealth.health = scriptHealth.health + amountRegenHealth;
                        scriptHealth.UpdateHealthBar();
                        // Si health est augmenter au dessus de maxhealth, le met au même niveau
                        if (scriptHealth.health > scriptHealth.maxHealth)
                        {
                            scriptHealth.health = scriptHealth.maxHealth;
                        }
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
        HealEffectGO.GetComponent<SpriteRenderer>().enabled = true;
        HealEffectBool = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.transform.position, 5);
    }
}