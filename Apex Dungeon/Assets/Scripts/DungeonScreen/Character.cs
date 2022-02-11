using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character
{
    GameObject panel;
    GameObject panelObject;
    Button close;

    bool closed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Character(GameObject panel)
    {
        this.panel = panel;
    }

    public void openStats()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Character");
        panelObject = GameObject.Instantiate(panel, Vector3.zero, Quaternion.identity);
        panelObject.transform.SetParent(parent.transform, false);

        close = panelObject.transform.GetChild(2).gameObject.GetComponent<Button>();
        close.onClick.AddListener(closeListener);
    }

    void closeListener()
    {
        closeInventory();
        closed = true;
    }

    public void closeInventory()
    {
        GameObject.Destroy(panelObject);
    }

    public bool getClosed()
    {
        return closed;
    }
    public void setClosed(bool b)
    {
        closed = b;
    }
}
