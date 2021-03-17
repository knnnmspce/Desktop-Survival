using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//The purpose of this script is to control the UI behavior f the game over menu when the player has died
public class GameOverManager : MonoBehaviour
{
    Text scoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        scoreTxt = GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = "Your Score: " + ScoreManager.Instance.GetScore();
    }

    public void Restart()
    {
        GameManager.Instance.Restart();
    }

    public void GoToMainMenu()
    {
        GameObject gameManager;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        Destroy(gameManager);

        SceneManager.LoadSceneAsync("MainMenu");
    }
}
