using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class LogManager : MonoBehaviour
{
    [SerializeField] private GameObject logEntry;

    private GameObject content;
    private GameObject scrollView;
    private List<GameObject> logEntries;

    private void Awake()
    {
        scrollView = transform.Find("Scroll View").gameObject;
        content = transform.Find("Scroll View/Viewport/Content").gameObject;
        logEntries = new List<GameObject>();
        Canvas.ForceUpdateCanvases();
        scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
    }

    public void AddLog(string message)
    {
       
        GameObject log = Instantiate(logEntry, Vector2.zero, Quaternion.identity);
        log.transform.SetParent(content.transform, false);
        log.GetComponent<TMP_Text>().text = message;
        logEntries.Add(log);
        Canvas.ForceUpdateCanvases();
        log.GetComponent<ContentSizeFitter>().SetLayoutVertical();
        content.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
        content.GetComponent<ContentSizeFitter>().SetLayoutVertical();
        scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
    }
}
