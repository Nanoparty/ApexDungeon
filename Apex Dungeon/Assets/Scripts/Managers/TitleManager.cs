using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public Button NewGame;
    public Button LoadGame;
    public Button Scores;
    public Button Options;
    public ImageLookup imageLookup;
    public SkillGenerator skillGenerator;

    public bool skipLoad;

    private void Awake()
    {
        if (skipLoad) return;

        Data.LoadFromFile(imageLookup, skillGenerator);
    }

    void Start()
    {
        SoundManager.sm.PlayTitleMusic();
        NewGame.onClick.AddListener(newGameListener);
        LoadGame.onClick.AddListener(loadGameListener);
        Scores.onClick.AddListener(scoresListener);
        Options.onClick.AddListener(optionsListener);
    }

    void newGameListener(){
        SoundManager.sm.PlayMenuSound();
        SceneManager.LoadScene("CharacterCreator", LoadSceneMode.Single);
    }

    void loadGameListener(){
        SoundManager.sm.PlayMenuSound();
        SceneManager.LoadScene("CharacterSelect", LoadSceneMode.Single);
    }

    void scoresListener(){
        SoundManager.sm.PlayMenuSound();
        SceneManager.LoadScene("Scores", LoadSceneMode.Single);
    }

    void optionsListener(){
        SoundManager.sm.PlayMenuSound();
        SceneManager.LoadScene("Options", LoadSceneMode.Single);
    }

    public void OpenYoutube()
    {
        Application.OpenURL("https://www.youtube.com/@dispixel");
    }

    public  void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/dispixel_exe");
    }

    public void OpenFacebook()
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=100088631456408");
    }

    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/dispixel_studios/");
    }
}
