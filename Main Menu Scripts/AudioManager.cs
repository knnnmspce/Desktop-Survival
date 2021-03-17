using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The purpose of this script is to control the audio settings behavior in the main menu and to save those settings.
public class AudioManager : Singleton<AudioManager>
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MusicPref = "MusicPref";
    private static readonly string FxPref = "FxPref";
    private int firstPlayInt;
    public Slider musicVol, fxVol;
    private float musicFloat, fxFloat;
    public AudioSource musicAudio;
    public AudioSource[] fxAudio;

    // Start is called before the first frame update
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay); //is this the first playthrough?

        if(firstPlayInt == 0)
        {
            //if so, these are the default volume settings
            musicFloat = .25f;
            fxFloat = .75f;
            musicVol.value = musicFloat;
            fxVol.value = fxFloat;
            PlayerPrefs.SetFloat(MusicPref, musicFloat);
            PlayerPrefs.SetFloat(FxPref, fxFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            //otherwise, get the last saved settings
            musicFloat = PlayerPrefs.GetFloat(MusicPref);
            musicVol.value = musicFloat;
            fxFloat = PlayerPrefs.GetFloat(FxPref);
            fxVol.value = fxFloat;
            UpdateVolume();
        }
    }

    public void SaveSoundSettngs()
    {
        PlayerPrefs.SetFloat(MusicPref, musicVol.value);
        PlayerPrefs.SetFloat(FxPref, fxVol.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSoundSettngs();
        }
    }

    public void UpdateVolume()
    {
        musicAudio.volume = musicVol.value;

        for(int i = 0; i < fxAudio.Length; i++)
        {
            fxAudio[i].volume = fxVol.value;
        }
    }
}
