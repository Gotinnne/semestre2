using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_Go_Queen_Gnip : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Sci_Individu_Gnip>() != null)
        {
            other.GetComponent<Sci_Individu_Gnip>().goForTheQueen = true;
        }
    }
}
