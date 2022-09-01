using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreatorManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject error;

    private List<string> taken;
    private bool done;
    private List<CharacterData> charData;

    void Start()
    {
        taken = Data.names ?? new List<string>();
        done = false;
        charData = Data.charData ?? new List<CharacterData>();

        error.SetActive(false);

        menu.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(acceptListener);
        menu.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(cancelListener);

    }

    void acceptListener()
    {
        SoundManager.sm.PlayMenuSound();
        string name = menu.transform.GetChild(4).GetComponent<TMP_InputField>().text.ToUpper();

        if (!(name.Length > 0)) return;
        if (taken.Contains(name) && !done)
        {
            error.SetActive(true);
            return;
        }

        done = true;
        Data.playerName = name;
        taken.Add(name);
        Data.names = taken;
        Data.activeCharacter = name;
        
        charData.Add(new CharacterData(name));
        Data.charData = charData;
        Data.LoadActiveData();
        Data.loadData = false;
        SoundManager.sm.StopMusic();
        SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
    }

    void cancelListener()
    {
        SoundManager.sm.PlayMenuSound();
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }
}
