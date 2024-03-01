using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Sci_Health_Gnip : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private Transform healthBar;

    public Sci_Spawn_Gnip Spawner;
    public float maxTimer;
    public float Timer;

    public float lenghtHealthBar = 5;
    void Start()
    {
        health = maxHealth;
        // Recherche de l'objet nommé "Barre de vie"
        healthBar = transform.Find("Barre de vie");

        if (healthBar == null)
        {
            Debug.LogError("Objet 'Barre de vie'  pas trouvé");
        }

        if (!gameObject.CompareTag("spawner"))
        {
            Spawner = GameObject.Find("Spawner_" + this.gameObject.tag).GetComponent<Sci_Spawn_Gnip>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //set taille taille healthBar (soucis: utilisation LocalScale)
        healthBar.localScale = new Vector3( lenghtHealthBar, 0.3f, 0.3f);

        //vérification mort de l'entité
        if (health <= 0) 
        {
            
            if (gameObject.name == "Spawner_Rouge" || gameObject.name == "Spawner_Bleu")
            {
                //si entité un spawner désactiver durant timer
                this.gameObject.GetComponent<Renderer>().enabled = false;
                this.gameObject.GetComponent<Sci_Spawn_Gnip>().enabled = false;
                Timer = Time.deltaTime + Timer;
                if(Timer >= maxTimer)
                {
                    Timer = 0;
                    this.gameObject.GetComponent<Renderer>().enabled = true;
                    this.gameObject.GetComponent<Sci_Spawn_Gnip>().enabled = true;
                }
            }
            else
            {
                //sinon détruit entité et baisse compteur
                //Spawner.Compteur = Spawner.Compteur - 1;
                Destroy(gameObject);
            }
        }
    }

    public void InflictDgt(float Dgt)
    {
        health = health - Dgt;
        lenghtHealthBar = (lenghtHealthBar * (health/maxHealth));
    }
}
