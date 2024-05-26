using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sci_ButtonScript_Gnip : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ReLoadGame()
    {
        SceneManager.LoadScene("Ld_TestQA2");
    }

}
