using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//The purpose of this script is to get the highscore data from PlayerPrefs and display it
public class DisplayHighscores : MonoBehaviour
{
    //Keys for PlayerPrefs
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
        DisplayLeaderboard();
    }

    private void DisplayLeaderboard()
    {
        string firstPlaceName;
        int firstPlaceScore;
        firstPlaceName = PlayerPrefs.GetString(FirstPlaceName);
        firstPlaceScore = PlayerPrefs.GetInt(FirstPlaceScore);
        
        if(firstPlaceName != "")
        {
            Text firstName;
            firstName = GameObject.Find("FirstName").GetComponent<Text>();
            firstName.text = firstPlaceName;

            Text firstScoreTxt;
            firstScoreTxt = GameObject.Find("FirstScore").GetComponent<Text>();
            firstScoreTxt.text = "" + firstPlaceScore;
        }

        string secondPlaceName;
        int secondPlaceScore;
        secondPlaceName = PlayerPrefs.GetString(SecondPlaceName);
        secondPlaceScore = PlayerPrefs.GetInt(SecondPlaceScore);

        if (secondPlaceName != "")
        {
            Text secondName;
            secondName = GameObject.Find("SecondName").GetComponent<Text>();
            secondName.text = secondPlaceName;

            Text secScoreTxt;
            secScoreTxt = GameObject.Find("SecondScore").GetComponent<Text>();
            secScoreTxt.text = "" + secondPlaceScore;
        }

        string thirdPlaceName;
        int thirdPlaceScore;
        thirdPlaceName = PlayerPrefs.GetString(ThirdPlaceName);
        thirdPlaceScore = PlayerPrefs.GetInt(ThirdPlaceScore);

        if (thirdPlaceName != "")
        {
            Text Name;
            Name = GameObject.Find("ThirdName").GetComponent<Text>();
            Name.text = thirdPlaceName;

            Text ScoreTxt;
            ScoreTxt = GameObject.Find("ThirdScore").GetComponent<Text>();
            ScoreTxt.text = "" + thirdPlaceScore;
        }
        else
        {
            Debug.Log(thirdPlaceName);
            Debug.Log(thirdPlaceScore);
            Debug.Log("3rd is null");
        }

        string fourthPlaceName;
        int fourthPlaceScore;
        fourthPlaceName = PlayerPrefs.GetString(FourthPlaceName);
        fourthPlaceScore = PlayerPrefs.GetInt(FourthPlaceScore);

        if (fourthPlaceName != "")
        {
            Text Name;
            Name = GameObject.Find("FourthName").GetComponent<Text>();
            Name.text = fourthPlaceName;

            Text ScoreTxt;
            ScoreTxt = GameObject.Find("FourthScore").GetComponent<Text>();
            ScoreTxt.text = "" + fourthPlaceScore;
        }

        string fifthPlaceName;
        int PlaceScore;
        fifthPlaceName = PlayerPrefs.GetString(FifthPlaceName);
        PlaceScore = PlayerPrefs.GetInt(FifthPlaceScore);

        if (fifthPlaceName != "")
        {
            Text Name;
            Name = GameObject.Find("FifthName").GetComponent<Text>();
            Name.text = fifthPlaceName;

            Text ScoreTxt;
            ScoreTxt = GameObject.Find("FifthScore").GetComponent<Text>();
            ScoreTxt.text = "" + PlaceScore;
        }
    }
}
