using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_Spawn_Gnip : MonoBehaviour
{
    public GameObject spawn;
    public GameObject ExitChosen;

    public float Timer;
    public float timeBetweenSpawn;

    public float timerVague;
    public float maxTimerVague;

    public int compteurMinions;
    public int compteurMinionsMax;

    public GameObject SpawnerDetruit;
    public GameObject SpawnerSpawn;



    private void Start()
    {
        SpawnerDetruit.GetComponent<SpriteRenderer>().enabled = false;
        SpawnerSpawn.GetComponent<SpriteRenderer>().enabled = false;
    }
    void Update()
    {
        timerVague = timerVague + Time.deltaTime;
        if(maxTimerVague * 0.5 <= timerVague)
        {
            SpawnerSpawn.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (maxTimerVague <= timerVague) 
        {
            if (compteurMinions == compteurMinionsMax)
            {
                compteurMinions = 0;
                timerVague = 0;
                SpawnerSpawn.GetComponent<SpriteRenderer>().enabled = false;
                return;
            }
            if (compteurMinions != compteurMinionsMax)
            {
                Timer = Timer + Time.deltaTime;
                if (timeBetweenSpawn <= Timer)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/autres/Spawn", GetComponent<Transform>().position);
                    GameObject Individu;
                    Individu = Instantiate(spawn, new Vector3(Random.Range(gameObject.transform.position.x + 1, gameObject.transform.position.x - 1), 0, Random.Range(gameObject.transform.position.z + 1, gameObject.transform.position.z - 1)), spawn.transform.rotation);
                    Individu.GetComponent<Sci_Individu_Gnip>().ExitChosen = ExitChosen;
                    compteurMinions++; // Incrémentation de 1
                    Timer = 0;
                }
            }
        }
    }
}