using UnityEngine;
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

    GameObject statusPanel;
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
    Animator anim;

    Sprite[] tabs;

    public List<Consumable> items;
    public List<Equipment> equipment;

    public List<GameObject> inventorySlots;
    public List<GameObject> equipmentSlots;

    GameObject[] invSlots;
    GameObject[] equipSlots;

    public List<string> quests;
    public List<GameObject> questObjects;

    bool closed;
    string topicTitleString = "Inventory";
    int tab = 0;

    private int[,] map;
    private int width = 50;
    private int height = 50;
    GameObject mapRoot;
    public Sprite[] icons;
    public Sprite s;
    int selected = -1;
    int gearSelection = -1;
    bool popupOpen = false;
    bool slotsLoaded = false;
    private Sprite[] frames;

    public int maxSlots = 25;

    private bool flipping = false;
    private bool flipping2 = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {

        if (flipping)
        {
            Debug.Log("Flipping");
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BookLeft") || anim.GetCurrentAnimatorStateInfo(0).IsName("BookRight"))
            {
                flipping2 = true;
                Debug.Log("In Progress");
            }
            if(flipping2 && anim.GetCurrentAnimatorStateInfo(0).IsName("BookIdle")) {
                Debug.Log("Done flipping");
                flipping = false;
                flipping2 = false;
                refreshTopicPanel();
            }
        }

        if (Input.GetKeyDown("left"))
        {
            anim.Play("BookLeft");
            Debug.Log("Left");
        }
        if (Input.GetKeyDown("right"))
        {
            anim.Play("BookRight");
            Debug.Log("Right");
        }
        
        if(popupOpen){
            //Debug.Log("POPUP OPEN");
            //close when click outside popup
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("use:" + useButton);
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
                GameObject gearImage = gearSlot.transform.GetChild(1).gameObject;
                if(gearSlot.GetComponent<Clickable>().getClicked() && gearImage.active){
                    gearSlot.GetComponent<Clickable>().setClicked(false);
                    gearSelection = i;
                    popupOpen = true;
                    createGearPopup();
                    SoundManager.sm.PlayMenuSound();
                    return;
                }
            }
            if(tab == 0){
                for(int i = 0; i < items.Count; i++)
                {
                    if (invSlots != null && invSlots[i].GetComponent<Clickable>().getClicked())
                    {
                        selected = i;
                        invSlots[i].GetComponent<Clickable>().setClicked(false);
                        popupOpen = true;
                        createPopup();
                        SoundManager.sm.PlayMenuSound();
                    }
                }
            }
            else if(tab == 1){
                for(int i = 0; i < equipment.Count; i++)
                {
                    if(equipSlots != null && equipSlots[i].GetComponent<Clickable>().getClicked())
                    {
                        selected = i;
                        equipSlots[i].GetComponent<Clickable>().setClicked(false);
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
            //check equiped gear for clicks
            GameObject status = panelObject.transform.GetChild(1).gameObject;
            GameObject equipment = status.transform.GetChild(6).gameObject;
            GameObject gearSlot = equipment.transform.GetChild(i).gameObject;
            GameObject gearImage = gearSlot.transform.GetChild(1).gameObject;
            if (gearSlot.GetComponent<Clickable>().getClicked() && gearImage.active)
            {
                gearSlot.GetComponent<Clickable>().setClicked(false);
            }
        }
        if (tab == 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (invSlots != null && invSlots[i].GetComponent<Clickable>().getClicked())
                {
                    invSlots[i].GetComponent<Clickable>().setClicked(false);
                }
            }
        }
        if (tab == 1)
        {
            for (int i = 0; i < equipment.Count; i++)
            {
                if (equipSlots != null && equipSlots[i].GetComponent<Clickable>().getClicked())
                {
                    equipSlots[i].GetComponent<Clickable>().setClicked(false);
                }
            }
        }
    }

    public CharacterMenu(GameObject panel, GameObject slot, GameObject questLine, GameObject minimap, GameObject block, GameObject pblock, GameObject itemPopup, Sprite[] frames, Sprite[] tabs)
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
        this.tabs = tabs;

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

        anim = panelObject.GetComponent<Animator>();

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
        nameBanner.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = player.getName();

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

        hpText.GetComponent<TMP_Text>().text = "HP: " + hp + "/" + maxHp;
        expText.GetComponent<TMP_Text>().text = "EXP: " + exp + "/" + maxExp;

        levelText.transform.GetChild(1).transform.GetComponent<TMP_Text>().text = player.getExpLevel().ToString();

        GameObject redbar = hpBar.transform.GetChild(0).gameObject;
        GameObject greenbar = expBar.transform.GetChild(0).gameObject;

        Canvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        Vector2 canvasScale = new Vector2(canvas.transform.localScale.x, canvas.transform.localScale.y);

        Vector2 redSizeDelta = redbar.transform.GetComponent<RectTransform>().sizeDelta;
        Vector2 redFinalScale = new Vector2(redSizeDelta.x * canvasScale.x, redSizeDelta.y * canvasScale.y);
        float redWidth = redFinalScale.x * panelObject.transform.GetComponent<RectTransform>().localScale.x;

        Vector3 pos1 = redbar.transform.position;
        float redStartingPos = hpBar.transform.GetChild(1).gameObject.transform.position.x;
        float redPos = redStartingPos - (redWidth - (((float)hp / (float)maxHp) * redWidth));
        Debug.Log($"{redStartingPos} - {redWidth} - {(float)hp / (float)maxHp} * {redWidth} = {redPos}");
        redbar.transform.GetComponent<RectTransform>().position = new Vector3(redPos, pos1.y, pos1.z);

        Vector2 greenSizeDelta = greenbar.transform.GetComponent<RectTransform>().sizeDelta;
        Vector2 greenFinalScale = new Vector2(greenSizeDelta.x * canvasScale.x, greenSizeDelta.y * canvasScale.y);
        float greenWidth = greenFinalScale.x * panelObject.transform.GetComponent<RectTransform>().localScale.x;

        Vector3 pos2 = greenbar.transform.position;
        float greenStartingPos = expBar.transform.GetChild(1).gameObject.transform.position.x;
        float greenPos = greenStartingPos + (((float)exp / (float)maxExp) * greenWidth);
        greenbar.transform.GetComponent<RectTransform>().position = new Vector3(greenPos, pos2.y, pos2.z);

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
        statusPanel = panelObject.transform.GetChild(1).gameObject;
        inventoryPanel = panelObject.transform.GetChild(2).gameObject;
        equipmentPanel = panelObject.transform.GetChild(3).gameObject;
        mapPanel = panelObject.transform.GetChild(4).gameObject;

        statusPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        equipmentPanel.SetActive(false);
        mapPanel.SetActive(false);

        inventoryTab.transform.GetComponent<Image>().sprite = tabs[1];
        equipmentTab.transform.GetComponent<Image>().sprite = tabs[1];
        mapTab.transform.GetComponent<Image>().sprite = tabs[1];

        if (tab == 0)
        {
            inventoryPanel.SetActive(true);
            inventoryTab.transform.GetComponent<Image>().sprite = tabs[0];
        }
        else if(tab == 1)
        {
            equipmentPanel.SetActive(true);
            equipmentTab.transform.GetComponent<Image>().sprite = tabs[0];
        }
        else if(tab == 3)
        {
            mapPanel.SetActive(true);
            mapTab.transform.GetComponent<Image>().sprite = tabs[0];
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

        invSlots = new GameObject[maxSlots];

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
        Debug.Log("Count:" + numEquip);

        equipSlots = new GameObject[maxSlots];

        for (int i = 0; i < maxSlots; i++)
        {
            equipSlots[i] = slotsPanel.transform.GetChild(i).gameObject;
            if (i < numEquip)
            {
                Equipment item = equipment[i];

                equipSlots[i].transform.GetChild(0).gameObject.SetActive(true);
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

        GameObject mapArea = mapPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;

        mapRoot = GameObject.Instantiate(minimap, new Vector3(0, 0, 0), Quaternion.identity);
        mapRoot.transform.SetParent(mapArea.transform, false);

        int size = 20;
        GameObject mapHolder = mapRoot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        mapHolder.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width * size + 100);
        mapHolder.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height * size + 100);

        //mapRoot.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, mapArea.GetComponent<RectTransform>().rect.width);
        //mapRoot.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, mapArea.GetComponent<RectTransform>().rect.height);

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
        Vector3 pos = new Vector3(0, 0, 0);
        GameObject popup = GameObject.Instantiate(itemPopup, pos, Quaternion.identity);
        GameObject mainHolder = popup.transform.GetChild(0).gameObject;

        GameObject primary = mainHolder.transform.GetChild(0).gameObject;
        GameObject modal1 = primary.transform.GetChild(0).gameObject;
        GameObject tags1 = primary.transform.GetChild(1).gameObject;
        GameObject top1 = modal1.transform.GetChild(0).gameObject;
        GameObject itemFrame1 = top1.transform.GetChild(0).gameObject;
        GameObject textHolder1 = top1.transform.GetChild(1).gameObject;
        GameObject itemName1 = textHolder1.transform.GetChild(0).gameObject;
        GameObject itemRank1 = textHolder1.transform.GetChild(1).gameObject;
        GameObject itemDesc1 = modal1.transform.GetChild(1).gameObject;
        useButton = tags1.transform.GetChild(0).gameObject;
        trashButton = tags1.transform.GetChild(1).gameObject;
        compareButton = tags1.transform.GetChild(2).gameObject;

        GameObject secondary = mainHolder.transform.GetChild(1).gameObject;
        GameObject modal2 = secondary.transform.GetChild(0).gameObject;
        GameObject top2 = modal2.transform.GetChild(0).gameObject;
        GameObject itemFrame2 = top2.transform.GetChild(0).gameObject;
        GameObject textHolder2 = top2.transform.GetChild(1).gameObject;
        GameObject itemName2 = textHolder2.transform.GetChild(0).gameObject;
        GameObject itemRank2 = textHolder2.transform.GetChild(1).gameObject;
        GameObject itemDesc2 = modal2.transform.GetChild(1).gameObject;
        
        popup.transform.SetParent(panelObject.transform, false);
        popupArea = popup;

        if(tab == 0){
            Item i = items[selected];
            itemName1.transform.gameObject.GetComponent<TMP_Text>().text = i.itemName;
            itemRank1.transform.gameObject.GetComponent<TMP_Text>().text = i.flavorText;
            itemDesc1.transform.gameObject.GetComponent<TMP_Text>().text = i.description;
            itemFrame1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = i.image;

            secondary.SetActive(false);
            compareButton.SetActive(false);

            useButton.GetComponent<Button>().onClick.AddListener(useListener);
            trashButton.GetComponent<Button>().onClick.AddListener(trashListener);
        }
        if(tab == 1){
            Item i = equipment[selected];
            itemName1.transform.gameObject.GetComponent<TMP_Text>().text = i.itemName;

            //itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = i.flavorText;
            if(i.tier == 1) itemRank1.transform.gameObject.GetComponent<TMP_Text>().text = "Common";
            if (i.tier == 2)
            {
                itemRank1.transform.gameObject.GetComponent<TMP_Text>().text = "Rare";
                itemRank1.transform.gameObject.GetComponent<TMP_Text>().color = Color.white;
            }
            if (i.tier == 3)
            {
                itemRank1.transform.gameObject.GetComponent<TMP_Text>().text = "Unique";
                itemRank1.transform.gameObject.GetComponent<TMP_Text>().color = new Color(1f, 215f / 255f, 0f, 1f);
            }
            if (i.tier == 4)
            {
                itemRank1.transform.gameObject.GetComponent<TMP_Text>().text = "Legendary";
                itemRank1.transform.gameObject.GetComponent<TMP_Text>().color = new Color(185f / 255f, 242f / 255f, 255f / 255f, 1f);
            }

            itemDesc1.transform.gameObject.GetComponent<TMP_Text>().text = i.description;
            itemFrame1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = i.image;

            useButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Equip";

            secondary.SetActive(false);
            
            useButton.GetComponent<Button>().onClick.AddListener(equipListener);
            trashButton.GetComponent<Button>().onClick.AddListener(trashListener);
            compareButton.GetComponent<Button>().onClick.AddListener(compareListener);
        }
        
    }

    void createGearPopup(){
        //charPanel = panelObject.transform.GetChild(0).gameObject;
        //GameObject middleStats = charPanel.transform.GetChild(1).gameObject;
        //GameObject gearSlot = middleStats.transform.GetChild(4+gearSelection).gameObject;

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

        GameObject primary = mainHolder.transform.GetChild(0).gameObject;
        GameObject modal1 = primary.transform.GetChild(0).gameObject;
        GameObject tags1 = primary.transform.GetChild(1).gameObject;
        GameObject top1 = modal1.transform.GetChild(0).gameObject;
        GameObject itemFrame1 = top1.transform.GetChild(0).gameObject;
        GameObject textHolder1 = top1.transform.GetChild(1).gameObject;
        GameObject itemName1 = textHolder1.transform.GetChild(0).gameObject;
        GameObject itemRank1 = textHolder1.transform.GetChild(1).gameObject;
        GameObject itemDesc1 = modal1.transform.GetChild(1).gameObject;
        useButton = tags1.transform.GetChild(0).gameObject;
        trashButton = tags1.transform.GetChild(1).gameObject;
        compareButton = tags1.transform.GetChild(2).gameObject;

        GameObject secondary = mainHolder.transform.GetChild(1).gameObject;


        popup.transform.SetParent(panelObject.transform, false);
        popupArea = popup;
        
        itemName1.transform.gameObject.GetComponent<TMP_Text>().text = e.itemName;

        //itemFlavor1.transform.gameObject.GetComponent<TMP_Text>().text = e.flavorText;
        if(e.tier == 1) itemRank1.transform.gameObject.GetComponent<TMP_Text>().text = "Common";
        if (e.tier == 2)
        {
            itemRank1.transform.gameObject.GetComponent<TMP_Text>().text = "Rare";
            itemRank1.transform.gameObject.GetComponent<TMP_Text>().color = Color.white;
        }
        if (e.tier == 3)
        {
            itemRank1.transform.gameObject.GetComponent<TMP_Text>().text = "Unique";
            itemRank1.transform.gameObject.GetComponent<TMP_Text>().color = new Color(1f, 215f / 255f, 0f, 1f);
        }
        if (e.tier == 4)
        {
            itemRank1.transform.gameObject.GetComponent<TMP_Text>().text = "Legendary";
            itemRank1.transform.gameObject.GetComponent<TMP_Text>().color = new Color(185f / 255f, 242f / 255f, 255f / 255f, 1f);
        }

        itemDesc1.transform.gameObject.GetComponent<TMP_Text>().text = e.description;
        itemFrame1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = e.image;

        secondary.SetActive(false);
        compareButton.SetActive(false);

        useButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Unequip";

        useButton.GetComponent<Button>().onClick.AddListener(unequipListener);
        trashButton.GetComponent<Button>().onClick.AddListener(trashEquipedListener);
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
        SoundManager.sm.PlayBookClose();
        closeInventory();
    }

    string flipDirection(int newTab)
    {
        if(newTab > tab)
        {
            return "BookLeft";
        }else if(newTab < tab)
        {
            return "BookRight";
        }
        else
        {
            return "BookIdle";
        }
    }

    void itemListener(){
        if (tab == 0) return;
        SoundManager.sm.PlayPageTurn();

        statusPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        equipmentPanel.SetActive(false);
        mapPanel.SetActive(false);

        anim.Play(flipDirection(0));
        flipping = true;

        tab = 0;
    }
    void equipmentListener(){
      
        if (tab == 1) return;
        SoundManager.sm.PlayPageTurn();

        statusPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        equipmentPanel.SetActive(false);
        mapPanel.SetActive(false);

        anim.Play(flipDirection(1));
        flipping = true;

        tab = 1;
    }
    void questListener(){
        SoundManager.sm.PlayMenuSound();
        tab = 2;
        refreshTopicPanel();
    }
    void mapListener(){
        if (tab == 3) return;
        SoundManager.sm.PlayPageTurn();

        statusPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        equipmentPanel.SetActive(false);
        mapPanel.SetActive(false);

        anim.Play(flipDirection(3));
        flipping = true;

        tab = 3;
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
        //SoundManager.sm.PlayMenuSound();
        SoundManager.sm.PlayEquipSound();
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
        //SoundManager.sm.PlayMenuSound();
        SoundManager.sm.PlayUnequipSound();
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

        GameObject secondary = mainHolder.transform.GetChild(1).gameObject;
        GameObject modal2 = secondary.transform.GetChild(0).gameObject;
        GameObject top2 = modal2.transform.GetChild(0).gameObject;
        GameObject itemFrame2 = top2.transform.GetChild(0).gameObject;
        GameObject textHolder2 = top2.transform.GetChild(1).gameObject;
        GameObject itemName2 = textHolder2.transform.GetChild(0).gameObject;
        GameObject itemRank2 = textHolder2.transform.GetChild(1).gameObject;
        GameObject itemDesc2 = modal2.transform.GetChild(1).gameObject;
        
        secondary.SetActive(true);

        itemName2.transform.gameObject.GetComponent<TMP_Text>().text = alt.itemName;
        if(alt.tier == 1) itemRank2.transform.gameObject.GetComponent<TMP_Text>().text = "Common";
        if (alt.tier == 2)
        {
            itemRank2.transform.gameObject.GetComponent<TMP_Text>().text = "Rare";
            itemRank2.transform.gameObject.GetComponent<TMP_Text>().color = Color.white;
        }
        if (alt.tier == 3)
        {
            itemRank2.transform.gameObject.GetComponent<TMP_Text>().text = "Unique";
            itemRank2.transform.gameObject.GetComponent<TMP_Text>().color = new Color(1f, 215f / 255f, 0f, 1f);
        }
        if (alt.tier == 4)
        {
            itemRank2.transform.gameObject.GetComponent<TMP_Text>().text = "Legendary";
            itemRank2.transform.gameObject.GetComponent<TMP_Text>().color = new Color(185f / 255f, 242f / 255f, 255f / 255f, 1f);
        }
        itemDesc2.transform.gameObject.GetComponent<TMP_Text>().text = alt.description;
        itemFrame2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = alt.image;
    }

    void refreshTopicPanel(){
        slotsLoaded = false;
        //foreach(GameObject o in inventorySlots){
        //    GameObject.Destroy(o);
        //}
        //inventorySlots.Clear();
        //foreach(GameObject o in equipmentSlots){
        //    GameObject.Destroy(o);
        //}
        //equipmentSlots.Clear();
        //foreach(GameObject o in questObjects){
        //    GameObject.Destroy(o);
        //}
        //questObjects.Clear();
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
