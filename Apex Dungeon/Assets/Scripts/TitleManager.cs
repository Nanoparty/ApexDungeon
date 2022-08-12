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

    private void Awake()
    {
        Data.LoadFromFile(imageLookup);
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
}
