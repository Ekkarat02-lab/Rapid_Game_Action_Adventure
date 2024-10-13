using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSystem : MonoBehaviour
{
    [SerializeField] private string newScene;
    public void NewGame()
    {
        SceneManager.LoadScene(newScene);
    }
    public void TurnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
