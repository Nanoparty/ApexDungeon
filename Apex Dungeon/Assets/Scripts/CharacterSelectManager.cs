using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class CharacterSelectManager : MonoBehaviour
{
    private List<GameObject> characters;
    private List<CharacterData> charData;
    public GameObject CharacterLine;
    public GameObject Confirm;
    public GameObject Back;
    public GameObject ContentArea;
    private GameObject selectedChar;

    void Start()
    {
        characters = new List<GameObject>();
        charData = Data.charData ?? new List<CharacterData>();
        Confirm.GetComponent<Button>().onClick.AddListener(ConfirmListener);
        Back.GetComponent<Button>().onClick.AddListener(BackListener);

        selectedChar = null;

        //AddTestData();

        PopulateCharacters();
    }

    void AddTestData(){
        charData.Add(new CharacterData(){name="bob",level=5,floor=1});
        charData.Add(new CharacterData(){name="kyle",level=6,floor=3});
        charData.Add(new CharacterData(){name="Queen Elizabeth",level=22,floor=11});
        charData.Add(new CharacterData(){name="Monster Energy",level=57,floor=32});
        charData.Add(new CharacterData(){name="Monster Energy",level=57,floor=32});
        charData.Add(new CharacterData(){name="Monster Energy",level=57,floor=32});
        charData.Add(new CharacterData(){name="Monster Energy",level=57,floor=32});
    }

    void PopulateCharacters(){
        foreach(CharacterData cd in charData){
            Vector3 position = new Vector3(0f, 0f, 0f);
            GameObject characterUnit = Instantiate(CharacterLine, position, Quaternion.identity, ContentArea.transform) as GameObject;

            characterUnit.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = cd.name;
            characterUnit.transform.GetChild(1).transform.GetChild(1).GetComponent<TMP_Text>().text = "Level: " + cd.expLevel.ToString();
            characterUnit.transform.GetChild(1).transform.GetChild(2).GetComponent<TMP_Text>().text = "Floor: " + cd.floor.ToString();
            characterUnit.AddComponent<CharacterData>();
            characterUnit.GetComponent<CharacterData>().name = cd.name;
            characters.Add(characterUnit);
        }
    }

    void Update()
    {
        foreach(GameObject c in characters){
            if(c.GetComponent<CharacterSelect>().getClicked()){
                if(selectedChar != null && c != selectedChar){
                    selectedChar.GetComponent<CharacterSelect>().setClicked(false);
                    selectedChar = c;
                }else{
                    selectedChar = c;
                }
            }
        }
    }

    void ConfirmListener(){
        SoundManager.sm.PlayMenuSound();
        if(selectedChar == null) return;
        Data.activeCharacter = selectedChar.GetComponent<CharacterData>().name;
        Data.LoadActiveData();
        Data.loadData = true;
        SceneManager.LoadScene("Dungeon",LoadSceneMode.Single);
    }

    void BackListener(){
        SoundManager.sm.PlayMenuSound();
        SceneManager.LoadScene("Title",LoadSceneMode.Single);
    }
}
