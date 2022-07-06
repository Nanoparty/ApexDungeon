using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Button menuButton;
    public Toggle musicToggle;
    public Slider musicSlider;
    public Toggle soundToggle;
    public Slider soundSlider;
    public Button resetButton;

    void Start()
    {
        menuButton.onClick.AddListener(menuListener);
        resetButton.onClick.AddListener(resetListener);
        musicToggle.onValueChanged.AddListener((value) => { musicToggleListener(value); });
        soundToggle.onValueChanged.AddListener((value) => { soundToggleListener(value); });
        musicSlider.onValueChanged.AddListener((value) => { musicSliderListener(value); });
        soundSlider.onValueChanged.AddListener((value) => { soundSliderListener(value); });
    }

    void menuListener()
    {
        SoundManager.sm.PlayMenuSound();
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    void resetListener()
    {
        SoundManager.sm.PlayMenuSound();
    }

    void musicToggleListener(bool value)
    {
        SoundManager.sm.PlayMenuSound();
        Data.music = value;
        SoundManager.sm.UpdatePlaying();
    }

    void musicSliderListener(float value)
    {
        //SoundManager.sm.PlayMenuSound();
        Data.musicVolume = value;
        SoundManager.sm.UpdateVolume();
    }

    void soundToggleListener(bool value)
    {
        SoundManager.sm.PlayMenuSound();
        Data.sound = value;
    }

    void soundSliderListener(float value)
    {
        //SoundManager.sm.PlayMenuSound();
        Data.soundVolume = value;
    }
}
