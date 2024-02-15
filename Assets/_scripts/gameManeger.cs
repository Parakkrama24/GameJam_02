using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManeger : MonoBehaviour
{
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameInPanel;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private AudioClip backgroundClip;
    private PlayerController playerController;
    private AudioSource BackgroundSfx;
    private int Points;
    private void Awake()
    {
        Time.timeScale = 0f;
        playerController = FindObjectOfType<PlayerController>();
        BackgroundSfx = GetComponent<AudioSource>();
    }
    private void Start()
    {
       // Debug.Log("PlayMusic");
        BackgroundSfx.Play();
    }
    void Update()
    {

        Points = playerController.points;
        float playerHealth = playerController.health;
        if (playerHealth <= 0f)
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        healthSlider.value = playerHealth;

        if (Points >=5)
        {
            MainPanel.SetActive(false);
            GameOverPanel.SetActive(false); 
           winPanel.SetActive(true);
            gameInPanel.SetActive(false) ;
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
        winPanel.SetActive(false );
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
