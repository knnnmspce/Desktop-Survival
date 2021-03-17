using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//The purpose of this script is to set the default values for the highscore table
public class SetHSData : MonoBehaviour
{
    //Keys for PlayerPref data

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

    private void Awake()
    {
        SetDefaultHighscores();
    }

    void SetDefaultHighscores()
    {
        //Each of thefive top scores must hold a value. if the HS data is empty (the name is empty) set the name and scores to the default name and score. Thus no-name highscores are not saved.

        string firstPlaceName;
        int firstPlaceScore = 1000;
        firstPlaceName = PlayerPrefs.GetString(FirstPlaceName);

        if (firstPlaceName == "")
        {
            firstPlaceName = "AAA";
            PlayerPrefs.SetString(FirstPlaceName, firstPlaceName);
            PlayerPrefs.SetInt(FirstPlaceScore, firstPlaceScore);
        }

        string secPlaceName;
        int secPlaceScore = 750;
        secPlaceName = PlayerPrefs.GetString(SecondPlaceName);

        if (secPlaceName == "")
        {
            secPlaceName = "BBB";
            PlayerPrefs.SetString(SecondPlaceName, secPlaceName);
            PlayerPrefs.SetInt(SecondPlaceScore, secPlaceScore);
        }

        string thirdPlaceName;
        int thirdPlaceScore = 500;
        thirdPlaceName = PlayerPrefs.GetString(ThirdPlaceName);

        if (thirdPlaceName == "")
        {
            thirdPlaceName = "CCC";
            PlayerPrefs.SetString(ThirdPlaceName, thirdPlaceName);
            PlayerPrefs.SetInt(ThirdPlaceScore, thirdPlaceScore);
        }

        string fourthPlaceName;
        int PlaceScore = 250;
        fourthPlaceName = PlayerPrefs.GetString(FourthPlaceName);

        if (fourthPlaceName == "")
        {
            fourthPlaceName = "DDD";
            PlayerPrefs.SetString(FourthPlaceName, fourthPlaceName);
            PlayerPrefs.SetInt(FourthPlaceScore, PlaceScore);
        }

        string fifthPlaceName;
        int fifthPlaceScore = 100;
        fifthPlaceName = PlayerPrefs.GetString(FifthPlaceName);

        if (fifthPlaceName == "")
        {
            fifthPlaceName = "EEE";
            PlayerPrefs.SetString(FifthPlaceName, fifthPlaceName);
            PlayerPrefs.SetInt(FifthPlaceScore, fifthPlaceScore);
        }

        PlayerPrefs.Save();
    }
}
