using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Pause", menuName = "ScriptableObjects/Pause")]

public class Pause : ScriptableObject
{
    public GameObject pausePrefab;

    private GameObject pauseRoot;
    private Player player;

    public void CreatePause(Player player)
    {
        this.player = player;

        SoundManager.sm.PlayMenuSound();
        player.openPause = true;
        GameObject parent = GameObject.FindGameObjectWithTag("Pause");
        pauseRoot = GameObject.Instantiate(pausePrefab, Vector3.zero, Quaternion.identity);
        pauseRoot.transform.SetParent(parent.transform, false);

        GameObject buttons = pauseRoot.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
        GameObject options = pauseRoot.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
        GameObject music = options.transform.GetChild(0).gameObject;
        GameObject sound = options.transform.GetChild(1).gameObject;

        buttons.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(MenuListener);
        buttons.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(ResumeListener);

        Toggle musicToggle = music.transform.GetChild(1).gameObject.GetComponent<Toggle>();
        Toggle soundToggle = sound.transform.GetChild(1).gameObject.GetComponent<Toggle>();
        Slider musicSlider = music.transform.GetChild(2).gameObject.GetComponent<Slider>();
        Slider soundSlider = sound.transform.GetChild(2).gameObject.GetComponent<Slider>();

        musicToggle.isOn = Data.music;
        soundToggle.isOn = Data.sound;
        musicSlider.value = Data.musicVolume;
        soundSlider.value = Data.soundVolume;

        musicToggle.onValueChanged.AddListener((value) => { musicToggleListener(value); });
        soundToggle.onValueChanged.AddListener((value) => { soundToggleListener(value); });
        musicSlider.onValueChanged.AddListener((value) => { musicSliderListener(value); });
        soundSlider.onValueChanged.AddListener((value) => { soundSliderListener(value); });
    }

    void ResumeListener()
    {
        SoundManager.sm.PlayMenuSound();
        player.openPause = false;
        player.pauseButton.GetComponent<Clickable>().setClicked(false);
        GameObject.Destroy(pauseRoot);
    }

    void MenuListener()
    {
        SoundManager.sm.PlayMenuSound();
        player.SaveCharacterData();
        Data.SaveToFile();
        Destroy(GameManager.gmInstance);
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    void musicToggleListener(bool value)
    {
        Data.music = value;
        SoundManager.sm.UpdatePlaying();
        SoundManager.sm.PlayMenuSound();
    }

    void musicSliderListener(float value)
    {
        Data.musicVolume = value;
        SoundManager.sm.UpdateMusicVolume();
    }

    void soundToggleListener(bool value)
    {
        SoundManager.sm.PlayMenuSound();
        Data.sound = value;
    }

    void soundSliderListener(float value)
    {
        Data.soundVolume = value;
        SoundManager.sm.UpdateSoundVolume();
    }
}
