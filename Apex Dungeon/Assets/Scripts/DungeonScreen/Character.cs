using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Character
{
    GameObject panel;
    GameObject panelObject;
    Button close;
    GameObject topicPanel;
    GameObject charPanel;
    GameObject slot;
    GameObject questLine;
    GameObject block;
    GameObject pblock;
    GameObject minimap;

    public List<GameObject> items;
    public List<GameObject> equipment;

    public List<GameObject> inventorySlots;
    public List<GameObject> equipmentSlots;

    public List<string> quests;
    public List<GameObject> questObjects;

    bool closed;
    string topicTitleString = "Inventory";
    int tab = 0;

    private int[,] map;
    private int width = 100;
    private int height = 100;
    GameObject mapRoot;
    public Sprite[] icons;
    public Sprite s;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Character(GameObject panel, GameObject slot, GameObject questLine, GameObject minimap, GameObject block, GameObject pblock)
    {
        this.panel = panel;
        this.slot = slot;
        this.questLine = questLine;
        this.block = block;
        this.pblock = pblock;
        this.minimap = minimap;

        inventorySlots = new List<GameObject>();
        equipmentSlots = new List<GameObject>();
        quests = new List<string>();
        questObjects = new List<GameObject>();

        map = new int[100, 100];
        buildMap();

        width = GameManager.gmInstance.Dungeon.width;
        height = GameManager.gmInstance.Dungeon.height;
        loadSprites();
    }

    public void openStats()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Character");
        panelObject = GameObject.Instantiate(panel, Vector3.zero, Quaternion.identity);
        panelObject.transform.SetParent(parent.transform, false);

        close = panelObject.transform.GetChild(1).transform.GetChild(2).gameObject.GetComponent<Button>();
        close.onClick.AddListener(closeListener);

        setPlayerStats();
        setTopicPanel();
        populateTopicArea();
        
    }

    private void setPlayerStats(){
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        charPanel = panelObject.transform.GetChild(0).gameObject;
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

    private void setTopicPanel(){
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        topicPanel = panelObject.transform.GetChild(1).gameObject;

        GameObject topicArea = topicPanel.transform.GetChild(0).gameObject;
        GameObject topicTitle = topicPanel.transform.GetChild(1).gameObject;
        Button itemsTab = topicPanel.transform.GetChild(3).gameObject.GetComponent<Button>();
        Button equipmentTab = topicPanel.transform.GetChild(4).gameObject.GetComponent<Button>();
        Button questTab = topicPanel.transform.GetChild(5).gameObject.GetComponent<Button>();
        Button mapTab = topicPanel.transform.GetChild(6).gameObject.GetComponent<Button>();

        topicTitle.transform.gameObject.GetComponent<TMP_Text>().text = topicTitleString;

        itemsTab.onClick.AddListener(itemListener);
        equipmentTab.onClick.AddListener(equipmentListener);
        questTab.onClick.AddListener(questListener);
        mapTab.onClick.AddListener(mapListener);
    }

    private void updateTopicPanel(){
        topicPanel = panelObject.transform.GetChild(1).gameObject;

        GameObject topicTitle = topicPanel.transform.GetChild(1).gameObject;
        topicTitle.transform.gameObject.GetComponent<TMP_Text>().text = topicTitleString;
    }

    void populateTopicArea(){
        if(tab == 0){
            //Inventory
            populateInventory();
        }
        if(tab == 1){
            //Equipment
            populateEquipment();
        }
        if(tab == 2){
            //Quests
            populateQuests();
        }
        if(tab == 3){
            //Map
            populateMap();
        }
    }

    void populateInventory(){
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Inventory inventory = player.getInventoryObject();
        GameObject topicArea = topicPanel.transform.GetChild(0).gameObject;

        int numItems = inventory.items.Count;

        int x = 0;
        int y = 0;
        float cellSize = 120;
        int xOff = 100;
        int yOff = -100;
        for(int i = 0; i < numItems; i++)
        {
            Vector3 pos = new Vector3(xOff + x * cellSize, yOff + -1 * y * cellSize, 0);

            Item item = inventory.items[i];

            GameObject itemslot = GameObject.Instantiate(slot, pos, Quaternion.identity);
            GameObject icon = itemslot.transform.GetChild(0).gameObject;
            icon.GetComponent<Image>().sprite = item.image;

            itemslot.transform.SetParent(topicArea.transform, false);
            inventorySlots.Add(itemslot);
            x++;
            if(x == 5){
                x = 0;
                y++;
            }
        }

    }

    void populateEquipment(){
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Inventory inventory = player.getInventoryObject();
        GameObject topicArea = topicPanel.transform.GetChild(0).gameObject;

        int numItems = inventory.items.Count;

        int x = 0;
        int y = 0;
        float cellSize = 120;
        int xOff = 100;
        int yOff = -100;
        for(int i = 0; i < numItems; i++)
        {
            Vector3 pos = new Vector3(xOff + x * cellSize, yOff + -1 * y * cellSize, 0);

            Item item = inventory.items[i];

            GameObject itemslot = GameObject.Instantiate(slot, pos, Quaternion.identity);
            GameObject icon = itemslot.transform.GetChild(0).gameObject;
            icon.GetComponent<Image>().sprite = item.image;

            itemslot.transform.SetParent(topicArea.transform, false);
            equipmentSlots.Add(itemslot);
            x++;
            if(x == 5){
                x = 0;
                y++;
            }
        }
    }

    void populateQuests(){
        quests.Add("Capture the orc.");
        quests.Add("Rescue the princess");
        quests.Add("Finish the game <333");

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GameObject topicArea = topicPanel.transform.GetChild(0).gameObject;

        int y = 0;
        float cellSize = 120;
        int yOff = -100;
        for(int i = 0; i < quests.Count; i++)
        {
            Vector3 pos = new Vector3(0, yOff + -1 * y * cellSize, 0);

            string quest = quests[i];

            GameObject questObject = GameObject.Instantiate(questLine, pos, Quaternion.identity);
            GameObject numText = questObject.transform.GetChild(0).gameObject;
            numText.GetComponent<TMP_Text>().text = (i+1).ToString() + ":";
            GameObject questText = questObject.transform.GetChild(1).gameObject;
            questText.GetComponent<TMP_Text>().text = quest;

            questObject.transform.SetParent(topicArea.transform, false);
            questObjects.Add(questObject);
            y++;
        }
    }

    void populateMap(){
        GameObject topicArea = topicPanel.transform.GetChild(0).gameObject;

        mapRoot = GameObject.Instantiate(minimap, new Vector3(0, 0, 0), Quaternion.identity);
        mapRoot.transform.SetParent(topicArea.transform, false);

        int size = 20;
        GameObject mapHolder = GameObject.FindGameObjectWithTag("Map").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        mapHolder.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width * size + 100);
        mapHolder.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height * size + 100);

        int xOff = -1*((width*size + 100)/2) + 50;
        int yOff = -1 * ((height * size +100)/ 2) + 50;
        
        for (int i = 0; i < height; i++)
        {
            for(int j = 0;j < width; j++)
            {
                if (GameManager.gmInstance.Dungeon.tileMap[i, j].explored == false) continue;
                if(GameManager.gmInstance.Dungeon.tileMap[i,j].type == 2)
                {
                    Vector3 pos = new Vector3(xOff + j * size,yOff + i * size, 0f);
                    GameObject b = GameObject.Instantiate(block, pos, Quaternion.identity);
                    b.GetComponent<Image>().sprite = icons[4];
                    b.transform.SetParent(mapHolder.transform, false);
                }
                if (GameManager.gmInstance.Dungeon.tileMap[i, j].type == 1 || GameManager.gmInstance.Dungeon.tileMap[i, j].type == 3)
                {
                    Vector3 pos = new Vector3(xOff + j * size, yOff + i * size, 0f);
                    GameObject b = GameObject.Instantiate(block, pos, Quaternion.identity);
                    b.GetComponent<Image>().sprite = icons[0];
                    b.transform.SetParent(mapHolder.transform, false);
                }
                if (GameManager.gmInstance.Dungeon.isPlayer(i, j))
                {
                    Vector3 pos = new Vector3(xOff + j * size, yOff + i * size, 0f);
                    GameObject b = GameObject.Instantiate(block, pos, Quaternion.identity);
                    b.GetComponent<Image>().sprite = icons[1];
                    b.transform.SetParent(mapHolder.transform, false);
                }
                if (GameManager.gmInstance.Dungeon.isEnemy(i, j))
                {
                    Vector3 pos = new Vector3(xOff + j * size, yOff + i * size, 0f);
                    GameObject b = GameObject.Instantiate(block, pos, Quaternion.identity);
                    b.GetComponent<Image>().sprite = icons[2];
                    b.transform.SetParent(mapHolder.transform, false);
                }
                
            }
        }
    }

    void buildMap()
    {
        for(int i = 0; i < 100; i++)
        {
            for(int j = 0;j < 100; j++)
            {
                map[i, j] = 0;
            }
            
        }
        for(int i = 0; i < 100; i++)
        {
            map[0, i] = 1;
            map[i, 0] = 1;
            map[99, i] = 1;
            map[i, 99] = 1;
        }
        for(int i = 0; i < 300; i++)
        {
            int x = Random.Range(0, 99);
            int y = Random.Range(0, 99);
            map[x, y] = 1;
        }
    }

    void loadSprites()
    {
        icons = Resources.LoadAll<Sprite>("mapIcons");

    }

    void closeListener()
    {
        closeInventory();
        closed = true;
    }

    void itemListener(){
        topicTitleString = "Inventory";
        tab = 0;
        refreshTopicPanel();
    }
    void equipmentListener(){
        topicTitleString = "Equipment";
        tab = 1;
        refreshTopicPanel();
    }
    void questListener(){
        topicTitleString = "Quests";
        tab = 2;
        refreshTopicPanel();
    }
    void mapListener(){
        topicTitleString = "Map";
        tab = 3;
        refreshTopicPanel();
    }

    void refreshTopicPanel(){
        foreach(GameObject o in inventorySlots){
            GameObject.Destroy(o);
        }
        foreach(GameObject o in equipmentSlots){
            GameObject.Destroy(o);
        }
        foreach(GameObject o in questObjects){
            GameObject.Destroy(o);
        }
        GameObject.Destroy(mapRoot);
        updateTopicPanel();
        populateTopicArea();
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
