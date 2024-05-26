using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_SpawnerBPupper_Gnip : MonoBehaviour
{
    public Sci_BaliseSpawner_Gnip SpawnCode;

    public int BaliseForcer;

    void Update()
    {
        SpawnCode.randBalise = BaliseForcer;
    }
}
