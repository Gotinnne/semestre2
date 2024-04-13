using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
public enum TypeAgent
{
    Bleu, Rouge
};

public class Sci_Individu_Gnip : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject exit;
    public Vector3 target;
    public bool followVerif;

    public float dgtMinion;
    public float timer;
    public float maxTimer = 2;
    public float distanceAttaque = 2;

    public bool targetEnnemiVerif = false;
    public Vector3 targetEnnemi;
    public GameObject ennemiToAttack;

    public bool baliseVerif;

    public bool spawnerIsNear;
    public float distanceSpawnerEnnemy = 5;

    public TypeAgent WhoAttack;

    //Définition des états possibles dans un enum
    public enum State
    {
        Follow,
        Balise,
        Attack
    }

    //Variable qui contient la valeur du state actuel :
    public State currentState = State.Follow;

    void Start()
    {
        exit = GameObject.Find("Spawner_"+ WhoAttack.ToString());
        agent = GetComponent<NavMeshAgent>();
        ChangeState(State.Follow);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == WhoAttack.ToString() && targetEnnemiVerif == false && spawnerIsNear == false)
        {
            ennemiToAttack = other.gameObject;
            targetEnnemiVerif = true;
            followVerif = false;
            baliseVerif = false;
        }
        else if (ennemiToAttack == null && targetEnnemiVerif == true)
        {

        }
        if (other.gameObject.tag == "Balise_" + this.gameObject.tag)
        {
            baliseVerif = true;
        }
        if(other.gameObject.name == "Player_" + this.gameObject.tag && other.gameObject.GetComponent<Sci_PlayerController_Gnip>().modeBaliseVerif == true)
        {
            baliseVerif = true;
        }
        if (other.gameObject.name == "Player_" + this.gameObject.tag && other.gameObject.GetComponent<Sci_PlayerController_Gnip>().modeBaliseVerif == false)
        {
            baliseVerif = false;
        }
        if (agent.remainingDistance <= distanceSpawnerEnnemy && currentState == State.Follow && other.gameObject.name == exit.name)
        {
            spawnerIsNear = true;
            ennemiToAttack = exit;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, distanceAttaque);
    }

    void Update()
    {
        //Update State
        switch (currentState)
        {
            case State.Follow:
                UpdateFollow();
                break;
            case State.Balise:
                UpdateBalise();
                break;
            case State.Attack:
                UpdateAttack();
                break;
        }
    }

    public void ChangeState(State newState)
    {
        //Exit current State
        switch (currentState)
        {
            case State.Follow:
                ExitFollow();
                break;
            case State.Balise:
                ExitBalise();
                break;
            case State.Attack:
                ExitAttack();
                break;
        }

        //Change currentState to newState
        currentState = newState;

        //Enter new State
        switch (currentState)
        {
            case State.Follow:
                EnterFollow();
                break;
            case State.Balise:
                EnterBalise();
                break;
            case State.Attack:
                EnterAttack();
                break;
        }
    }
        
    #region Follow
    private void EnterFollow()
    {
        target = new Vector3(Random.Range(exit.GetComponent<Transform>().position.x - 1, exit.GetComponent<Transform>().position.x + 1), 0, exit.GetComponent<Transform>().position.z - 1);
    }

    private void UpdateFollow()
    {
        if(baliseVerif == true)
        {
            ChangeState(State.Balise);
        }
        if (targetEnnemiVerif == true)
        {
            ChangeState(State.Attack);
            targetEnnemiVerif = false;
        }
        if(spawnerIsNear == true)
        {
            timer = timer + Time.deltaTime;
            agent.isStopped = true;
            GameObject EnnemiGameObject = ennemiToAttack;
            Sci_Health_Gnip healthComponent = EnnemiGameObject.GetComponent<Sci_Health_Gnip>();
            if (timer >= maxTimer)
            {
                healthComponent.InflictDgt(dgtMinion);
                timer = 0;
            }
        }
        if (target == null)
        {
            // the spawner is Dead
            spawnerIsNear = false;
            agent.isStopped = false;
        }
        else
        {
            agent.SetDestination(target);
            followVerif = true;
        }
    }

    private void ExitFollow()
    {
        //todo
    }
    #endregion

    #region Balise
    private void EnterBalise()
    {
        //todo
    }

    private void UpdateBalise()
    {
        if (baliseVerif == false)
        {
            ChangeState(State.Follow);
        }
        if (targetEnnemiVerif == true)
        {
            ChangeState(State.Attack);
        }
    }

    private void ExitBalise()
    {
        //todo
    }
    #endregion

    #region Attack
    private void EnterAttack()
    {
        //todo
    }
    private void UpdateAttack()
    {

        if (ennemiToAttack == null || spawnerIsNear == true)
        {
            targetEnnemiVerif = false;
            agent.isStopped = false;
            ChangeState(State.Follow);
        }
        else
        {
            target = new Vector3(ennemiToAttack.transform.position.x, 0, ennemiToAttack.transform.position.z);
            agent.SetDestination(target);
        }
        if(targetEnnemiVerif == true)
        {
            timer = timer + Time.deltaTime;
            if(distanceAttaque >= agent.remainingDistance)
            {
                agent.isStopped = true;
                GameObject EnnemiGameObject = ennemiToAttack;
                Sci_Health_Gnip healthComponent = EnnemiGameObject.GetComponent<Sci_Health_Gnip>();
                if (timer >= maxTimer)
                {
                    healthComponent.InflictDgt(dgtMinion);
                    timer = 0;
                }
            }
        }
    }
    private void ExitAttack()
    {
        //todo
    }
    #endregion
}
