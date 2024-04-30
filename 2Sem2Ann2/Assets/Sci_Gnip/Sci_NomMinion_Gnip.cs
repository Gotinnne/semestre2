using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Sci_NomMinion_Gnip : MonoBehaviour
{
    public Dictionary<int, GameObject> objectsInTrigger = new Dictionary<int, GameObject>();
    public KeyCode keyNom = KeyCode.R;
    public Sci_PlayerController_Gnip PlayerController;
    private GameObject randomObject;
    private Timer timer;
    void Update()
    {
        if(objectsInTrigger.Count > 0 && Input.GetKeyDown(keyNom))
        {
            Debug.Log("get key down");
            int MinionRan = UnityEngine.Random.Range(0, objectsInTrigger.Count);
            MinionRan = objectsInTrigger.Keys.ElementAt(MinionRan);
            objectsInTrigger.TryGetValue(MinionRan, out randomObject);

            Destroy(randomObject);
            objectsInTrigger.Remove(MinionRan);
            PlayerController.canAttack = true;
            PlayerController.multiplierAttack = PlayerController.multiplierAttack + 1;
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
