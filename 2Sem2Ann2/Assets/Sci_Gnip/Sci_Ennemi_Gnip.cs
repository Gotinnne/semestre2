using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Sci_Ennemi_Gnip : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject Exit;
    public Vector3 target;
    private bool followVerif;

    public float DgtIndividu;
    private float timerAttack;
    public float maxTimerAttack;

    public bool targetEnnemiVerif = false;
    public Vector3 targetEnnemi;
    private GameObject ennemiToAttack;

    public bool baliseVerif;

    void Start()
    {
        Exit = GameObject.FindGameObjectWithTag("Exit2");
        agent = GetComponent<NavMeshAgent>();

        target = new Vector3(Random.Range(Exit.GetComponent<Transform>().position.x - 5, Exit.GetComponent<Transform>().position.x + 5), 0, Exit.GetComponent<Transform>().position.z - 1);
        agent.SetDestination(target);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Individu" && targetEnnemiVerif == false)
        {
            ennemiToAttack = other.gameObject;
            Attack();
            targetEnnemiVerif = true;
            followVerif = false;
            baliseVerif = false;
        }
        if (other.gameObject.tag == "Balise")
        {
            baliseVerif = true;
        }
    }
    private void Update()
    {
        if (targetEnnemiVerif == true)
        {
            Attack();
        }
        if (ennemiToAttack == null && followVerif == false)
        {
            Follow();
            followVerif = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Individu")
        {
            timerAttack = timerAttack + Time.deltaTime;
            if (timerAttack >= maxTimerAttack)
            {
                GameObject EnnemiGameObject = collision.gameObject;
                Sci_Health_Gnip healthComponent = EnnemiGameObject.GetComponent<Sci_Health_Gnip>();
                if (healthComponent.health - DgtIndividu <= 0)
                {
                    healthComponent.InflictDgt(DgtIndividu);
                    targetEnnemiVerif = false;
                }
                else
                {
                    healthComponent.InflictDgt(DgtIndividu);
                }
                timerAttack = 0;
            }
        }
    }

    public void Follow()
    {
        target = new Vector3(Random.Range(Exit.GetComponent<Transform>().position.x - 5, Exit.GetComponent<Transform>().position.x + 5), 0, Exit.GetComponent<Transform>().position.z - 1);
        agent.SetDestination(target);
        followVerif = true;
    }
    public void Attack()
    {
        if (ennemiToAttack == null)
        {
            targetEnnemiVerif = false;
        }
        else
        {
            target = new Vector3(ennemiToAttack.transform.position.x, 0, ennemiToAttack.transform.position.z);
            agent.SetDestination(target);
        }
    }
}