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
    private Sci_PlayerController_Gnip playerController;
    private Sci_QueenBehaviour_Gnip QueenBehaviour;
    public int oldAtk = 1;
    public float oldPlayerAtk = 1;
    public float oldQueenAtk = 5;
    public int boostAtk = 1;
    public bool PlayerBoosted = false;

    //healEffect
    public GameObject HealEffectGO;
    public float TimeEffectmax = 0.5f;
    private float TimerEffect = 0.0f;
    private bool HealEffectBool = false;

    public GameObject Balise1;
    public GameObject Balise2;
    public GameObject Balise3;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == BaliseTag.ToString())
        {
            if (!objetsBoosted.ContainsKey(other.GetInstanceID()))
            {
                objetsBoosted.Add(other.GetInstanceID(), other.gameObject);
                scriptIndividu = other.GetComponent<Sci_Individu_Gnip>();
                
                if(scriptIndividu == null)
                {
                    playerController = other.GetComponent<Sci_PlayerController_Gnip>();
                    if (scriptIndividu == null && playerController == null)
                    {
                        QueenBehaviour = other.GetComponent<Sci_QueenBehaviour_Gnip>();
                        if(QueenBehaviour != null)
                        {
                            HealEffect();
                            other.GetComponent<Sci_Health_Gnip>().DgtUpEffect();
                            oldQueenAtk = QueenBehaviour.dgtReine;
                            if(QueenBehaviour.maxTimer != 1)
                            {
                                QueenBehaviour.maxTimer = QueenBehaviour.maxTimer - 1;
                            }
                        }
                    }
                }
                if (scriptIndividu != null)
                {
                    oldAtk = scriptIndividu.dgtMinion;
                    if (scriptIndividu.dgtMinion >= (oldAtk + boostAtk))
                    {
                        HealEffect();
                        other.GetComponent<Sci_Health_Gnip>().DgtUpEffect();
                        scriptIndividu.dgtMinion = (oldAtk + boostAtk);
                    }
                }
                if(playerController != null)
                {
                    oldPlayerAtk = playerController.dgtPlayer;
                    if (playerController.dgtPlayer != (oldPlayerAtk + boostAtk) && PlayerBoosted == false)
                    {
                        HealEffect();
                        other.GetComponent<Sci_Health_Gnip>().DgtUpEffect();
                        playerController.dgtPlayer = (oldPlayerAtk + boostAtk);
                        PlayerBoosted = true;
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
            if (scriptIndividu == null)
            {
                playerController = other.GetComponent<Sci_PlayerController_Gnip>();
            }
            if (scriptIndividu != null)
            {
                if (scriptIndividu.dgtMinion == (oldAtk + boostAtk))
                {
                    scriptIndividu.dgtMinion = oldAtk;
                    other.GetComponent<Sci_Health_Gnip>().DgtUpEffectFalse();
                }
            }
            if (playerController != null)
            {
                other.GetComponent<Sci_Health_Gnip>().DgtUpEffectFalse();
                playerController.dgtPlayer = oldPlayerAtk;
                PlayerBoosted = false;
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
        timeRemaining = timeRemaining + Time.deltaTime;

        if (timeRemaining >= maxTime)
        {
            foreach (var obj in objetsBoosted.Values)
            {
                if (scriptIndividu != null)
                {
                    if (scriptIndividu.dgtMinion == (oldAtk + boostAtk))
                    {
                        scriptIndividu.dgtMinion = oldAtk;
                        obj.GetComponent<Sci_Health_Gnip>().DgtUpEffectFalse();
                    }
                }
            }
            if(playerController !=null)
            {
                playerController.dgtPlayer = oldPlayerAtk;
            }
            if(QueenBehaviour !=  null)
            {
                QueenBehaviour.maxTimer = QueenBehaviour.maxTimer + 1;
            }
            Destroy(gameObject);
        }
        if (timeRemaining > maxTime * 0.4)
        {
            Balise1.GetComponent<SpriteRenderer>().enabled = false;
            Balise2.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (timeRemaining > maxTime * 0.7)
        {
            Balise2.GetComponent<SpriteRenderer>().enabled = false;
            Balise3.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void HealEffect()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Balises/boost_attaque", GetComponent<Transform>().position);
        HealEffectGO.GetComponent<SpriteRenderer>().enabled = true;
        HealEffectBool = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.transform.position, 5);
    }
}
