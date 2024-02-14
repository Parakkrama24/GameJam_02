using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManeger : MonoBehaviour
{
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject GameOverPanel;
    private PlayerController playerController;
    private void Awake()
    {
        Time.timeScale = 0f;
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
      

        float playerHealth = playerController.health;
        if (playerHealth <= 0f)
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    public void play()
    {
        Time.timeScale = 1f;
        MainPanel.SetActive(false);


    }

    public void Reset()
    {
        GameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     //   playerController.health = 100f;
    }
}
