using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan: MonoBehaviour
{

    //Script qui gere les scenes

    public void MainMenu ()
    {
        SceneManager.LoadScene(0);
        print("reloading");
    }

    public void Exit ()
    {
        Application.Quit();
        print("exiting");
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
