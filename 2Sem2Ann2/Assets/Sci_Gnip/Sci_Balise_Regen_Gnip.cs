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
            IdleBalise();
            timeRegen = 0;
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
                // Vérifie si l'objet possède le code health
                scriptHealth = obj.GetComponent<Sci_Health_Gnip>();
                if (scriptHealth != null)
                {
                    if (scriptHealth.health < scriptHealth.maxHealth)
                    {
                        //Debug.Log("regen effectuer");
                        //Debug.Log(obj);
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

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.transform.position, 5);
    }
}