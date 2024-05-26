using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Sci_Health_Gnip : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private Transform healthBar;

    public float shield;
    public float maxShield = 2;
    public bool shieldActif;

    public float maxTimerSpawn;
    public float timerSpawn;

    public float maxTimerPlayer;
    public float timerPlayer;
    public SpriteRenderer PlayerSprite;
    //TimerMort_Bleu
    public TextMeshProUGUI TimerText_Bleu;
    public Image Contour_Bleu;
    public Image Fill_Bleu;
    //TimerMort_Rouge
    public TextMeshProUGUI TimerText_Rouge;
    public Image Contour_Rouge;
    public Image Fill_Rouge;

    public float lenghtHealthBar = 5;
    private float lenghtHealthBarMax;
    public GameObject Barre;
    public bool BoolSpawn = true;
    public Vector3 RespawnPoint;
    public bool IsDead= false;

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
    //MinionsEffect
    public GameObject DeathMinion;

    //QueenEffect
    public GameObject DgtEffectQueen;
    public GameObject DgtUpEffectQueen;
    public GameObject HealEffectQueen;
    public GameObject BouclierEffectQueen;
    public GameObject QueenBase;
    //PlayerEffect
    public GameObject DeathScreenRed;
    public GameObject DeathScreenBlue;

    //winmenu
    public GameObject BoutonRestart;
    public GameObject BoutonMenu;
    public GameObject WinBleu;
    public GameObject WinRouge;

    //sons
    private FMOD.Studio.EventInstance event_DestructionSpawner;
    public int MinionsAutour = 0;

    void Start()
    {
    if(gameObject.tag == "spawner")
        {
            event_DestructionSpawner = FMODUnity.RuntimeManager.CreateInstance("event:/autres/Destruction_Spawner");
            event_DestructionSpawner.start();
        }
        RespawnPoint = transform.position;
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
        if(gameObject.tag == "spawner")
        {
            MinionsAutour = this.gameObject.GetComponent<Sci_SpawnTrigger_Gnip>().MinionsAutour;
            event_DestructionSpawner.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            event_DestructionSpawner.setParameterByName("NumMinions", MinionsAutour); 
        }
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
            if (IsDead == true)
            {
                if (gameObject.tag != "spawner" && gameObject.name != "Player_Rouge" && gameObject.name != "Player_Bleu" && gameObject.name != "Queen_Bleu" && gameObject.name != "Queen_Rouge")
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Minion/Mort_Minion", GetComponent<Transform>().position);
                    Destroy(gameObject);
                }
                if (gameObject.name == "Player_Rouge" || gameObject.name == "Player_Bleu")
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/MortPlayer", GetComponent<Transform>().position);
                }
                if (gameObject.tag == "spawner")
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/autres/Destruction_Spawner", GetComponent<Transform>().position);
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
                IsDead = true;
                if (timerSpawn >= maxTimerSpawn)
                {
                    IsDead = false;
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
                this.gameObject.GetComponent<Transform>().position = RespawnPoint;
                Sci_PlayerController_Gnip PlayerCode = this.gameObject.GetComponent<Sci_PlayerController_Gnip>();
                PlayerSprite.enabled = false;
                PlayerCode.enabled = false;
                if (gameObject.name == "Player_Rouge")
                {
                    Fill_Rouge.GetComponent<Image>().enabled = true;
                    Contour_Rouge.GetComponent<Image>().enabled = true;
                    TimerText_Rouge.enabled = true;
                    DeathScreenRed.gameObject.SetActive(true);
                    float TimerShown = maxTimerPlayer - timerPlayer + 1;
                    TimerText_Rouge.text = "" + (int)timerPlayer;
                    Fill_Rouge.fillAmount = timerPlayer / maxTimerPlayer;
                }
                if (gameObject.name == "Player_Bleu")
                {
                    Fill_Bleu.GetComponent<Image>().enabled = true;
                    Contour_Bleu.GetComponent<Image>().enabled = true;
                    TimerText_Bleu.enabled = true;
                    DeathScreenBlue.gameObject.SetActive(true);
                    float TimerShown =  maxTimerPlayer - timerPlayer +1;
                    TimerText_Bleu.text = "" + (int)TimerShown;
                    Fill_Bleu.fillAmount = timerPlayer / maxTimerPlayer;
                }
                IsDead = true;
                timerPlayer = Time.deltaTime + timerPlayer;
                if (timerPlayer >= maxTimerPlayer)
                {
                    IsDead = false;
                    if (gameObject.name == "Player_Rouge")
                    {
                        Fill_Rouge.GetComponent<Image>().enabled = false;
                        Contour_Rouge.GetComponent<Image>().enabled = false;
                        TimerText_Rouge.enabled = false;
                        DeathScreenRed.gameObject.SetActive(false);
                    }
                    if (gameObject.name == "Player_Bleu")
                    {
                        Fill_Bleu.GetComponent<Image>().enabled = false;
                        Contour_Bleu.GetComponent<Image>().enabled = false;
                        TimerText_Bleu.enabled = false;
                        DeathScreenBlue.gameObject.SetActive(false);
                    }
                    
                    health = maxHealth;
                    this.gameObject.GetComponent<Collider>().enabled = true;
                    this.gameObject.GetComponent<Sci_PlayerController_Gnip>().enabled = true;
                    PlayerSprite.enabled = true;
                    timerPlayer = 0;
                    maxTimerPlayer = maxTimerPlayer + 5;
                }
            }
            if(gameObject.tag != "spawner" && gameObject.name != "Player_Rouge" && gameObject.name != "Player_Bleu" && gameObject.name != "Queen_Bleu" && gameObject.name != "Queen_Rouge")
            {
                IsDead = true;
            }
            //win
            if(gameObject.name == "Queen_Bleu")
            {
                //winRouge
                BoutonRestart.SetActive(true);
                BoutonMenu.SetActive(true);
                WinRouge.SetActive(true);
                Time.timeScale = 0;
            }
            if (gameObject.name == "Queen_Rouge")
            {
                //winBleu
                BoutonRestart.SetActive(true);
                BoutonMenu.SetActive(true);
                WinBleu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    public void OnDestroy()
    {
        DeathMinion.GetComponent<SpriteRenderer>().enabled = true;
        TimeEffectmax = 0.5f;
        EffectBool = true;
    }
    public void InflictDgt(float Dgt)
    {
        if(shieldActif == true)
        {
            shield = shield - Dgt;
            if(shield <= maxShield * 0.7)
            {
                Bouclier1GOdgt.GetComponent<SpriteRenderer>().enabled = true;
                Bouclier1GO.GetComponent<SpriteRenderer>().enabled = false;
                Bouclier2GO.GetComponent<SpriteRenderer>().enabled = true;
                EffectBool = true;
            }
            if (shield <= maxShield * 0.3)
            {
                Bouclier2GOdgt.GetComponent<SpriteRenderer>().enabled = true;
                Bouclier2GO.GetComponent<SpriteRenderer>().enabled = false;
                Bouclier3GO.GetComponent<SpriteRenderer>().enabled = true;
                EffectBool = true;
            }
            if (shield <= 0)
            {
                Bouclier3GOdgt.GetComponent<SpriteRenderer>().enabled = true;
                Bouclier3GO.GetComponent<SpriteRenderer>().enabled = false;
                Bouclier2GO.GetComponent<SpriteRenderer>().enabled = false;
                Bouclier1GO.GetComponent<SpriteRenderer>().enabled = false;
                health = health + shield;
                EffectBool = true;
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
