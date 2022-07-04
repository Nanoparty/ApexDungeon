using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class ScoresManager : MonoBehaviour
{
    public GameObject content;
    public GameObject scoreCard;
    public Button button;
    
    private List<(string, int)> scores;
    void Start()
    {
        Data.scores = GameManager.gmInstance?.scores ?? new List<(string, int)>();
        scores = GameManager.gmInstance?.scores ?? new List<(string, int)>();

        TestData();

        //scores.OrderBy (s => s.Item2);

        scores.Sort((a, b) => {
            int result = b.Item2.CompareTo(a.Item2);
            return result;
        });

        foreach((string, int) score in scores){
            GameObject sc = GameObject.Instantiate(scoreCard, new Vector3(0, 0, 0), Quaternion.identity);
            GameObject nameText = sc.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
            GameObject scoreText = sc.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
            nameText.GetComponent<TMP_Text>().text = score.Item1;
            scoreText.GetComponent<TMP_Text>().text = "Score:" + score.Item2.ToString();
            sc.transform.SetParent(content.transform, false);
        }
        button.onClick.AddListener(mainMenu);

        
    }

    void TestData(){
        scores.Add(("player1",1));
        scores.Add(("player2",5));
        scores.Add(("player3",3));
        scores.Add(("player3",6));
        scores.Add(("player3",5));
        scores.Add(("player3",4));
    }

    private void mainMenu(){
        GameObject.Destroy(GameManager.gmInstance);
        SceneManager.LoadScene("Title",LoadSceneMode.Single);
    }

}
