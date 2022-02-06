using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class titleController : MonoBehaviour
{
    public Button play;
    public Button options;

    void Start()
    {
        play.onClick.AddListener(playListener);
        options.onClick.AddListener(optionsListener);
    }

    void playListener()
    {
        SceneManager.LoadScene("test", LoadSceneMode.Single);
    }

    void optionsListener()
    {

    }

}
