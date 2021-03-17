using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The purpose of this script is to control the behavior of the countdown animation that plays at the beginning of each new game
public class Countdown : MonoBehaviour
{
    public Animation countAnim;
    public GameObject am;
    public AudioSource countDown;

    private void Start()
    {
        if(countAnim != null)
        {
            countAnim.Play();
        }
        else
        {
            Debug.LogError("Countdown: count animation could not be found");
        }
    }

    void OnCountdownComplete()
    {
        EventBroker.CallCountdownEnded();
    }

    void OnRestart()
    {
        countAnim.Play();
    }

    void SetAmActive()
    {
        am.SetActive(true);
        if (countDown != null)
        {
            countDown.Play();
        }
        else
        {
            Debug.LogError("Unable to find countDown audio");
        }
    }
}
