using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button NewGame;
    public Button LoadGame;
    public Button Scores;
    public Button Options;
    //public GameObject newPopup;
    //public GameObject popupError;
    //public bool popupOpen = false;
    //public List<string> taken;
    private bool done;
    private List<CharacterData> charData;
    //public AudioSource audioSource;

    //public AudioClip buttonClick;

    public ImageLookup imageLookup;

    private void Awake()
    {
        Data.LoadFromFile(imageLookup);
    }

    void Start()
    {
        SoundManager.sm.PlayTitleMusic();
        //audioSource = GetComponent<AudioSource>();
        NewGame.onClick.AddListener(newGameListener);
        LoadGame.onClick.AddListener(loadGameListener);
        Scores.onClick.AddListener(scoresListener);
        Options.onClick.AddListener(optionsListener);
        //newPopup.SetActive(false);
        //popupError.SetActive(false);
        //taken = Data.names ?? new List<string>();
        done = false;
    }

    void newGameListener(){
        SoundManager.sm.PlayMenuSound();
        //if(popupOpen)return;

        SceneManager.LoadScene("CharacterCreator", LoadSceneMode.Single);

        //popupOpen = true;
        //newPopup.SetActive(true);
        //newPopup.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(acceptListener);
        //newPopup.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(cancelListener);
    }

    void loadGameListener(){
        SoundManager.sm.PlayMenuSound();
        //if(popupOpen)return;

        SceneManager.LoadScene("CharacterSelect", LoadSceneMode.Single);
    }

    void scoresListener(){
        SoundManager.sm.PlayMenuSound();
        //if(popupOpen)return;

        SceneManager.LoadScene("Scores", LoadSceneMode.Single);
    }

    void optionsListener(){
        SoundManager.sm.PlayMenuSound();
        //if(popupOpen)return;
        SceneManager.LoadScene("Options", LoadSceneMode.Single);
    }

    //void acceptListener(){
    //    SoundManager.sm.PlayMenuSound();
    //    string name = newPopup.transform.GetChild(4).GetComponent<TMP_InputField>().text;

    //    if(!(name.Length > 0))return;
    //    if(taken.Contains(name) && !done){
    //        popupError.SetActive(true);
    //        return;
    //    }

    //    done = true;
    //    Data.playerName = name;
    //    taken.Add(name);
    //    Data.names = taken;
    //    Data.activeCharacter = name;
    //    charData = Data.charData ?? new List<CharacterData>();
    //    charData.Add(new CharacterData(name));
    //    Data.charData = charData;
    //    Data.LoadActiveData();
    //    Data.loadData = false;
    //    SoundManager.sm.StopMusic();
    //    SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
    //}

    //void cancelListener(){
    //    SoundManager.sm.PlayMenuSound();
    //    newPopup.transform.GetChild(4).GetComponent<TMP_InputField>().text = "";
    //    newPopup.SetActive(false);
    //    popupOpen = false;
    //}
}
