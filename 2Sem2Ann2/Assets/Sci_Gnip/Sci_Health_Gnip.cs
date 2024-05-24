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
    public bool BoolSpawn = true;


    //healEffect
    public GameObject HealEffectGO;
    public float TimeEffectmax = 0.2f;
    private float TimerEffect = 0.0f;
    private bool EffectBool = false;
    //DgtEffect
    public GameObject DgtEffectGO;
    //Bouclier
    public GameObject BouclierEffectGO;
    public GameObject Bouclier1GO;
    public GameObject Bouclier2GO;
    public GameObject Bouclier3GO;
    public GameObject Bouclier1GOdgt;
    public GameObject Bouclier2GOdgt;
    public GameObject Bouclier3GOdgt;
    //BaliseDGT
    public GameObject DgtUpEffectGO;
    //QueenEffect
    public GameObject DgtEffectQueen;
    public GameObject DgtUpEffectQueen;
    public GameObject HealEffectQueen;
    public GameObject BouclierEffectQueen;
    public GameObject QueenBase;
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
                Debug.LogError(gameObject);
            }
        }
        health = maxHealth;
        lenghtHealthBarMax = lenghtHealthBar;
    }
    void Update()
    {

        //Update
        if (this.name == "Queen_Bleu" || this.name == "Queen_Rouge")
        {
            UpdateHealthBar();
            Barre.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lenghtHealthBar);
        }
        if (this.name != "Queen_Bleu" && this.name != "Queen_Rouge")
        {
            UpdateHealthBar();
            healthBar.localScale = new Vector3(lenghtHealthBar, 1, 1);
        }

        //effets
        if (EffectBool == true)
        {
            TimerEffect = TimerEffect + Time.deltaTime;
        }
        if (TimerEffect >= TimeEffectmax && EffectBool == true)
        {
            DgtEffectGO.GetComponent<SpriteRenderer>().enabled = false;
            if(gameObject.tag != "spawner")
            {
                BouclierEffectGO.GetComponent<SpriteRenderer>().enabled = false;
                HealEffectGO.GetComponent<SpriteRenderer>().enabled = false;
                DgtUpEffectGO.GetComponent<SpriteRenderer>().enabled = false;
                Bouclier1GOdgt.GetComponent<SpriteRenderer>().enabled = false;
                Bouclier2GOdgt.GetComponent<SpriteRenderer>().enabled = false;
                Bouclier3GOdgt.GetComponent<SpriteRenderer>().enabled = false;
                //queenreset
                if (gameObject.name == "Queen_Bleu" || gameObject.name == "Queen_Rouge")
                {
                    QueenBase.GetComponent<SpriteRenderer>().enabled = true;
                    BouclierEffectQueen.GetComponent<SpriteRenderer>().enabled = false;
                    HealEffectQueen.GetComponent<SpriteRenderer>().enabled = false;
                    DgtEffectQueen.GetComponent<SpriteRenderer>().enabled = false;
                    DgtUpEffectQueen.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            EffectBool = false;
            TimerEffect = 0;
            TimeEffectmax = 0.2f;
        }



        //vérification mort de l'entité
        if (health <= 0) 
        {
            if (gameObject.tag == "spawner")
            {
                //si entité un spawner désactiver durant timer
                Sci_Spawn_Gnip SpawnerCode = this.gameObject.GetComponent<Sci_Spawn_Gnip>();
                SpawnerCode.SpawnerDetruit.GetComponent<SpriteRenderer>().enabled = true;
                SpawnerCode.enabled = false;
                BoolSpawn = false;
                timerSpawn = Time.deltaTime + timerSpawn;
                if(timerSpawn >= maxTimerSpawn)
                {
                    timerSpawn = 0;
                    BoolSpawn = true;
                    health = maxHealth;
                    this.gameObject.GetComponent<Collider>().enabled = true;
                    this.gameObject.GetComponent<Sci_Spawn_Gnip>().enabled = true;
                    SpawnerCode.SpawnerDetruit.GetComponent<SpriteRenderer>().enabled = false;
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
            if(gameObject.tag != "spawner" && gameObject.name != "Player_Rouge" && gameObject.name != "Player_Bleu")
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
            if(shield <= shield*0.7)
            {
                Bouclier1GOdgt.GetComponent<SpriteRenderer>().enabled = true;
                Bouclier1GO.GetComponent<SpriteRenderer>().enabled = false;
                Bouclier2GO.GetComponent<SpriteRenderer>().enabled = true;
                EffectBool = true;
            }
            if (shield <= shield * 0.35)
            {
                Bouclier2GOdgt.GetComponent<SpriteRenderer>().enabled = true;
                Bouclier2GO.GetComponent<SpriteRenderer>().enabled = false;
                Bouclier3GO.GetComponent<SpriteRenderer>().enabled = true;
                EffectBool = true;
            }
            if (shield < 0)
            {
                Bouclier3GOdgt.GetComponent<SpriteRenderer>().enabled = true;
                Bouclier3GO.GetComponent<SpriteRenderer>().enabled = false;
                Bouclier2GO.GetComponent<SpriteRenderer>().enabled = false;
                Bouclier1GO.GetComponent<SpriteRenderer>().enabled = false;
                health = health + shield;
                shieldActif = false;
                UpdateHealthBar();
                DGTEffect();
            }
            else if (shield == 0)
            {
                shieldActif = false;
            }
        }
        else if (shieldActif == false)
        {
            health = health - Dgt;
            DGTEffect();
            UpdateHealthBar();
        }
    }
    public void UpdateHealthBar()
    {
        lenghtHealthBar = (lenghtHealthBarMax * (health / maxHealth));

    }
    public void HealEffect()
    {
        HealEffectGO.GetComponent<SpriteRenderer>().enabled = true;
        if (gameObject.name == "Queen_Bleu" || gameObject.name == "Queen_Rouge")
        {
            QueenBase.GetComponent<SpriteRenderer>().enabled = false;
            HealEffectQueen.GetComponent<SpriteRenderer>().enabled = true;
        }
        EffectBool = true;
    }
    public void DGTEffect()
    {
        DgtEffectGO.GetComponent<SpriteRenderer>().enabled = true;
        if (gameObject.name == "Queen_Bleu" || gameObject.name == "Queen_Rouge")
        {
            QueenBase.GetComponent<SpriteRenderer>().enabled = false;
            DgtEffectQueen.GetComponent<SpriteRenderer>().enabled = true;
        }
        EffectBool = true;
    }
    public void DgtUpEffect()
    {
        DgtUpEffectGO.GetComponent<SpriteRenderer>().enabled = true;
        EffectBool = true;
        TimeEffectmax = 3;
        if(gameObject.name == "Queen_Bleu" || gameObject.name == "Queen_Rouge")
        {
            QueenBase.GetComponent<SpriteRenderer>().enabled = false;
            DgtUpEffectQueen.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void DgtUpEffectFalse()
    {
        DgtUpEffectGO.GetComponent<SpriteRenderer>().enabled = false;
        if (gameObject.name == "Queen_Bleu" || gameObject.name == "Queen_Rouge")
        {
            QueenBase.GetComponent<SpriteRenderer>().enabled = false;
            DgtUpEffectQueen.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void BouclierEffect()
    {
        BouclierEffectGO.GetComponent<SpriteRenderer>().enabled = true;
        if (gameObject.name == "Queen_Bleu" || gameObject.name == "Queen_Rouge")
        {
            QueenBase.GetComponent<SpriteRenderer>().enabled = false;
            BouclierEffectQueen.GetComponent<SpriteRenderer>().enabled = true;
        }
        EffectBool = true;
    }
}
