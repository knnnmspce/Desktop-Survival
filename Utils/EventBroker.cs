using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBroker
{
    public static event Action CountdownEnded;
    public static event Action GameRestarted;
    public static event Action PowerupActive;
    public static event Action PowerupComplete;
    public static event Action DoublePoints;
    public static event Action NormalizeScore;
    public static event Action PlayerDied;

    public static void CallCountdownEnded()
    {
        if(CountdownEnded != null)
        {
            CountdownEnded();
        }
        else
        {
            Debug.LogError("EventBroker: No listeners for Event Action CountdownEnded");
        }
    }

    public static void CallPowerupActive()
    {
        if(PowerupActive != null)
        {
            PowerupActive();
        }
        else
        {
            Debug.LogError("EventBroker: No listeners for Event Action PowerupActive");
        }
    }

    public static void CallPowerupComplete()
    {
        if(PowerupComplete != null)
        {
            PowerupComplete();
        }
        else
        {
            Debug.LogError("EventBroker: No listeners for Event Action PowerupComplete");
        }
    }

    public static void CallDoublePoints()
    {
        if(DoublePoints != null)
        {
            DoublePoints();
        }
        else
        {
            Debug.LogError("EventBroker: No listeners for Event Action DoublePoints");
        }
    }

    public static void CallNormalizeScore()
    {
        if(NormalizeScore != null)
        {
            NormalizeScore();
        }
        else
        {
            Debug.LogError("EventBroker: No listeners for Event Action NormalizeScore");
        }
    }

    public static void CallPlayerDied()
    {
        if (PlayerDied != null)
        {
            PlayerDied();
        }
        else
        {
            Debug.LogError("EventBroker: No listeners for Event Action PlayerDied");
        }
    }

    public static void RestartGame()
    {
        if(GameRestarted != null)
        {
            GameRestarted();
        }
        else
        {
            Debug.LogError("EventBroker: No listeners for Event Action GameRestarted");
        }
    }
}
