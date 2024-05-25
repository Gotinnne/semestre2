using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Sci_QueenBehaviour_Gnip : MonoBehaviour
{
    public Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();

    //verif pour passer reine en spawner
    private Sci_Health_Gnip healthCode;
    public GameObject Spawner1;
    public GameObject Spawner2;
    private Sci_Health_Gnip Spawner1Health;
    private Sci_Health_Gnip Spawner2Health;
    //Spawner
    public GameObject spawn;
    private GameObject ExitChosen;

    private float Timer;
    public float timeBetweenSpawn;

    private float timerVague;
    public float maxTimerVague;

    private int compteurMinions;
    public int compteurMinionsMax;

    //Attaque
    private float timer;
    public float maxTimer;
    public bool targetEnnemiVerif;
    public int dgtReine;
    public int distancestopatkplayer;
    public GameObject ennemiToAttack;
    //EffetVisuel
    public float TimeEffectmax = 0.2f;
    private float TimerEffect = 0.0f;
    private bool EffectBool = false;
    public GameObject AttackEffect;
    public GameObject BaseQueen;
    


    public enum TypeAgent
    {
        Bleu, Rouge
    };
    public TypeAgent WhoAttack;

    // Start is called before the first frame update
    void Start()
    {
        Spawner1Health = Spawner1.GetComponent<Sci_Health_Gnip>();
        Spawner2Health = Spawner2.GetComponent<Sci_Health_Gnip>();
        healthCode = gameObject.GetComponent<Sci_Health_Gnip>();
    }

    // Update is called once per frame
    void Update()
    {
        //code reset effets
        if (EffectBool == true)
        {
            TimerEffect = TimerEffect + Time.deltaTime;
        }
        if (TimerEffect >= TimeEffectmax && EffectBool == true)
        {
            BaseQueen.GetComponent<SpriteRenderer>().enabled = true;
            AttackEffect.GetComponent<SpriteRenderer>().enabled = false;
            EffectBool = false;
            TimerEffect = 0;
            TimeEffectmax = 0.2f;
        }

        //Code d'attaque
        if (ennemiToAttack == null || Vector3.Distance(ennemiToAttack.transform.position, this.transform.position) >= distancestopatkplayer)
        {
            targetEnnemiVerif = false;
        }
        if (targetEnnemiVerif == true)
        {
            timer = timer + Time.deltaTime;
            GameObject EnnemiGameObject = ennemiToAttack;
            Sci_Health_Gnip healthComponent = EnnemiGameObject.GetComponent<Sci_Health_Gnip>();
            if (timer >= maxTimer)
            {
                BaseQueen.GetComponent<SpriteRenderer>().enabled = false;
                AttackEffect.GetComponent<SpriteRenderer>().enabled = true;
                EffectBool = true;
                healthComponent.InflictDgt(dgtReine);
                timer = 0;
            }
            if (ennemiToAttack == null)
            {
                targetEnnemiVerif = false;
            }
        }

        //Code Spawner
        if (healthCode.health < healthCode.maxHealth / 2 && (Spawner1.GetComponent<Sci_Health_Gnip>().BoolSpawn == false || Spawner2Health.BoolSpawn == false))
        {
            if (Spawner1Health.BoolSpawn == false)
            {
                ExitChosen = Spawner1.GetComponent<Sci_Spawn_Gnip>().ExitChosen;
            }
            if (Spawner2Health.BoolSpawn == false)
            {
                ExitChosen = Spawner2.GetComponent<Sci_Spawn_Gnip>().ExitChosen;
            }
            timerVague = timerVague + Time.deltaTime;
            if (maxTimerVague <= timerVague)
            {
                if (compteurMinions == compteurMinionsMax)
                {
                    compteurMinions = 0;
                    timerVague = 0;
                    return;
                }
                if (compteurMinions != compteurMinionsMax)
                {
                    Timer = Timer + Time.deltaTime;
                    if (timeBetweenSpawn <= Timer)
                    {
                        GameObject Individu;
                        Individu = Instantiate(spawn, new Vector3(UnityEngine.Random.Range(gameObject.transform.position.x + 1, gameObject.transform.position.x - 1), 0, UnityEngine.Random.Range(gameObject.transform.position.z + 1, gameObject.transform.position.z - 1)), spawn.transform.rotation);
                        Individu.GetComponent<Sci_Individu_Gnip>().ExitChosen = ExitChosen;
                        compteurMinions++; // Incrémentation de 1
                        Timer = 0;
                    }
                }

            }
        }
    }
            


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == WhoAttack.ToString())
        {
            if (!objectsInTrigger.ContainsKey(other.GetInstanceID()))
            {
                objectsInTrigger.Add(other.GetInstanceID(), other.gameObject);
                if(targetEnnemiVerif == false)
                {
                    targetEnnemiVerif = true;
                    ennemiToAttack = other.gameObject;
                }
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (objectsInTrigger.ContainsKey(other.GetInstanceID()))
        {
            if (targetEnnemiVerif == false)
            {
                targetEnnemiVerif = true;
                ennemiToAttack = other.gameObject;
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
