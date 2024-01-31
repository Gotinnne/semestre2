using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Individu")
        {
            Destroy(other.gameObject);
        }
    }
}
