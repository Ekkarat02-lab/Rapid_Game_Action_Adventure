using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CheckForBossEnemy();
    }

    private void CheckForBossEnemy()
    {
        if (GameObject.Find("BossEnemy") == null)
        {
            LoadCreditScene(); 
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void LoadCreditScene()
    {
        LoadScene("Credit");
    }
}