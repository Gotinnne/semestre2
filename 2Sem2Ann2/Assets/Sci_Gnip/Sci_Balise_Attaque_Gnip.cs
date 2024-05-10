using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Sci_Balise_Attaque_Gnip : MonoBehaviour
{
    private Dictionary<int, GameObject> objetsBoosted = new Dictionary<int, GameObject>();

    //timer temps restant balise
    private float timeRemaining = 0;
    public float maxTime;

    //couleur balise
    public enum TypeAgentBalise
    {
        Bleu, Rouge
    };
    public TypeAgentBalise BaliseTag;

    private Sci_Individu_Gnip scriptIndividu;
    public int oldAtk = 1;
    public int oldPlayerAtk = 1;
    public int boostAtk = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == BaliseTag.ToString())
        {
            if (!objetsBoosted.ContainsKey(other.GetInstanceID()))
            {
                objetsBoosted.Add(other.GetInstanceID(), other.gameObject);
                scriptIndividu = other.GetComponent<Sci_Individu_Gnip>();
                if (scriptIndividu != null)
                {
                    if (other.gameObject.tag == BaliseTag.ToString() && other.gameObject.name == "Player")
                    {
                        if (scriptIndividu.dgtMinion != (oldPlayerAtk + boostAtk))
                        {
                            oldPlayerAtk = scriptIndividu.dgtMinion;
                            scriptIndividu.dgtMinion = (oldPlayerAtk + boostAtk);
                        }
                    }
                    else
                    {
                        Debug.Log(other.gameObject);
                        if (scriptIndividu.dgtMinion >= (oldAtk + boostAtk))
                        {
                            Debug.Log(scriptIndividu.dgtMinion);
                            oldAtk = scriptIndividu.dgtMinion;
                            scriptIndividu.dgtMinion = (oldAtk + boostAtk);
                        }
                    }
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        //permet d'arreter set destination si individu sort de la zone
        if (objetsBoosted.ContainsKey(other.GetInstanceID()))
        {
            objetsBoosted.Remove(other.GetInstanceID());
            scriptIndividu = other.GetComponent<Sci_Individu_Gnip>();
            if (scriptIndividu != null)
            {
                if (other.gameObject.tag == BaliseTag.ToString() && other.gameObject.name == "Player")
                {
                    if (scriptIndividu.dgtMinion == (oldPlayerAtk + boostAtk))
                    {
                        scriptIndividu.dgtMinion = oldPlayerAtk;
                    }
                }
                else
                {
                    if (scriptIndividu.dgtMinion != (oldAtk + boostAtk))
                    {
                        scriptIndividu.dgtMinion = oldAtk;
                        Debug.Log("retour atk");
                    }
                }
            }
        }
    }
    void Update()
    {
        timeRemaining = timeRemaining + Time.deltaTime;
        if (timeRemaining >= maxTime)
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
