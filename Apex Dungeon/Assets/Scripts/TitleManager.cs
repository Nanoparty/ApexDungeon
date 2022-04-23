using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button Play;
    public Button Scores;
    public Button Options;
    public GameObject newPopup;
    
    void Start()
    {
        Play.onClick.AddListener(playListener);
        Scores.onClick.AddListener(scoresListener);
        Options.onClick.AddListener(optionsListener);
        newPopup.SetActive(false);
    }

    void playListener(){
        if(!Data.inProgress){
            //name popup
            Data.playerName = "Boomer";
            newPopup.SetActive(true);
            newPopup.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(acceptListener);
        }else{
            SceneManager.LoadScene("test", LoadSceneMode.Single);
        }
        
    }

    void scoresListener(){
        SceneManager.LoadScene("Scores", LoadSceneMode.Single);
    }

    void optionsListener(){

    }

    void acceptListener(){
        string name = newPopup.transform.GetChild(2).GetComponent<TMP_InputField>().text;
        Data.playerName = name;
        Data.inProgress = true;
        SceneManager.LoadScene("test", LoadSceneMode.Single);
    }
}
