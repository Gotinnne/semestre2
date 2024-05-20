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

    void Update()
    {
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
                    Individu = Instantiate(spawn, new Vector3(Random.Range(gameObject.transform.position.x + 1, gameObject.transform.position.x - 1), 0, Random.Range(gameObject.transform.position.z + 1, gameObject.transform.position.z - 1)), spawn.transform.rotation);
                    Individu.GetComponent<Sci_Individu_Gnip>().ExitChosen = ExitChosen;
                    compteurMinions++; // Incrémentation de 1
                    Timer = 0;
                }
            }
            
        }
    }
}