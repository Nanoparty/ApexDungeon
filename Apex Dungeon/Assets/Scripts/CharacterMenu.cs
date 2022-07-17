﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CharacterMenu
{
    GameObject panel;
    GameObject panelObject;

    Button closeTab;
    Button inventoryTab;
    Button equipmentTab;
    Button mapTab;

    GameObject inventoryPanel;
    GameObject equipmentPanel;
    GameObject mapPanel;

    GameObject topicPanel;
    GameObject charPanel;
    GameObject slot;
    GameObject questLine;
    GameObject block;
    GameObject pblock;
    GameObject minimap;
    GameObject itemPopup;
    GameObject popupArea;
    GameObject useButton;
    GameObject equipButton;
    GameObject trashButton;
    GameObject compareButton;
    PlayerGear gear;
    Player player;

    public List<Consumable> items;
    public List<Equipment> equipment;

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
    int selected = -1;
    int gearSelection = -1;
    bool popupOpen = false;
    bool slotsLoaded = false;
    private Sprite[] frames;

    private int maxSlots = 25;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        
        if(popupOpen){
            //Debug.Log("POPUP OPEN");
            //close when click outside popup
            if (Input.GetButtonDown("Fire1"))
            {
                bool isClicked = false;
                if(tab == 0 || gearSelection > -1){
                    isClicked = popupArea.GetComponent<Clickable>().getClicked() 
                     || useButton.GetComponent<Clickable>().getClicked() 
                     || trashButton.GetComponent<Clickable>().getClicked();
                }
                if(tab == 1){
                    isClicked = popupArea.GetComponent<Clickable>().getClicked() 
                     || useButton.GetComponent<Clickable>().getClicked() 
                     || trashButton.GetComponent<Clickable>().getClicked()
                     || compareButton.GetComponent<Clickable>().getClicked();
                }

                if(isClicked)
                {
                    popupArea.GetComponent<Clickable>().setClicked(false);

                    if(tab == 1){
                        compareButton.GetComponent<Clickable>().setClicked(false);
                    }
                    useButton.GetComponent<Clickable>().setClicked(false);
                    trashButton.GetComponent<Clickable>().setClicked(false);
                }else{
                    //Debug.Log("DESTROY");
                    closePopup();
                    unclickAll();
                }           
                     
            }
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < 8;i++){
                //check equiped gear for clicks
                GameObject status = panelObject.transform.GetChild(1).gameObject;
                GameObject equipment = status.transform.GetChild(6).gameObject;
                GameObject gearSlot = equipment.transform.GetChild(i).gameObject;
                if(gearSlot.GetComponent<Clickable>().getClicked()){
                    gearSlot.GetComponent<Clickable>().setClicked(false);
                    gearSelection = i;
                    popupOpen = true;
                    createGearPopup();
                    SoundManager.sm.PlayMenuSound();
                    return;
                }
            }
            if(tab == 0){
                //Debug.Log("num slots=" + inventorySlots.Count);
                for(int i = 0; i < items.Count; i++)
                {
                    //Debug.Log("Checking slot:" + i);
                    if (inventorySlots != null && inventorySlots.Count > 0 
                        && inventorySlots[i].GetComponent<Clickable>().getClicked())
                    {
                        //Debug.Log(i + " is selected");
                        selected = i;
                        inventorySlots[i].GetComponent<Clickable>().setClicked(false);
                        popupOpen = true;
                        createPopup();
                        SoundManager.sm.PlayMenuSound();
                    }
                }
            }
            else if(tab == 1){
                for(int i = 0; i < equipmentSlots.Count; i++)
                {
                    if (equipmentSlots != null && equipmentSlots.Count > 0 
                        && equipmentSlots[i].GetComponent<Clickable>().getClicked())
                    {
                        selected = i;
                        equipmentSlots[i].GetComponent<Clickable>().setClicked(false);
                        popupOpen = true;
                        //Debug.Log("Clicked on equipment:"+ equipment[selected]);
                        createPopup();
                        SoundManager.sm.PlayMenuSound();
                    }
                }
            }
            
        }
    }

    public void closePopup()
    {
        GameObject.Destroy(popupArea);
        popupOpen = false;
    }

    public void unclickAll()
    {
        for (int i = 0; i < 8; i++)
        {
            charPanel = panelObject.transform.GetChild(0).gameObject;
            GameObject middleStats = charPanel.transform.GetChild(1).gameObject;
            GameObject gearSlot = middleStats.transform.GetChild(4 + i).gameObject;
            if (gearSlot.GetComponent<Clickable>().getClicked())
            {
                gearSlot.GetComponent<Clickable>().setClicked(false);
            }
        }
        if (tab == 0)
        {
            //Debug.Log("num slots=" + inventorySlots.Count);
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                //Debug.Log("Checking slot:" + i);
                if (inventorySlots != null && inventorySlots.Count > 0
                    && inventorySlots[i].GetComponent<Clickable>().getClicked())
                {
                    inventorySlots[i].GetComponent<Clickable>().setClicked(false);
                }
            }
        }
        if (tab == 1)
        {
            for (int i = 0; i < equipmentSlots.Count; i++)
            {
                if (equipmentSlots != null && equipmentSlots.Count > 0
                    && equipmentSlots[i].GetComponent<Clickable>().getClicked())
                {
                    equipmentSlots[i].GetComponent<Clickable>().setClicked(false);
                }
            }
        }
    }

    public CharacterMenu(GameObject panel, GameObject slot, GameObject questLine, GameObject minimap, GameObject block, GameObject pblock, GameObject itemPopup, Sprite[] frames)
    {
        this.panel = panel;
        this.slot = slot;
        this.questLine = questLine;
        this.block = block;
        this.pblock = pblock;
        this.minimap = minimap;
        this.itemPopup = itemPopup;
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        this.frames = frames;

        items = new List<Consumable>();
        equipment = new List<Equipment>();
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        this.gear = player.getGear();

        slotsLoaded = false;
        inventorySlots = new List<GameObject>();
        equipmentSlots = new List<GameObject>();

        GameObject parent = GameObject.FindGameObjectWithTag("Character");
        panelObject = GameObject.Instantiate(panel, Vector3.zero, Quaternion.identity);
        panelObject.transform.SetParent(parent.transform, false);

        inventoryTab = panelObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Button>();
        equipmentTab = panelObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Button>();
        mapTab = panelObject.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Button>();
        closeTab = panelObject.transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<Button>();

        inventoryTab.onClick.AddListener(itemListener);
        equipmentTab.onClick.AddListener(equipmentListener);
        mapTab.onClick.AddListener(mapListener);
        closeTab.onClick.AddListener(closeListener);

        setPlayerStats();
        setTopicPanel();
        populateTopicArea();
        
    }

    private void setPlayerStats(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        charPanel = panelObject.transform.GetChild(1).gameObject;
        GameObject nameBanner = charPanel.transform.GetChild(0).gameObject;
        GameObject healthBanner = charPanel.transform.GetChild(1).gameObject;
        GameObject playerProfile = charPanel.transform.GetChild(2).gameObject;
        GameObject statusBanner = charPanel.transform.GetChild(4).gameObject;
        GameObject equipment = charPanel.transform.GetChild(6).gameObject;

        //set name
        nameBanner.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = Data.playerName;

        //Set HP and MP
        GameObject hpText = healthBanner.transform.GetChild(0).gameObject;
        GameObject expText = healthBanner.transform.GetChild(1).gameObject;
        GameObject levelText = healthBanner.transform.GetChild(2).gameObject;
        GameObject hpBar = healthBanner.transform.GetChild(3).gameObject;
        GameObject expBar = healthBanner.transform.GetChild(4).gameObject;

        int hp = player.getHP();
        int exp = player.getExp();
        int maxHp = player.getMaxHP();
        int maxExp = player.getMaxExp();

        hpText.GetComponent<TMP_Text>().text = hp + "/" + maxHp;
        expText.GetComponent<TMP_Text>().text = exp + "/" + maxExp;

        levelText.transform.GetChild(1).transform.GetComponent<TMP_Text>().text = player.getExpLevel().ToString();

        //hpbar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)hp / (float)maxHp * 136f);
        //mpbar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)mp / (float)maxMp * 136f);

        //hpText.transform.gameObject.GetComponent<TMP_Text>().text = hp + "/" + maxHp;
        //mpText.transform.gameObject.GetComponent<TMP_Text>().text = mp + "/" + maxMp;

        //Set Player Profile picture

        //Set Player Stats
        TMP_Text strengthText = statusBanner.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        TMP_Text defenseText = statusBanner.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        TMP_Text criticalText = statusBanner.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        TMP_Text evasionText = statusBanner.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
        TMP_Text goldText = statusBanner.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();

        strengthText.text = "Strength:" + player.getStrength();
        defenseText.text = "Defense:" + player.getDefense();
        criticalText.text = "Critical:" + player.getCritical();
        evasionText.text = "Evasion:" + player.getEvade();
        goldText.text = "Gold:" + player.getGold();

        //set equipment
        GameObject HelmSlot = equipment.transform.GetChild(0).gameObject;
        GameObject ChestSlot = equipment.transform.GetChild(1).gameObject;
        GameObject LegSlot = equipment.transform.GetChild(2).gameObject;
        GameObject BootSlot = equipment.transform.GetChild(3).gameObject;
        GameObject WeaponSlot = equipment.transform.GetChild(4).gameObject;
        GameObject UtilitySlot = equipment.transform.GetChild(5).gameObject;
        GameObject NecklaceSlot = equipment.transform.GetChild(6).gameObject;
        GameObject RingSlot = equipment.transform.GetChild(7).gameObject;

        setEquipmentSlot(HelmSlot, gear.Helmet);
        setEquipmentSlot(ChestSlot, gear.Chestplate);
        setEquipmentSlot(LegSlot, gear.Legs);
        setEquipmentSlot(BootSlot, gear.Feet);
        setEquipmentSlot(WeaponSlot, gear.Weapon);
        setEquipmentSlot(UtilitySlot, gear.Secondary);
        setEquipmentSlot(NecklaceSlot, gear.Necklace);
        setEquipmentSlot(RingSlot, gear.Ring);
    }

    private void setEquipmentSlot(GameObject slot, Equipment? e){
        if(e == null){
            slot.transform.GetChild(0).gameObject.SetActive(true);
            slot.transform.GetChild(1).gameObject.SetActive(false);
            slot.GetComponent<Image>().sprite = frames[0];
        } 
        else {
            slot.transform.GetChild(0).gameObject.SetActive(false);
            slot.transform.GetChild(1).gameObject.SetActive(true);
            slot.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = e.image;

            if(e.tier == 1) slot.GetComponent<Image>().sprite = frames[0];
            if(e.tier == 2) slot.GetComponent<Image>().sprite = frames[1];
            if(e.tier == 3) slot.GetComponent<Image>().sprite = frames[2];
            if(e.tier == 4) slot.GetComponent<Image>().sprite = frames[3];
        }
    }

    private void setTopicPanel(){
        inventoryPanel = panelObject.transform.GetChild(2).gameObject;
        equipmentPanel = panelObject.transform.GetChild(3).gameObject;
        mapPanel = panelObject.transform.GetChild(4).gameObject;

        inventoryPanel.SetActive(false);
        equipmentPanel.SetActive(false);
        mapPanel.SetActive(false);

        if(tab == 0)
        {
            inventoryPanel.SetActive(true);
        }
        else if(tab == 1)
        {
            equipmentPanel.SetActive(true);
        }
        else if(tab == 3)
        {
            mapPanel.SetActive(true);
        }
    }

    void populateTopicArea(){
        if(tab == 0){
            //Inventory
            populateInventory();
        }
        else if(tab == 1){
            //Equipment
            populateEquipment();
        }
        else if(tab == 2){
            //Quests
            populateQuests();
        }
        else if(tab == 3){
            //Map
            populateMap();
        }
    }

    void populateInventory(){
        GameObject slotsPanel = inventoryPanel.transform.GetChild(0).gameObject;

        int numItems = items.Count;

        GameObject[] invSlots = new GameObject[maxSlots];

        for(int i = 0; i < maxSlots; i++)
        {
            invSlots[i] = slotsPanel.transform.GetChild(i).gameObject;
            if(i < numItems)
            {
                Item item = items[i];

                invSlots[i].transform.GetChild(0).gameObject.SetActive(true);
                invSlots[i].transform.GetChild(0).transform.GetComponent<Image>().sprite = item.image;
                inventorySlots.Add(invSlots[i]);
            }
            else
            {
                invSlots[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        //int x = 0;
        //int y = 0;
        //float cellSize = 120;
        //int xOff = 100;
        //int yOff = -100;
        ////Debug.Log("NumItems:"+ numItems);
        //for(int i = 0; i < numItems; i++)
        //{
        //    //Debug.Log("Adding new Item");
        //    Vector3 pos = new Vector3(xOff + x * cellSize, yOff + -1 * y * cellSize, 0);

        //    Item item = items[i];

        //    GameObject itemslot = GameObject.Instantiate(slot, pos, Quaternion.identity);
        //    GameObject icon = itemslot.transform.GetChild(0).gameObject;
        //    icon.GetComponent<Image>().sprite = item.image;

        //    itemslot.transform.SetParent(topicArea.transform, false);
        //    inventorySlots.Add(itemslot);
        //    x++;
        //    if(x == 5){
        //        x = 0;
        //        y++;
        //    }
        //}
        //Debug.Log("Num Item slots:" + inventorySlots.Count);
        slotsLoaded = true;
    }

    void populateEquipment(){

        GameObject slotsPanel = equipmentPanel.transform.GetChild(0).gameObject;

        int numEquip = equipment.Count;

        GameObject[] equipSlots = new GameObject[maxSlots];

        for (int i = 0; i < maxSlots; i++)
        {
            equipSlots[i] = slotsPanel.transform.GetChild(i).gameObject;
            if (i < numEquip)
            {
                Equipment item = equipment[i];

                equipSlots[i].transform.GetChild(0).gameObject.SetActive(false);
                equipSlots[i].transform.GetChild(0).transform.GetComponent<Image>().sprite = item.image;
                equipmentSlots.Add(equipSlots[i]);
            }
            else
            {
                equipSlots[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        //GameObject topicArea = topicPanel.transform.GetChild(0).gameObject;

        //int numItems = equipment.Count;

        //int x = 0;
        //int y = 0;
        //float cellSize = 120;
        //int xOff = 100;
        //int yOff = -100;
        //for(int i = 0; i < numItems; i++)
        //{
        //    Vector3 pos = new Vector3(xOff + x * cellSize, yOff + -1 * y * cellSize, 0);

        //    Equipment item = equipment[i];

        //    GameObject itemslot = GameObject.Instantiate(slot, pos, Quaternion.identity);
        //    GameObject icon = itemslot.transform.GetChild(0).gameObject;
        //    icon.GetComponent<Image>().sprite = item.image;
        //    //Debug.Log("Creating Inventory Item " + item.tier);

        //    if(item.tier == 1) itemslot.GetComponent<Image>().sprite = frames[0];
        //    if(item.tier == 2) itemslot.GetComponent<Image>().sprite = frames[1];
        //    if(item.tier == 3) itemslot.GetComponent<Image>().sprite = frames[2];
        //    if(item.tier == 4) itemslot.GetComponent<Image>().sprite = frames[3];

        //    itemslot.transform.SetParent(topicArea.transform, false);
        //    equipmentSlots.Add(itemslot);
        //    x++;
        //    if(x == 5){
        //        x = 0;
        //        y++;
        //    }
        //}
        slotsLoaded = true;
    }

    void populateQuests(){
        quests.Add("Capture the orc.");
        quests.Add("Rescue the princess");
        quests.Add("Finish the game <333");

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

        GameObject mapArea = mapPanel.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;

        GameObject mapRoot = GameObject.Instantiate(minimap, new Vector3(0, 0, 0), Quaternion.identity);
        mapRoot.transform.SetParent(mapArea.transform, false);

        int size = 20;
        GameObject mapHolder = GameObject.FindGameObjectWithTag("Map").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        mapHolder.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width * size + 100);
        mapHolder.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height * size + 100);

        int xOff = -1 * ((width * size + 100) / 2) + 50;
        int yOff = -1 * ((height * size + 100) / 2) + 50;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (GameManager.gmInstance.Dungeon.tileMap[i, j].explored == false) continue;
                if (GameManager.gmInstance.Dungeon.tileMap[i, j].type == 2)
                {
                    Vector3 pos = new Vector3(xOff + j * size, yOff + i * size, 0f);
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
                if (GameManager.gmInstance.Dungeon.isItem(i, j))
                {
                    Vector3 pos = new Vector3(xOff + j * size, yOff + i * size, 0f);
                    GameObject b = GameObject.Instantiate(block, pos, Quaternion.identity);
                    b.GetComponent<Image>().sprite = icons[3];
                    b.transform.SetParent(mapHolder.transform, false);
                }
                if (GameManager.gmInstance.Dungeon.isStairs(i, j))
                {
                    //Debug.Log("STAIRS");
                    Vector3 pos = new Vector3(xOff + j * size, yOff + i * size, 0f);
                    GameObject b = GameObject.Instantiate(block, pos, Quaternion.identity);
                    b.GetComponent<Image>().sprite = icons[5];
                    b.transform.SetParent(mapHolder.transform, false);
                }

            }
        }
    }

    private void createPopup(){
        //Debug.Log("Create Popup");
        Vector3 pos = new Vector3(0, 0, 0);
        //Debug.Log("gear popup");
        GameObject popup = GameObject.Instantiate(itemPopup, pos, Quaternion.identity);
        GameObject mainHolder = popup.transform.GetChild(0).gameObject;

        GameObject modal1 = mainHolder.transform.GetChild(0).gameObject;
        GameObject modal2 = mainHolder.transform.GetChild(1).gameObject;

        GameObject itemName1 = modal1.transform.GetChild(0).gameObject;
        GameObject itemFlavor1 = modal1.transform.GetChild(1).gameObject;
        GameObject itemDesc1 = modal1.transform.GetChild(2).gameObject;

        GameObject itemName2 = modal2.transform.GetChild(0).gameObject;
        GameObject itemFlavor2 = modal2.transform.GetChild(1).gameObject;
        GameObject itemDesc2 = modal2.transform.GetChild(2).gameObject;

        GameObject buttonHolder = modal1.transform.GetChild(3).gameObject;
        GameObject button1 = buttonHolder.transform.GetChild(0).gameObject;
        GameObject button2 = buttonHolder.transform.GetChild(1).gameObject;
        GameObject button3 = buttonHolder.transform.GetChild(2).gameObject;
        
        popup.transform.SetParent(topicPanel.transform, false);
        popupArea = popup;

        if(tab == 0){
            Item i = items[selected];
            itemName1.transform.gameObject.GetComponent<TMP_Text>().text = i.itemName;
            itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = i.flavorText;
            itemDesc1.transform.gameObject.GetComponent<TMP_Text>().text = i.description;

            modal2.SetActive(false);
            button3.SetActive(false);

            button1.GetComponent<Button>().onClick.AddListener(useListener);
            button2.GetComponent<Button>().onClick.AddListener(trashListener);
            useButton = button1;
            trashButton = button2;
            compareButton = button3;
        }
        if(tab == 1){
            Item i = equipment[selected];
            itemName1.transform.gameObject.GetComponent<TMP_Text>().text = i.itemName;

            //itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = i.flavorText;
            if(i.tier == 1) itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = "Common";
            if(i.tier == 2) itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = "Rare";
            if(i.tier == 3) itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = "Unique";
            if(i.tier == 4) itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = "Legendary";
            
            itemDesc1.transform.gameObject.GetComponent<TMP_Text>().text = i.description;

            button1.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Equip";

            modal2.SetActive(false);
            
            button1.GetComponent<Button>().onClick.AddListener(equipListener);
            button2.GetComponent<Button>().onClick.AddListener(trashListener);
            button3.GetComponent<Button>().onClick.AddListener(compareListener);
            useButton = button1;
            trashButton = button2;
            compareButton = button3;
        }
        
    }

    void createGearPopup(){
        charPanel = panelObject.transform.GetChild(0).gameObject;
        GameObject middleStats = charPanel.transform.GetChild(1).gameObject;
        GameObject gearSlot = middleStats.transform.GetChild(4+gearSelection).gameObject;

        Equipment e = new Equipment();

        if(gearSelection == 0) e = gear.Helmet;
        if(gearSelection == 1) e = gear.Chestplate;
        if(gearSelection == 2) e = gear.Legs;
        if(gearSelection == 3) e = gear.Feet;
        if(gearSelection == 4) e = gear.Weapon;
        if(gearSelection == 5) e = gear.Secondary;
        if(gearSelection == 6) e = gear.Necklace;
        if(gearSelection == 7) e = gear.Ring;

        if(e == null)return;

        Vector3 pos = new Vector3(0, 0, 0);
        //Debug.Log("Gear Popup");
        GameObject popup = GameObject.Instantiate(itemPopup, pos, Quaternion.identity);
        GameObject mainHolder = popup.transform.GetChild(0).gameObject;

        GameObject modal1 = mainHolder.transform.GetChild(0).gameObject;
        GameObject modal2 = mainHolder.transform.GetChild(1).gameObject;

        GameObject itemName1 = modal1.transform.GetChild(0).gameObject;
        GameObject itemFlavor1 = modal1.transform.GetChild(1).gameObject;
        GameObject itemDesc1 = modal1.transform.GetChild(2).gameObject;

        GameObject itemName2 = modal2.transform.GetChild(0).gameObject;
        GameObject itemFlavor2 = modal2.transform.GetChild(1).gameObject;
        GameObject itemDesc2 = modal2.transform.GetChild(2).gameObject;

        GameObject buttonHolder = modal1.transform.GetChild(3).gameObject;
        GameObject button1 = buttonHolder.transform.GetChild(0).gameObject;
        GameObject button2 = buttonHolder.transform.GetChild(1).gameObject;
        GameObject button3 = buttonHolder.transform.GetChild(2).gameObject;
        
        popup.transform.SetParent(topicPanel.transform, false);
        popupArea = popup;
        
        itemName1.transform.gameObject.GetComponent<TMP_Text>().text = e.itemName;

        //itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = e.flavorText;
        if(e.tier == 1) itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = "Common";
        if(e.tier == 2) itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = "Rare";
        if(e.tier == 3) itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = "Unique";
        if(e.tier == 4) itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = "Legendary";

        itemDesc1.transform.gameObject.GetComponent<TMP_Text>().text = e.description;

        modal2.SetActive(false);
        button3.SetActive(false);

        button1.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Unequip";

        button1.GetComponent<Button>().onClick.AddListener(unequipListener);
        button2.GetComponent<Button>().onClick.AddListener(trashEquipedListener);
        useButton = button1;
        trashButton = button2;
        compareButton = button3;
        
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
        SoundManager.sm.PlayMenuSound();
        closeInventory();
    }

    void itemListener(){
        SoundManager.sm.PlayMenuSound();
        tab = 0;
        refreshTopicPanel();
    }
    void equipmentListener(){
        SoundManager.sm.PlayMenuSound();
        tab = 1;
        refreshTopicPanel();
    }
    void questListener(){
        SoundManager.sm.PlayMenuSound();
        tab = 2;
        refreshTopicPanel();
    }
    void mapListener(){
        SoundManager.sm.PlayMenuSound();
        tab = 3;
        refreshTopicPanel();
    }
    void useListener(){
        SoundManager.sm.PlayMenuSound();
        Debug.Log("USE ITEM");
        items[selected].UseItem();
        items.RemoveAt(selected);
        selected = -1;
        //Debug.Log("DESTROY");
        GameObject.Destroy(popupArea);
        popupOpen = false;
        refreshTopicPanel();
        setPlayerStats();
    }
    void equipListener(){
        SoundManager.sm.PlayMenuSound();
        //Debug.Log("EQUIP");

        //equip armor
        Equipment e = equipment[selected];
        Equipment old = new Equipment();


        if(e.type == "helmet"){
            old = gear.Helmet;
            gear.Helmet = e;
        } 
        if(e.type == "chestplate"){
            old = gear.Chestplate;
            gear.Chestplate = e;
        } 
        if(e.type == "legs"){
            old = gear.Legs;
            gear.Legs = e;
        } 
        if(e.type == "boots"){
            old = gear.Feet;
            gear.Feet = e;
        } 
        if(e.type == "weapon"){
            old = gear.Weapon;
            gear.Weapon = e;
        } 
        if(e.type == "shield"){
            old = gear.Secondary;
            gear.Secondary = e;
        } 
        if(e.type == "necklace"){
            old = gear.Necklace;
            gear.Necklace = e;
        } 
        if(e.type == "ring"){
            old = gear.Ring;
            gear.Ring = e;
        } 

        //remove equipment from inventory
        equipment.RemoveAt(selected);

        //add old armor to inventory
        if(old != null) addEquipment(old);

        selected = -1;
        GameObject.Destroy(popupArea);
        popupOpen = false;
        //openStats();
        removeGearStats(old);
        applyGearStats(e);

        refreshTopicPanel();
        setPlayerStats();
    }

    void unequipListener(){
        SoundManager.sm.PlayMenuSound();
        Equipment e = new Equipment();

        if(gearSelection == 0){ 
            e = gear.Helmet; 
            gear.Helmet = null;
        }
        if(gearSelection == 1){
            e = gear.Chestplate;
            gear.Chestplate = null;
        } 
        if(gearSelection == 2){
            e = gear.Legs;
            gear.Legs = null;
        } 
        if(gearSelection == 3){
            e = gear.Feet;
            gear.Feet = null;
        } 
        if(gearSelection == 4){
            e = gear.Weapon;
            gear.Weapon = null;
        } 
        if(gearSelection == 5){
            e = gear.Secondary;
            gear.Secondary = null;
        } 
        if(gearSelection == 6){
            e = gear.Necklace;
            gear.Necklace = null;
        } 
        if(gearSelection == 7){
            e = gear.Ring;
            gear.Ring = null;
        } 

        if(e != null) addEquipment(e);

        gearSelection = -1;
        selected = -1;
        GameObject.Destroy(popupArea);
        popupOpen = false;
        //openStats();

        removeGearStats(e);

        refreshTopicPanel();
        setPlayerStats();
    }

    void applyGearStats(Equipment e){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        int att = e.attack;
        int hp = e.defense;
        int crit = e.crit;
        int intel = e.intelligence;
        //int block = e.block;
        //int evade = e.evade;

        //Debug.Log("GEAR STR IS "+ e.attack);

        player.addAttack(att);
        player.addTotalHP(hp);
        player.addCrit(crit);
        player.addIntelligence(intel);
    }

    void removeGearStats(Equipment e){
        if (e == null)return;

        int att = e.attack;
        int hp = e.defense;
        int crit = e.crit;
        int intel = e.intelligence;
        //int block = e.block;
        //int evade = e.evade;

        player.addAttack(-att);
        player.addMaxHP(-hp);
        player.addCrit(-crit);
        player.addIntelligence(-intel);
    }

    void trashListener(){
        SoundManager.sm.PlayMenuSound();
        if(tab == 0){
            items.RemoveAt(selected);
        }
        if(tab == 1){
            equipment.RemoveAt(selected);
        }
        
        selected = -1;
        GameObject.Destroy(popupArea);
        popupOpen = false;
        refreshTopicPanel();
    }

    void trashEquipedListener(){
        SoundManager.sm.PlayMenuSound();
        Equipment e = new Equipment();

        if(gearSelection == 0){ 
            e = gear.Helmet; 
            gear.Helmet = null;
        }
        if(gearSelection == 1){
            e = gear.Chestplate;
            gear.Chestplate = null;
        } 
        if(gearSelection == 2){
            e = gear.Legs;
            gear.Legs = null;
        } 
        if(gearSelection == 3){
            e = gear.Feet;
            gear.Feet = null;
        } 
        if(gearSelection == 4){
            e = gear.Weapon;
            gear.Weapon = null;
        } 
        if(gearSelection == 5){
            e = gear.Secondary;
            gear.Secondary = null;
        } 
        if(gearSelection == 6){
            e = gear.Necklace;
            gear.Necklace = null;
        } 
        if(gearSelection == 7){
            e = gear.Ring;
            gear.Ring = null;
        } 

        gearSelection = -1;
        GameObject.Destroy(popupArea);
        popupOpen = false;
        refreshTopicPanel();
        setPlayerStats();
    }

    void compareListener(){
        SoundManager.sm.PlayMenuSound();
        //Debug.Log("COMPARE");
        Equipment e = equipment[selected];
        Equipment alt = new Equipment();

        if(e.type == "helmet"){
            alt = gear.Helmet;
        } 
        if(e.type == "chestplate"){
            alt = gear.Chestplate;
        } 
        if(e.type == "legs"){
            alt = gear.Legs;
        } 
        if(e.type == "boots"){
            alt = gear.Feet;
        } 
        if(e.type == "weapon"){
            alt = gear.Weapon;
        } 
        if(e.type == "shield"){
            alt = gear.Secondary;
        } 
        if(e.type == "necklace"){
            alt = gear.Necklace;
        } 
        if(e.type == "ring"){
            alt = gear.Ring;
        } 
        if(alt == null)return;

        openComparePopup(alt);
    }

    void openComparePopup(Equipment alt){
        GameObject mainHolder = popupArea.transform.GetChild(0).gameObject;
        GameObject modal2 = mainHolder.transform.GetChild(1).gameObject;
        modal2.SetActive(true);

        GameObject itemName2 = modal2.transform.GetChild(0).gameObject;
        GameObject itemFlavor2 = modal2.transform.GetChild(1).gameObject;
        GameObject itemDesc2 = modal2.transform.GetChild(2).gameObject;

        itemName2.transform.gameObject.GetComponent<TMP_Text>().text = alt.itemName;
        //itemFlavor2.transform.gameObject.GetComponent<TMP_Text>().text = alt.flavorText;
        if(alt.tier == 1) itemFlavor2.transform.gameObject.GetComponent<TMP_Text>().text = "Common";
        if(alt.tier == 2) itemFlavor2.transform.gameObject.GetComponent<TMP_Text>().text = "Rare";
        if(alt.tier == 3) itemFlavor2.transform.gameObject.GetComponent<TMP_Text>().text = "Unique";
        if(alt.tier == 4) itemFlavor2.transform.gameObject.GetComponent<TMP_Text>().text = "Legendary";
        itemDesc2.transform.gameObject.GetComponent<TMP_Text>().text = alt.description;
    }

    void refreshTopicPanel(){
        slotsLoaded = false;
        foreach(GameObject o in inventorySlots){
            GameObject.Destroy(o);
        }
        inventorySlots.Clear();
        foreach(GameObject o in equipmentSlots){
            GameObject.Destroy(o);
        }
        equipmentSlots.Clear();
        foreach(GameObject o in questObjects){
            GameObject.Destroy(o);
        }
        questObjects.Clear();
        GameObject.Destroy(mapRoot);
        setTopicPanel();
        populateTopicArea();
    }

    void applyGearStats(){

    }

    public void addItem(Item i){
        items.Add(i as Consumable);
    }

    public void addEquipment(Item i){
        equipment.Add(i as Equipment);
    }

    public void closeInventory()
    {
        closePopup();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.setGear(gear);
        closed = true;
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
