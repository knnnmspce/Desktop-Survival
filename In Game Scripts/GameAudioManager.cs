using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The purpose of this script is to load and use the settings out of PlayerPrefs saved on the main menu and to also control the in-game audio settings from the options menu
public class GameAudioManager : Singleton<GameAudioManager>
{
    //keys for PlayerPrefs
    private static readonly string MusicPref = "MusicPref";
    private static readonly string FxPref = "FxPref";

    private float musicFloat, fxFloat;
    public AudioSource musicAudio;
    public AudioSource[] fxAudio;

    public Slider musicVol, fxVol;

    protected override void Awake()
    {
        base.Awake();
        ContinueSettings();
        EventBroker.PowerupActive += PlayPowerupSound;
    }

    private void OnDisable()
    {
        EventBroker.PowerupActive -= PlayPowerupSound;
    }

    void PlayPowerupSound()
    {
        fxAudio[6].Play();
    }

    private void ContinueSettings()
    {
        musicFloat = PlayerPrefs.GetFloat(MusicPref);
        fxFloat = PlayerPrefs.GetFloat(FxPref);

        musicAudio.volume = musicFloat;

        musicVol.value = musicFloat;
        fxVol.value = fxFloat;

        for (int i = 0; i < fxAudio.Length; i++)
        {
            fxAudio[i].volume = fxFloat;
        }
    }

    public void SaveSoundSettngs()
    {
        PlayerPrefs.SetFloat(MusicPref, musicVol.value);
        PlayerPrefs.SetFloat(FxPref, fxVol.value);
    }

    public void UpdateVolume()
    {
        musicAudio.volume = musicVol.value;

        for (int i = 0; i < fxAudio.Length; i++)
        {
            fxAudio[i].volume = fxVol.value;
        }
    }
}
