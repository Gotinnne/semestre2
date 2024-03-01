using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public enum TypeAgent
{
    Bleu, Rouge
};

public class Sci_Individu_Gnip : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject exit;
    public Vector3 target;
    private bool followVerif;

    public float dgtMinion;
    private float timerAttack;
    public float maxTimerAttack;

    public bool targetEnnemiVerif = false;
    public Vector3 targetEnnemi;
    private GameObject ennemiToAttack;

    public bool baliseVerif;

    public TypeAgent WhoAttack;
    void Start()
    {
        exit = GameObject.Find("Spawner_"+ WhoAttack.ToString());
        agent = GetComponent<NavMeshAgent>();

        target = new Vector3(Random.Range(exit.GetComponent<Transform>().position.x - 5, exit.GetComponent<Transform>().position.x + 5), 0, exit.GetComponent<Transform>().position.z - 1);
        agent.SetDestination(target);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == WhoAttack.ToString() && targetEnnemiVerif == false)
        {
            ennemiToAttack = other.gameObject;
            Attack();
            targetEnnemiVerif = true;
            followVerif = false;
            baliseVerif = false;
        }
        if (other.gameObject.name == "Balise_" + this.gameObject.tag)
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
        if (collision.gameObject.tag == WhoAttack.ToString())
        {

            timerAttack = timerAttack + Time.deltaTime;
            if (timerAttack >= maxTimerAttack)
            {
                GameObject EnnemiGameObject = collision.gameObject;
                Sci_Health_Gnip healthComponent = EnnemiGameObject.GetComponent<Sci_Health_Gnip>();
                if (healthComponent.health - dgtMinion <= 0)
                {
                    healthComponent.InflictDgt(dgtMinion);
                    targetEnnemiVerif = false;
                    baliseVerif = false;
                }
                else
                {
                    healthComponent.InflictDgt(dgtMinion);
                }
                timerAttack = 0;
            }
        }
    }
    public void Follow()
    {
        //call target
        target = new Vector3(Random.Range(exit.GetComponent<Transform>().position.x - 5, exit.GetComponent<Transform>().position.x + 5), 0, exit.GetComponent<Transform>().position.z - 1);
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