using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_Exit : MonoBehaviour
{
    public Sci_Spawn Spawner;
    // Sortie détruissant les Individu touchant l'exit
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Individu")
        {
            Spawner.Compteur = Spawner.Compteur - 1;
            Destroy(other.gameObject);
        }
    }
}
