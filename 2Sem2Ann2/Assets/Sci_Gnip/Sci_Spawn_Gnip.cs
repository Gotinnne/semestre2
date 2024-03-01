using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_Spawn_Gnip : MonoBehaviour
{
    public GameObject spawn;

    public float Timer;
    public float timeBetweenSpawn;

    public float timerVague;
    public float maxTimerVague;

    public List<Color> ColoursList;

    public float compteurMinions;
    public float compteurMinionsMax;

    void Update()
    {
        timerVague = timerVague + Time.deltaTime;
        if (maxTimerVague <= timerVague) 
        {
            while(compteurMinionsMax != compteurMinions)
            {
                Timer = Timer + Time.deltaTime;
                if (timeBetweenSpawn < Timer)
                {
                    GameObject Individu;
                    Individu = Instantiate(spawn, new Vector3(Random.Range(gameObject.transform.position.x + 1, gameObject.transform.position.x - 1), 0, Random.Range(gameObject.transform.position.z + 1, gameObject.transform.position.z - 1)), spawn.transform.rotation);
                    int ColorRandomCount = Random.Range(0, ColoursList.Count);
                    Individu.GetComponent<MeshRenderer>().material.color = ColoursList[ColorRandomCount];
                    compteurMinions = compteurMinions + 1;
                    Timer = 0;
                }
            }
            timerVague = 0;
            compteurMinions = 0;
        }
    }
}