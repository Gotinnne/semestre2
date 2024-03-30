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
    public float maxTimerSpawn;
    public float timerSpawn;

    public float maxTimerPlayer;
    public float timerPlayer;

    public float lenghtHealthBar = 5;
    private float lenghtHealthBarMax;
    void Start()
    {
        health = maxHealth;
        lenghtHealthBarMax = lenghtHealthBar;
        // Recherche de "Barre de vie"
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
                this.gameObject.GetComponent<Collider>().enabled = false;
                this.gameObject.GetComponent<Renderer>().enabled = false;
                this.gameObject.GetComponent<Sci_Spawn_Gnip>().enabled = false;
                timerSpawn = Time.deltaTime + timerSpawn;
                if(timerSpawn >= maxTimerSpawn)
                {
                    timerSpawn = 0;
                    health = maxHealth;
                    this.gameObject.GetComponent<Collider>().enabled = true;
                    this.gameObject.GetComponent<Renderer>().enabled = true;
                    this.gameObject.GetComponent<Sci_Spawn_Gnip>().enabled = true;
                }
            }
            if (gameObject.name == "Player_Rouge" || gameObject.name == "Player_Bleu")
            {
                //si entité un spawner désactiver durant timer

                timerPlayer = Time.deltaTime + timerPlayer;
                if (timerPlayer >= maxTimerPlayer)
                {
                    timerPlayer = 0;
                    //effet de respawn player
                    maxTimerPlayer = maxTimerPlayer + 5;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public void InflictDgt(float Dgt)
    {
        health = health - Dgt;
        lenghtHealthBar = (lenghtHealthBarMax * (health/maxHealth));
    }
}
