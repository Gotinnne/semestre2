using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

public class Sci_NomMinion_Gnip : MonoBehaviour
{

    public Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();
    public KeyCode keyNom = KeyCode.R;
    public Sci_PlayerController_Gnip PlayerController;
    private GameObject randomObject;
    private Timer timer;

    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;

    void Update()
    {
        if (objectsInTrigger.Count > 0 && Input.GetKeyDown(keyNom))
        {
            int MinionRan = UnityEngine.Random.Range(0, objectsInTrigger.Count);
            MinionRan = objectsInTrigger.Keys.ElementAt(MinionRan);
            objectsInTrigger.TryGetValue(MinionRan, out randomObject);
            PlayerController.canAttack = true;
            PlayerController.multiplierAttack = PlayerController.multiplierAttack + 1;
            Destroy(randomObject);
            objectsInTrigger.Remove(MinionRan);
        }
        if(PlayerController.multiplierAttack == 0)
        {
            Level1.GetComponent<SpriteRenderer>().enabled = false;
            Level2.GetComponent<SpriteRenderer>().enabled = false;
            Level3.GetComponent<SpriteRenderer>().enabled = false;
        }
        if(PlayerController.multiplierAttack >= 1)
        {
            Level1.GetComponent<SpriteRenderer>().enabled = true;
        }
        if( PlayerController.multiplierAttack >= 2)
        {
            Level2.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (PlayerController.multiplierAttack >= 3)
        {
            Level3.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == this.tag)
        {
            if (!objectsInTrigger.ContainsKey(other.GetInstanceID()))
            {
                objectsInTrigger.Add(other.GetInstanceID(), other.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        //permet d'arreter set destination si individu sort de la zone
        if (objectsInTrigger.ContainsKey(other.GetInstanceID()))
        {
            objectsInTrigger.Remove(other.GetInstanceID());
        }
    }
}