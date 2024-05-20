using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Sci_Health_Gnip : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private Transform healthBar;

    public float shield;
    public bool shieldActif;

    public float maxTimerSpawn;
    public float timerSpawn;

    public float maxTimerPlayer;
    public float timerPlayer;

    public float lenghtHealthBar = 5;
    private float lenghtHealthBarMax;
    public GameObject Barre;
    void Start()
    {
        if (this.name == "Queen_Bleu" || this.name == "Queen_Rouge")
        {

        }
        else
        {
            // Recherche de "Barre de vie"
            healthBar = transform.Find("Barre de vie");
            if (healthBar == null)
            {
                Debug.LogError("Objet 'Barre de vie'  pas trouvé");
            }
        }

        health = maxHealth;
        lenghtHealthBarMax = lenghtHealthBar;

    }
    void Update()
    {
        if (this.name == "Queen_Bleu" || this.name == "Queen_Rouge")
        {
            UpdateHealthBar();
            Barre.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lenghtHealthBar);
        }
        else if(this.name != "Queen_Bleu" && this.name != "Queen_Rouge" && this.name != "Spawner_Rouge_1" && this.name != "Spawner_Rouge_2" && this.name != "Spawner_Bleu_1" && this.name != "Spawner_Bleu_2")
        {
            UpdateHealthBar();
            Barre.GetComponent<Transform>().localScale = new Vector3(lenghtHealthBar, 1, 1);
        }

        //vérification mort de l'entité
        if (health <= 0) 
        {
            if ( gameObject.name == "Spawner_Rouge" || gameObject.name == "Spawner_Bleu")
            {
                Debug.Log("destroy spawner");
                //si entité un spawner désactiver durant timer
                this.gameObject.GetComponent<Collider>().enabled = false;
                this.gameObject.GetComponent<Sci_Spawn_Gnip>().enabled = false;
                timerSpawn = Time.deltaTime + timerSpawn;
                if(timerSpawn >= maxTimerSpawn)
                {
                    timerSpawn = 0;
                    health = maxHealth;
                    this.gameObject.GetComponent<Collider>().enabled = true;
                    this.gameObject.GetComponent<Sci_Spawn_Gnip>().enabled = true;
                }
            }
            if (gameObject.name == "Player_Rouge" || gameObject.name == "Player_Bleu")
            {
                Debug.Log("Destroy player");    
                //si entité un spawner désactiver durant timer

                timerPlayer = Time.deltaTime + timerPlayer;
                if (timerPlayer >= maxTimerPlayer)
                {
                    timerPlayer = 0;
                    //effet de respawn player
                    maxTimerPlayer = maxTimerPlayer + 5;
                }
            }
            if(gameObject.name != "Spawner_Rouge" && gameObject.name != "Spawner_Bleu" && gameObject.name != "Player_Rouge" && gameObject.name != "Player_Bleu")
            {
                Destroy(gameObject);
            }
        }
    }
    public void InflictDgt(float Dgt)
    {
        if(shieldActif == true)
        {
            shield = shield - Dgt;
            if (shield < 0)
            {
                health = health + shield;
                shieldActif = false;
                UpdateHealthBar();
            }
            else if (shield == 0)
            {
                shieldActif = false;
            }
        }
        else if (shieldActif == false)
        {
            health = health - Dgt;
            UpdateHealthBar();
        }
    }
    public void UpdateHealthBar()
    {
        lenghtHealthBar = (lenghtHealthBarMax * (health / maxHealth));
    }
}
