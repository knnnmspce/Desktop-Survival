using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The purpose of this script is to keep track and adjust the score
//this script was updated to implement the Singleton design pattern
public class ScoreManager : Singleton<ScoreManager>
{
    public Image crown;
    public bool newHighscore;

    private int score;

    private static readonly string ScoreThreshHold = "FifthPlaceScore";

    Text text;
    int threshHold;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<Text>();
        threshHold = PlayerPrefs.GetInt(ScoreThreshHold);

        score = 0;
    }

    public void UpdateScore(int points)
    {
        score = score + points;
        text.text = "Score: " + score;

        if (!newHighscore) //only check for highscores if we don't already have more than the 5th place player
        {
            CheckForHighscore();
        }
    }

    private void CheckForHighscore()
    {
        if(score > threshHold)
        {
            newHighscore = true;
            crown.enabled = true;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
