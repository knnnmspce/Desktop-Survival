using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//The purpose of this script is to control the behavior of what happens when a button is pressed of hovered over
public class MenuButtonHandler : MonoBehaviour
{
    public Image carrot;
    public AudioSource hoverSound;
    public GameObject quitDialog;
    public Text targetText;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject pausedOptions;
    public GameObject pauseContainer;
    public GameObject hsMenu;
    public GameObject newHSHUD;
    public GameObject endGameHUD;

    private Color hovColor = new Color(1f, 0.6627451f, 0.2627451f);

    private void OnEnable()
    {
        //reset all color text and carrots not in focus
        if(targetText != null)
        {
            targetText.color = Color.white;
        }
        
        if(carrot != null)
        {
            carrot.gameObject.SetActive(false);
        }
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync("LoadWithInstructions");
    }

    public void OpenPausedOptions()
    {
        pausedOptions.SetActive(true);
        pauseContainer.SetActive(false);
    }

    public void BackToPause()
    {
        pausedOptions.SetActive(false);
        pauseContainer.SetActive(true);
    }

    public void ViewHighscores()
    {
        mainMenu.SetActive(false);
        hsMenu.SetActive(true);
    }

    public void Continue()
    {
        newHSHUD.SetActive(false);
        endGameHUD.SetActive(true);
    }

  public void Options()
  {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
  }

  public void Back()
  {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
  }

  public void BackFromHS()
  {
        mainMenu.SetActive(true);
        hsMenu.SetActive(false);
  }

  public void Quit()
  {
        quitDialog.SetActive(true);
  }

  public void CancelQuit()
  {
        quitDialog.SetActive(false);
  }

  public void ConfirmQuit()
  {
        //close the game but make sure the game manager is destroyed
        GameObject gameManager;
        if(gameManager = null)
        {
            Application.Quit();
        }
        else
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
            Destroy(gameManager);
            Application.Quit();
        }        
  }

  public void ConfirmQuitFromGame()
  {
        GameObject gameManager;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        Destroy(gameManager);

        SceneManager.LoadSceneAsync("MainMenu");
  }


  public void OnHoverEnterAction()
  {
        carrot.gameObject.SetActive(true);
        hoverSound.Play();
  }

  public void OnHoverExitAction()
  {
        carrot.gameObject.SetActive(false);
  }

  public void HoverTextColorChange()
  {
        hoverSound.Play();

        if (targetText != null)
        {
            targetText.color = hovColor;
        }
        else
        {
            return;
        }
  }

  public void OffTextColorChange()
  {
        if (targetText != null)
        {
            targetText.color = Color.white;
        }
        else
        {
            return;
        }
  }
}
