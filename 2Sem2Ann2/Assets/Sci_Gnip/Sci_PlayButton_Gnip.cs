using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Sci_PlayButton_Gnip : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Ld_TestQA2");
    }
    public void LoadTuto()
    {
        SceneManager.LoadScene("Tutoriel");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
