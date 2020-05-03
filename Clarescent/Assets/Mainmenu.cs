using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{

    public void StartYO()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitYO()
    {
        Application.Quit();
    }

    public void Restartyo()
    {
        SceneManager.LoadScene(1);
    }

}
