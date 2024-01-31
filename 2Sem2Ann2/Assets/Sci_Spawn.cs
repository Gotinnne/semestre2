using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_Spawn : MonoBehaviour
{
    public GameObject spawn;
    public Transform Point1;
    public Transform Point2;

    public float Timer;
    public float MaxTimer;

    public List<Color> coloursList;

    void Update()
    {
        // spawner selon MaxTimer dans point 1 et 2 afin de crée sensation de random
        Timer = Timer + Time.deltaTime;
        if (MaxTimer < Timer)
        {
            GameObject Individu;
            Individu = Instantiate(spawn, new Vector3(Random.Range(Point1.position.x, Point2.position.x), 0, this.gameObject.GetComponent<Transform>().position.z+1), spawn.transform.rotation);
            int ColorRandomCount = Random.Range(0, coloursList.Count);
            Individu.GetComponent<MeshRenderer>().material.color = coloursList[ColorRandomCount];
            Timer = 0;
        }
    }
}
