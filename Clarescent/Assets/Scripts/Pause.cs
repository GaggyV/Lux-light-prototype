using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] InputHandler inputHandler;
    void Update()
    {
        if (inputHandler.PauseButton.enter)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0.001f;
                canvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
                Resume();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}