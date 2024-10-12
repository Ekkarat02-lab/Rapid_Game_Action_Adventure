using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceSystem : MonoBehaviour
{
      
    public bool isGameOver;
    public object gameOverUI;
          

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void TurnToSeene(string sceneName)
    {

        name = sceneName;
        SceneManager.LoadScene(name);
    }
    public void TurnMainMenu()
    {

        SceneManager.LoadScene("Menu Sence");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
