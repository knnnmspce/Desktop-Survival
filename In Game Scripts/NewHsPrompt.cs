using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewHsPrompt : MonoBehaviour
{
    public Text userInit;
    public Button contButton;
    public Text yourScore;

    int playerScore;

    private static readonly string FirstPlaceName = "FirstPlaceName";
    private static readonly string FirstPlaceScore = "FirstPlaceScore";
    private static readonly string SecondPlaceName = "SecondPlaceName";
    private static readonly string SecondPlaceScore = "SecondPlaceScore";
    private static readonly string ThirdPlaceName = "ThirdPlaceName";
    private static readonly string ThirdPlaceScore = "ThirdPlaceScore";
    private static readonly string FourthPlaceName = "FourthPlaceName";
    private static readonly string FourthPlaceScore = "FourthPlaceScore";
    private static readonly string FifthPlaceName = "FifthPlaceName";
    private static readonly string FifthPlaceScore = "FifthPlaceScore";

    private void Start()
    {
        playerScore = ScoreManager.Instance.GetScore();
        yourScore.text = "Your score: " + playerScore;        
    }

    private void FindScorePosition()
    {
        int firstScore = PlayerPrefs.GetInt(FirstPlaceScore);
        int secondScore = PlayerPrefs.GetInt(SecondPlaceScore);
        int thirdScore = PlayerPrefs.GetInt(ThirdPlaceScore);
        int fourthScore = PlayerPrefs.GetInt(FourthPlaceScore);
        int fifthScore = PlayerPrefs.GetInt(FifthPlaceScore);

        string firstName = PlayerPrefs.GetString(FirstPlaceName);
        string secondName = PlayerPrefs.GetString(SecondPlaceName);
        string thirdName = PlayerPrefs.GetString(ThirdPlaceName);
        string fourthName = PlayerPrefs.GetString(FourthPlaceName);

        if (playerScore < fourthScore && playerScore > fifthScore)
        {
            PlayerPrefs.SetInt(FifthPlaceScore, playerScore);
            PlayerPrefs.SetString(FifthPlaceName, userInit.text);
        }

        if(playerScore > fourthScore && playerScore <= thirdScore)
        {
            PlayerPrefs.SetInt(FifthPlaceScore, fourthScore);
            PlayerPrefs.SetString(FifthPlaceName, fourthName);

            PlayerPrefs.SetInt(FourthPlaceScore, playerScore);            
            PlayerPrefs.SetString(FourthPlaceName, userInit.text);
        }

        if (playerScore > thirdScore && playerScore <= secondScore)
        {
            PlayerPrefs.SetInt(FifthPlaceScore, fourthScore);
            PlayerPrefs.SetString(FifthPlaceName, fourthName);

            PlayerPrefs.SetInt(FourthPlaceScore, thirdScore);
            PlayerPrefs.SetString(FourthPlaceName, thirdName);

            PlayerPrefs.SetInt(ThirdPlaceScore, playerScore);
            PlayerPrefs.SetString(ThirdPlaceName, userInit.text);
        }

        if (playerScore > secondScore && playerScore <= firstScore)
        {
            PlayerPrefs.SetInt(FifthPlaceScore, fourthScore);
            PlayerPrefs.SetString(FifthPlaceName, fourthName);

            PlayerPrefs.SetInt(FourthPlaceScore, thirdScore);
            PlayerPrefs.SetString(FourthPlaceName, thirdName);

            PlayerPrefs.SetInt(ThirdPlaceScore, secondScore);
            PlayerPrefs.SetString(ThirdPlaceName, secondName);

            PlayerPrefs.SetInt(SecondPlaceScore, playerScore);            
            PlayerPrefs.SetString(SecondPlaceName, userInit.text);
        }

        if (playerScore > firstScore)
        {
            PlayerPrefs.SetInt(FifthPlaceScore, fourthScore);
            PlayerPrefs.SetString(FifthPlaceName, fourthName);

            PlayerPrefs.SetInt(FourthPlaceScore, thirdScore);
            PlayerPrefs.SetString(FourthPlaceName, thirdName);

            PlayerPrefs.SetInt(ThirdPlaceScore, secondScore);
            PlayerPrefs.SetString(ThirdPlaceName, secondName);

            PlayerPrefs.SetInt(SecondPlaceScore, firstScore);
            PlayerPrefs.SetString(SecondPlaceName, firstName);

            PlayerPrefs.SetInt(FirstPlaceScore, playerScore);           
            PlayerPrefs.SetString(FirstPlaceName, userInit.text);
        }
        
        PlayerPrefs.Save();
    }

    public void AcceptUser()
    {
        FindScorePosition();

        if (userInit.text.Length == 3)
        {
            contButton.gameObject.SetActive(true);
        }   
    }
}
