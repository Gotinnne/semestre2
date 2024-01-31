using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_LaunchIdle : MonoBehaviour
{

    private Dictionary<int, GameObject> Tampon = new Dictionary<int, GameObject>();

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Individu")
        {
            if (!Tampon.ContainsKey(other.GetInstanceID()))
            {
                Tampon.Add(other.GetInstanceID(), other.gameObject);
                other.gameObject.GetComponent<Sci_Individu>().IdleActivator = true;
                other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
