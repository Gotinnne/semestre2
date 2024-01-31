using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Sci_Individu : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject Exit;
    public GameObject Player;

    public Vector3 target;

    // Mode Idle
    public bool IdleActivator;
    private bool CallOnce = false;
    public float RangeIdlemin;
    public float RangeIdlemax;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Exit = GameObject.FindGameObjectWithTag("Exit1");
        agent = GetComponent<NavMeshAgent>();
        target = new Vector3(Random.Range(Exit.GetComponent<Transform>().position.x - 5, Exit.GetComponent<Transform>().position.x + 5), 0, Exit.GetComponent<Transform>().position.z - 1);
        agent.SetDestination(target);
    }
    private void Update()
    {
        // vérification Idle si faux, vérifier si agent arrivé a destination
        if (IdleActivator == false) 
        {
            if (agent.remainingDistance <= 0.2)
            {
                target = new Vector3(Random.Range(Exit.GetComponent<Transform>().position.x - 5, Exit.GetComponent<Transform>().position.x + 5), 0, Exit.GetComponent<Transform>().position.z - 1);
                agent.SetDestination(target);
                CallOnce = false;
            }
        }
        //activation Idle ATTENTION Actuellement IRREVERSIBLE
        if (IdleActivator == true) 
        {
            if (CallOnce == false)
            {
                InvokeRepeating("Idle", 0f, 2.0f);
                CallOnce = true;
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (IdleActivator == true)
        {
            if(collision.gameObject.tag != "Sol")
            {
                Idle();
            }
        }
        if (IdleActivator == false)
        {
            if (collision.gameObject.tag != "Sol")
            {

            }
        }
    }
    void Idle()
    {
        target = new Vector3(Random.Range(this.gameObject.GetComponent<Transform>().position.x + RangeIdlemin, this.gameObject.GetComponent<Transform>().position.x + RangeIdlemax), 1 , Random.Range(this.gameObject.GetComponent<Transform>().position.z + RangeIdlemin, this.gameObject.GetComponent<Transform>().position.z + RangeIdlemax));
        agent.SetDestination(target);
    }
}
