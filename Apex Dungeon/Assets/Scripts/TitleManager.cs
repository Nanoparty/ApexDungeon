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
    public GameObject newPopup;
    public GameObject popupError;
    public bool popupOpen = false;
    public List<string> taken;
    private bool done;
    private List<CharacterData> charData;
    
    
    void Start()
    {
        NewGame.onClick.AddListener(newGameListener);
        LoadGame.onClick.AddListener(loadGameListener);
        Scores.onClick.AddListener(scoresListener);
        Options.onClick.AddListener(optionsListener);
        newPopup.SetActive(false);
        popupError.SetActive(false);
        taken = Data.names ?? new List<string>();
        done = false;
    }

    void newGameListener(){
        if(popupOpen)return;

        popupOpen = true;
        newPopup.SetActive(true);
        newPopup.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(acceptListener);
        newPopup.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(cancelListener);
    }

    void loadGameListener(){
        if(popupOpen)return;
        SceneManager.LoadScene("CharacterSelect", LoadSceneMode.Single);
    }

    void scoresListener(){
        if(popupOpen)return;

        SceneManager.LoadScene("Scores", LoadSceneMode.Single);
    }

    void optionsListener(){
        if(popupOpen)return;
    }

    void acceptListener(){
        string name = newPopup.transform.GetChild(4).GetComponent<TMP_InputField>().text;

        if(!(name.Length > 0))return;
        if(taken.Contains(name) && !done){
            popupError.SetActive(true);
            return;
        }

        done = true;
        Data.playerName = name;
        taken.Add(name);
        Data.names = taken;
        Data.activeCharacter = name;
        charData = Data.charData ?? new List<CharacterData>();
        charData.Add(new CharacterData(name));
        Data.charData = charData;
        Data.LoadActiveData();
        Data.loadData = false;
        SceneManager.LoadScene("test", LoadSceneMode.Single);
    }

    void cancelListener(){
        newPopup.transform.GetChild(4).GetComponent<TMP_InputField>().text = "";
        newPopup.SetActive(false);
        popupOpen = false;
    }
}
