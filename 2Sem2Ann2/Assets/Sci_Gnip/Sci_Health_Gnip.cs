using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sci_Health_Gnip : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Transform healthBar;

    public Sci_Spawn Spawner;
 
    void Start()
    {
        health = maxHealth;
        // Recherche de l'objet nommé "Barre de vie"
        healthBar = transform.Find("Barre de vie");

        if (healthBar == null)
        {
            Debug.LogError("Objet 'Barre de vie'  pas trouvé");
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.localScale = new Vector3 (health,0.2f,0.2f);
        if(health <= 0) 
        {
            Spawner = GameObject.Find("Spawner_"+this.gameObject.tag).GetComponent<Sci_Spawn>();
            Spawner.Compteur = Spawner.Compteur - 1;
            Destroy(gameObject);
        }
    }

    public void InflictDgt(float Dgt)
    {
        health = health - Dgt;
    }
}
