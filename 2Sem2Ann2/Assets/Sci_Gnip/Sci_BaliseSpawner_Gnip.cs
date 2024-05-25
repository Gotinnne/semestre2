using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Sci_Inventaire_Gnip;

public class Sci_BaliseSpawner_Gnip : MonoBehaviour
{
    private Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();
    public enum State
    {
        BaliseAttaque,
        BaliseBouclier,
        BaliseRegen
    }
    public State choixBalise;

    public GameObject BaliseAttaqueSP1;
    public GameObject BaliseBouclierSP1;
    public GameObject BaliseRegenSP1;

    public GameObject BaliseAttaqueSP2;
    public GameObject BaliseBouclierSP2;
    public GameObject BaliseRegenSP2;

    public GameObject BaliseAttaqueGO;
    public GameObject BaliseBouclierGO;
    public GameObject BaliseRegenGO;

    public GameObject BaliseAttaqueBleu;
    public GameObject BaliseBouclierBleu;
    public GameObject BaliseRegenBleu;

    public GameObject BaliseAttaqueRouge;
    public GameObject BaliseBouclierRouge;
    public GameObject BaliseRegenRouge;

    //timer temps de spawn d'une balise
    public float timeRemaining = 0;
    public float maxTime;
    public float maxTimePlayer;
    public bool VerifSpawn;
    public int randBalise;

    public GameObject Player;
    public GameObject lastBalise;

    public GameObject CamBleu;
    public GameObject CamRouge;

    private bool ChoiceVerif;
    private bool Spawn1bool;
    private bool Spawn2bool;

