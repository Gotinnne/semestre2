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

    private void Start()
    {
        CamBleu = GameObject.Find("Camera_Bleu");
        CamRouge = GameObject.Find("Camera_Rouge");
    }
    void Update()
    {
        timeRemaining = timeRemaining + Time.deltaTime;
        if (timeRemaining >= maxTime && VerifSpawn == false)
        {
            Destroy(lastBalise);
            randBalise = Random.Range(1, 4);
            if(randBalise == 1)
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
            Spawn();
            VerifSpawn = true;
            timeRemaining = 0;
        }
        if(Player != null)
        {
            if (timeRemaining >= maxTimePlayer && objectsInTrigger.Count > 0 && Player.GetComponent<Sci_PlayerController_Gnip>().Balise3 == null || Player.GetComponent<Sci_PlayerController_Gnip>().Balise2 == null)
            {
                Give(Player);
            }
        }
    }

    void Spawn()
    {
        switch (choixBalise)
        {
            case State.BaliseAttaque:
                lastBalise = Instantiate(BaliseAttaqueGO, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
                break;
            case State.BaliseBouclier:
                lastBalise = Instantiate(BaliseBouclierGO, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
                break;
            case State.BaliseRegen:
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
                    Destroy(lastBalise);
                    VerifSpawn = false;
                    timeRemaining = 0;
                }
            }
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
