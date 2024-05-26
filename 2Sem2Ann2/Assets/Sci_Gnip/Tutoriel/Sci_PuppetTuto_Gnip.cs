using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_PuppetTuto_Gnip : MonoBehaviour
{
    public Sci_Health_Gnip healthCode;

    private void Start()
    {
        healthCode = this.gameObject.GetComponent<Sci_Health_Gnip>();
    }
    // Update is called once per frame
    void Update()
    {
        if(healthCode.health <= 0)
        {
            healthCode.health = healthCode.maxHealth;
        }
    }
    
}
