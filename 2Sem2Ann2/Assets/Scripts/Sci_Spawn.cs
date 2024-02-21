using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_Spawn : MonoBehaviour
{
    public GameObject spawn;
    public Transform Point1;
    public Transform Point2;

    private float Timer;
    public float MaxTimer;

    public List<Color> ColoursList;

    public float Compteur;
    public float MaxCompteur;

    void Update()
    {
        // spawner selon MaxTimer dans point 1 et 2 afin de crée sensation de random
        Timer = Timer + Time.deltaTime;
        if (MaxTimer < Timer & Compteur <= MaxCompteur)
        {
            GameObject Individu;
            Individu = Instantiate(spawn, new Vector3(Random.Range(Point1.position.x, Point2.position.x), 0, Random.Range(this.gameObject.GetComponent<Transform>().position.z+1, this.gameObject.GetComponent<Transform>().position.z-1)), spawn.transform.rotation);
            int ColorRandomCount = Random.Range(0, ColoursList.Count);
            Individu.GetComponent<MeshRenderer>().material.color = ColoursList[ColorRandomCount];
            Compteur = Compteur + 1;
            Timer = 0;
        }
    }
}
