using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScenesSystem : MonoBehaviour
{
    [SerializeField] private string newScene;
    [SerializeField] private Transform respawnPlayer;  
    [SerializeField] private GameObject player;  
    private int deathCount = 0;
    [SerializeField] private TextMeshProUGUI destroyCountText;
    public GameObject gameOverUI;
    protected PlayerStats stats;
    void Start()
    {
        UpdateDestroyCountUI();
        stats = FindObjectOfType<PlayerStats>();
    }
    public void NewGame()
    {
        SceneManager.LoadScene(newScene);
    }
    public void TurnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Respawn()
    {
        player.transform.position = respawnPlayer.position;  
        deathCount++;  
        UpdateDestroyCountUI();  
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        stats.CurrentHealth = stats.maxHealth;
    }
    public void UpdateDestroyCountUI()
    {
        destroyCountText.text = "Destroy Count: " + deathCount;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
