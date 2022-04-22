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
        scores = GameManager.gmInstance.scores;
        foreach(int score in scores){
            GameObject sc = GameObject.Instantiate(scoreCard, new Vector3(0, 0, 0), Quaternion.identity);
            sc.transform.GetComponent<TMP_Text>().text = "player 1 - " + score.ToString();
            sc.transform.SetParent(content.transform, false);
        }
        button.onClick.AddListener(mainMenu);
        Debug.Log("SHADOWS:"+ GameManager.gmInstance.Dungeon.activeShadows.Count);
    }

    private void mainMenu(){
        //GameManager.gmInstance.fullReset();
        Debug.Log("CLICK");
        GameManager.gmInstance.Dungeon = null;
        SceneManager.LoadScene("test", LoadSceneMode.Single);
    }

}
