using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject canvasWin;
    private bool isWin;
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
        if (GameObject.Find("BossEnemy") == null)
        {
            LoadCanvas(); 
        }
    }

    private void LoadCanvas()
    {
        canvasWin.SetActive(true);
    }
}