using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//The purpose of this script is to control the behavior of the UI in game and is responsible for some buttons 
public class HUDManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject newHSHUD;
    public GameObject gameOverHUD;
    public AudioSource gameMusic;

    PlayerShooting playerShooting;
    bool isPaused;
    bool gameOver;
    GameObject[] uiElements;

    // Start is called before the first frame update
    void Start()
    {
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerShooting>();
        uiElements = GameObject.FindGameObjectsWithTag("GameActiveUI");
    }

    private void OnEnable()
    {
        EventBroker.PlayerDied += HandlePlayerDead;
    }

    private void OnDisable()
    {
        EventBroker.PlayerDied -= HandlePlayerDead;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            TogglePause();
        }
        
    }

    public void TogglePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        gameMusic.Pause();
        pauseMenu.SetActive(true);
        playerShooting.enabled = false;
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        gameMusic.UnPause();
        pauseMenu.SetActive(false);
        playerShooting.enabled = true;
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Restart()
    {
        EventBroker.RestartGame();
    }

    public void HandlePlayerDead()
    {
        if (ScoreManager.Instance.newHighscore)
        {
            newHSHUD.SetActive(true);
        }
        else
        {
            gameOverHUD.SetActive(true);
        }

        gameOver = true;
        gameMusic.Stop();
        foreach(GameObject obj in uiElements)
        {
            obj.SetActive(false);
        }
    }
}
