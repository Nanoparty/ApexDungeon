using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoresManager : MonoBehaviour
{
    public GameObject content;
    public GameObject scoreCard;
    public Button button;
    private List<int> scores;
    void Start()
    {
        Data.scores = GameManager.gmInstance.scores;
        scores = GameManager.gmInstance.scores;
        foreach(int score in scores){
            GameObject sc = GameObject.Instantiate(scoreCard, new Vector3(0, 0, 0), Quaternion.identity);
            sc.transform.GetComponent<TMP_Text>().text = "player 1 - " + score.ToString();
            sc.transform.SetParent(content.transform, false);
        }
        button.onClick.AddListener(mainMenu);
    }

    private void mainMenu(){
        //GameManager.gmInstance.FullReset();
        //GameManager.gmInstance.state = "play";
        
        GameObject.Destroy(GameManager.gmInstance);
        SceneManager.LoadScene("test",LoadSceneMode.Single);
    }

}