    private void Start()
    {
        CamBleu = GameObject.Find("Camera_Bleu");
        CamRouge = GameObject.Find("Camera_Rouge");
    }
    void Update()
    {
        timeRemaining = timeRemaining + Time.deltaTime;

        if(timeRemaining >= maxTime * 0.5 && Spawn1bool == false)
        {
            Destroy(lastBalise);
            Spawn1();
            Spawn1bool = true;
        }
        if (timeRemaining >= maxTime * 0.8 && Spawn2bool == false)
        {
            Spawn2();
            Spawn2bool = true;
        }
        if (timeRemaining >= maxTime && VerifSpawn == false)
        {
            VerifSpawn = true;
            Spawn();
        }
        if(ChoiceVerif == false)
        {
            randBalise = Random.Range(1, 4);
            if (randBalise == 1)
            {
                choixBalise = State.BaliseAttaque;
            }
            else if (randBalise == 2)
            {
                choixBalise = State.BaliseBouclier;
            }
            else if (randBalise == 3)
            {
                choixBalise = State.BaliseRegen;
            }
            ChoiceVerif = true;
        }
        if(Player != null)
        {
            if (timeRemaining >= maxTimePlayer && objectsInTrigger.Count > 0 && Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 == null || Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 == null)
            {
                Give(Player);
            }
        }
    }
    void Spawn1()
    {
        switch (choixBalise)
        {
            case State.BaliseAttaque:
                BaliseAttaqueSP1.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case State.BaliseBouclier:
                BaliseBouclierSP1.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case State.BaliseRegen:
                BaliseRegenSP1.GetComponent<SpriteRenderer>().enabled = true;
                break;
        }
    }
    void Spawn2()
    {
        switch (choixBalise)
        {
            case State.BaliseAttaque:
                BaliseAttaqueSP1.GetComponent<SpriteRenderer>().enabled = false;
                BaliseAttaqueSP2.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case State.BaliseBouclier:
                BaliseBouclierSP1.GetComponent<SpriteRenderer>().enabled = false;
                BaliseBouclierSP2.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case State.BaliseRegen:
                BaliseRegenSP1.GetComponent<SpriteRenderer>().enabled = false;
                BaliseRegenSP2.GetComponent<SpriteRenderer>().enabled = true;
                break;
        }
    }
    void Spawn()
    {
        switch (choixBalise)
        {
            case State.BaliseAttaque:
                BaliseAttaqueSP2.GetComponent<SpriteRenderer>().enabled = false;
                lastBalise = Instantiate(BaliseAttaqueGO, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
                break;
            case State.BaliseBouclier:
                BaliseBouclierSP2.GetComponent<SpriteRenderer>().enabled = false;
                lastBalise = Instantiate(BaliseBouclierGO, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
                break;
            case State.BaliseRegen:
                BaliseRegenSP2.GetComponent<SpriteRenderer>().enabled = false;
                lastBalise = Instantiate(BaliseRegenGO, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
                break;
        }
    }
    void Give(GameObject Player)
    {
        if(objectsInTrigger.Count > 0 && VerifSpawn == true)
        {
            if (Player.name == "Player_Bleu")
            {
                if (Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 == null && Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 != null)
                {
                    switch (choixBalise)
                    {
                        case State.BaliseAttaque:

                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 = BaliseAttaqueBleu;
                            CamBleu.GetComponent<Sci_Inventaire_Gnip>().Balise_3_State = Balise_3.Atk;
                            break;
                        case State.BaliseBouclier:
                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 = BaliseBouclierBleu;
                            CamBleu.GetComponent<Sci_Inventaire_Gnip>().Balise_3_State = Balise_3.Bouc;
                            break;
                        case State.BaliseRegen:
                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 = BaliseRegenBleu;
                            CamBleu.GetComponent<Sci_Inventaire_Gnip>().Balise_3_State = Balise_3.Regen;
                            break;
                    }
                    Destroy(lastBalise);
                    VerifSpawn = false;
                    timeRemaining = 0;
                }
                else if (Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 == null && Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 == null || Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 == null && Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 != null)
                {
                    switch (choixBalise)
                    {
                        case State.BaliseAttaque:
                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 = BaliseAttaqueBleu;
                            CamBleu.GetComponent<Sci_Inventaire_Gnip>().Balise_2_State = Balise_2.Atk;
                            break;
                        case State.BaliseBouclier:
                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 = BaliseBouclierBleu;
                            CamBleu.GetComponent<Sci_Inventaire_Gnip>().Balise_2_State = Balise_2.Bouc;
                            break;
                        case State.BaliseRegen:
                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 = BaliseRegenBleu;
                            CamBleu.GetComponent<Sci_Inventaire_Gnip>().Balise_2_State = Balise_2.Regen;
                            break;
                    }
                    Destroy(lastBalise);
                    VerifSpawn = false;
                    timeRemaining = 0;
                }
            }
            else if (Player.name == "Player_Rouge")
            {
                if (Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 == null && Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 != null)
                {
                    switch (choixBalise)
                    {
                        case State.BaliseAttaque:
                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 = BaliseAttaqueRouge;
                            CamRouge.GetComponent<Sci_Inventaire_Gnip>().Balise_3_State = Balise_3.Atk;
                            break;
                        case State.BaliseBouclier:
                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 = BaliseBouclierRouge;
                            CamRouge.GetComponent<Sci_Inventaire_Gnip>().Balise_3_State = Balise_3.Bouc;
                            break;
                        case State.BaliseRegen:
                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 = BaliseRegenRouge;
                            CamRouge.GetComponent<Sci_Inventaire_Gnip>().Balise_3_State = Balise_3.Regen;
                            break;
                    }
                    Destroy(lastBalise);
                    VerifSpawn = false;
                    timeRemaining = 0;
                }
                else if (Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 == null && Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 == null)
                {
                    switch (choixBalise)
                    {
                        case State.BaliseAttaque:
                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 = BaliseAttaqueRouge;
                            CamRouge.GetComponent<Sci_Inventaire_Gnip>().Balise_2_State = Balise_2.Atk;
                            break;
                        case State.BaliseBouclier:
                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 = BaliseBouclierRouge;
                            CamRouge.GetComponent<Sci_Inventaire_Gnip>().Balise_2_State = Balise_2.Bouc;
                            break;
                        case State.BaliseRegen:
                            Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 = BaliseRegenRouge;
                            CamRouge.GetComponent<Sci_Inventaire_Gnip>().Balise_2_State = Balise_2.Regen;
                            break;
                    }
                }
            }
            timeRemaining = 0;
            VerifSpawn = false;
            Spawn1bool = false;
            Spawn2bool = false;
            ChoiceVerif = false;
            Destroy(lastBalise);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player_Bleu" || other.name == "Player_Rouge")
        {
            //Objet de la couleur de la balise
            if (!objectsInTrigger.ContainsKey(other.GetInstanceID()))
            {
                objectsInTrigger.Add(other.GetInstanceID(), other.gameObject);
                if(Player == null)
                {
                    Player = other.gameObject;
                }
                Give(other.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player_Bleu" || other.name == "Player_Rouge")
        {
            if (objectsInTrigger.ContainsKey(other.GetInstanceID()))
            {
                if(other.gameObject == Player)
                {
                    Player = null;
                }
                objectsInTrigger.Remove(other.GetInstanceID());
            }
        }
    }
}
