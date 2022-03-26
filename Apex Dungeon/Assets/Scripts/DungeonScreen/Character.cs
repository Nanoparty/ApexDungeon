using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


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

        close = panelObject.transform.GetChild(1).transform.GetChild(2).gameObject.GetComponent<Button>();
        close.onClick.AddListener(closeListener);

        setPlayerStats();
    }

    private void setPlayerStats(){
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        GameObject charPanel = panelObject.transform.GetChild(0).gameObject;
        GameObject topStats = charPanel.transform.GetChild(0).gameObject;
        GameObject middleStats = charPanel.transform.GetChild(1).gameObject;
        GameObject bottomStats = charPanel.transform.GetChild(2).gameObject;

        //Set HP and MP
        Image hpbar = topStats.transform.GetChild(2).gameObject.transform.GetComponent<Image>();
        Image mpbar = topStats.transform.GetChild(3).gameObject.transform.GetComponent<Image>();
        GameObject hpText = topStats.transform.GetChild(4).gameObject;
        GameObject mpText = topStats.transform.GetChild(5).gameObject;

        int hp = player.getHP();
        int mp = player.getMP();
        int maxHp = player.getMaxHP();
        int maxMp = player.getMaxMP();

        hpbar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)hp / (float)maxHp * 136f);
        mpbar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)mp / (float)maxMp * 136f);

        hpText.transform.gameObject.GetComponent<TMP_Text>().text = hp + "/" + maxHp;
        mpText.transform.gameObject.GetComponent<TMP_Text>().text = mp + "/" + maxMp;


        //Set Equipment, Level and EXP
        GameObject levelText = middleStats.transform.GetChild(1).gameObject;
        Image expBar = middleStats.transform.GetChild(3).gameObject.transform.GetComponent<Image>();

        int exp = player.getExp();
        int maxExp = player.getMaxExp();

        expBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)exp / (float)maxExp * 124f);
        levelText.transform.GetComponent<TMP_Text>().text = "Level:" + player.getExpLevel();

        //Set Stats and Gold
        GameObject str = bottomStats.transform.GetChild(0).gameObject;
        GameObject def = bottomStats.transform.GetChild(1).gameObject;
        GameObject crit = bottomStats.transform.GetChild(2).gameObject;
        GameObject intel = bottomStats.transform.GetChild(3).gameObject;
        GameObject gold = bottomStats.transform.GetChild(4).gameObject;
        GameObject evd = bottomStats.transform.GetChild(5).gameObject;
        GameObject blck = bottomStats.transform.GetChild(6).gameObject;

        str.transform.gameObject.GetComponent<TMP_Text>().text = "Str:" + player.getStrength();
        def.transform.gameObject.GetComponent<TMP_Text>().text = "Def:" + player.getDefense();
        crit.transform.gameObject.GetComponent<TMP_Text>().text = "Crit:" + player.getCritical();
        intel.transform.gameObject.GetComponent<TMP_Text>().text = "Int:" + player.getIntelligence();
        gold.transform.gameObject.GetComponent<TMP_Text>().text = "Gold:" + player.getGold();
        evd.transform.gameObject.GetComponent<TMP_Text>().text = "Evd:" + player.getEvade();
        blck.transform.gameObject.GetComponent<TMP_Text>().text = "Blck:" + player.getBlock();
        
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
