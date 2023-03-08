using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class LogManager : MonoBehaviour
{
    [SerializeField] private GameObject logEntry;

    [SerializeField] private GameObject Collapsed;
    [SerializeField] private GameObject Fullscreen;

    private GameObject fullContent;
    private GameObject partialContent;
    private GameObject fullScrollView;
    private GameObject partialScrollView;

    private List<GameObject> logEntries;

    public bool isFullscreen;

    private void Awake()
    {
        logEntries = new List<GameObject>();

        if (isFullscreen)
        {
            SetFullscreen();
        }
        else
        {
            SetCollapsed();
        }
    }

    private void SetFullscreen()
    {
        Fullscreen.SetActive(true);
        fullScrollView = Fullscreen.transform.Find("Scroll View").gameObject;
        fullContent = Fullscreen.transform.Find("Scroll View/Viewport/Content").gameObject;
        Canvas.ForceUpdateCanvases();
        fullScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;

        Fullscreen.transform.Find("Minimize").GetComponent<Button>().onClick.AddListener(ToggleSize);
    }

    private void DisableFullscreen()
    {
        Fullscreen.SetActive(false);
    }

    private void SetCollapsed()
    {
        Collapsed.SetActive(true);
        partialScrollView = Collapsed.transform.Find("Scroll View").gameObject;
        partialContent = Collapsed.transform.Find("Scroll View/Viewport/Content").gameObject;
        Canvas.ForceUpdateCanvases();
        partialScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;

        Collapsed.transform.Find("Open").GetComponent<Button>().onClick.AddListener(ToggleSize);
    }

    private void DisableCollapsed()
    {
        Collapsed.SetActive(false);
    }

    public void ToggleSize()
    {
        if (isFullscreen)
        {
            isFullscreen = false;
            DisableFullscreen();
            SetCollapsed();
            PopulateCollapsed();
        }
        else
        {
            isFullscreen = true;
            DisableCollapsed();
            SetFullscreen();
            PopulateFullscreen();
        }
    }

    private void PopulateCollapsed()
    {
        foreach (GameObject log in logEntries) { 
            log.transform.SetParent(partialContent.transform, false);
            log.GetComponent<ContentSizeFitter>().SetLayoutVertical();
        }
        Canvas.ForceUpdateCanvases();
        partialContent.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
        partialContent.GetComponent<ContentSizeFitter>().SetLayoutVertical();
        partialScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
    }

    private void PopulateFullscreen()
    {
        foreach (GameObject log in logEntries)
        {
            log.transform.SetParent(fullContent.transform, false);
            log.GetComponent<ContentSizeFitter>().SetLayoutVertical();
        }
        Canvas.ForceUpdateCanvases();
        fullContent.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
        fullContent.GetComponent<ContentSizeFitter>().SetLayoutVertical();
        fullScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
    }

    public void AddLog(string message)
    {
       
        GameObject log = Instantiate(logEntry, Vector2.zero, Quaternion.identity);
        log.GetComponent<TMP_Text>().text = message;
        logEntries.Add(log);
        
        if (isFullscreen)
        {
            log.transform.SetParent(fullContent.transform, false);
            Canvas.ForceUpdateCanvases();
            log.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            fullContent.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
            fullContent.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            fullScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        }
        else
        {
            log.transform.SetParent(partialContent.transform, false);
            Canvas.ForceUpdateCanvases();
            log.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            partialContent.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
            partialContent.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            partialScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        }
        
    }
}
