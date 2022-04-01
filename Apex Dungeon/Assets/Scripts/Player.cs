using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Player : MovingEntity
{
    private Image hpbar;
    private Image mpbar;
    private Button character;
    public GameObject characterPanel;
    public GameObject slot;
    public GameObject block;
    public GameObject pblock;
    public GameObject mapHolder;
    public GameObject questLine;
    public GameObject mapArea;
    public GameObject itemPopup;
    public GameObject levelPopup;

    private CharacterMenu charMenu;
    private GameObject levelPopHolder;
    private int gold;
    Animator animator;
    private bool openCharacter = false;
    private bool openLevel = false;
    private bool opening = true;
    private bool fadeIn = false;
    private Time time;
    private float start;
    private int prevLevel;
    private string levelStat;
    private PlayerGear gear;
    protected override void Start()
    {
        hp = 100;
        mp = 50;
        maxMp = 50;
        maxHp = 100;

        expLevel = 5;
        exp = 50;
        maxExp = 100;

        damage = 10;
        defense = 10;
        intelligence = 5;
        critical = 8;
        evade = 15;
        blockStat = 12;

        type = 1;
        gold = 0;
        start = 0;

        animator = GetComponent<Animator>();
        gear = new PlayerGear();
        charMenu = new CharacterMenu(characterPanel, slot, questLine, mapArea, block, pblock, itemPopup);

        if (GameManager.gmInstance.level > 1)
        {
            loadCharacterData();
        }

        hpbar = GameObject.FindGameObjectWithTag("hpbar").transform.GetChild(1).gameObject.GetComponent<Image>();
        mpbar = GameObject.FindGameObjectWithTag("mpbar").transform.GetChild(1).gameObject.GetComponent<Image>();
        character = GameObject.FindGameObjectWithTag("characterButton").GetComponent<Button>();

        character.onClick.AddListener(characterListener);

        base.Start();
    }

    void moveController(int clickRow, int clickCol)
    {
        bool val;
        val = AttemptMove<Player>(clickRow, clickCol);
        GameManager.gmInstance.playersTurn = false;
    }

    void checkDead()
    {
        if (dead)
        {
            //calculate and save score

            //reset character stats

            //load score screen
        }
    }

    void setFadeIn()
    {

    }

    protected override void Update()
    {
        if (!GameManager.gmInstance.playersTurn) return;

        updateUI();

        GameManager.gmInstance.Dungeon.UpdateShadows(row, col);

        if (openCharacter)
        {
            if (charMenu.getClosed())
            {
                charMenu.setClosed(false);
                openCharacter = false;
            }
            return;
        }

        if(openLevel){
            return;
        }

        //Debugging tool
        if (Input.GetKeyDown("t"))
        {
            nextFloor();
        }

        checkDead();
        base.Update();
        checkMoving();
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
                return;
            }

            int clickRow = (int)GameManager.gmInstance.mRow;
            int clickCol = (int)GameManager.gmInstance.mCol;

            //attack or interact
            if (isAdjacent(clickRow, clickCol))
            {
                if(isFurniture(clickRow, clickCol))
                {
                    return;
                }
                else
                {
                    attackController(clickRow, clickCol);
                }
            }

            //move
            moveController(clickRow, clickCol);
        }
    }

    public void saveCharacterData(){
        Data.hp = hp;
        Data.maxHp = maxHp;
        Data.mp = mp;
        Data.maxMp = maxMp;
        Data.exp = exp;
        Data.maxExp = maxExp;
        Data.strength = damage;
        Data.defense = defense;
        Data.intelligence = intelligence;
        Data.crit = critical;
        Data.evade = evade;
        Data.block = blockStat;
        Data.gold = gold;

        Data.charMenu = charMenu;
    }

    public void loadCharacterData(){
        hp = Data.hp;
        maxHp = Data.maxHp;
        mp = Data.mp;
        maxMp = Data.maxMp;
        exp = Data.exp;
        maxExp = Data.maxExp;
        damage = Data.strength;
        defense = Data.defense;
        intelligence = Data.intelligence;
        critical = Data.crit;
        evade = Data.evade;
        blockStat = Data.block;
        gold = Data.gold;

        charMenu = Data.charMenu;
    }

    private void nextFloor(){
        GameManager.gmInstance.level++;
        saveCharacterData();
        GameManager.gmInstance.Reset();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Stairs")
        {
            nextFloor();
        }
        if(other.gameObject.tag == "Potion")
        {
            charMenu.addItem(other.GetComponent<Consumable>());
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Gold")
        {
            Destroy(other.gameObject);
            gold += 100;
        }
        if (other.gameObject.tag == "Silver")
        {
            Destroy(other.gameObject);
            gold += 50;
        }
        if (other.gameObject.tag == "Copper")
        {
            Destroy(other.gameObject);
            gold += 25;
        }
        if (other.gameObject.tag == "Equipment")
        {
            charMenu.addEquipment(other.GetComponent<Equipment>());
            Destroy(other.gameObject);
            
        }
    }

    bool isFurniture(int row, int col)
    {
        Furniture f = GameManager.gmInstance.getFurnitureAtLoc(row, col);
        if (f != null)
        {
            f.setDamage(-1);
            GameManager.gmInstance.playersTurn = false;
            return true;
        }
        else return false;
    }

    void characterListener()
    {
        if (!openCharacter)
        {
            charMenu.openStats();
            openCharacter = true;
        }
    }

    void checkMoving()
    {
        if (moving)
        {
            if (atTarget)
            {
                setNextTarget();
                GameManager.gmInstance.playersTurn = false;
            }

            return;
        }
    }

    void attackController(int clickRow, int clickCol)
    {
        
            Enemy enemy = GameManager.gmInstance.getEnemyAtLoc(clickRow, clickCol);
            if (enemy != null)
            {
                enemy.takeDamage(-10);
                GameManager.gmInstance.playersTurn = false;
                return;
            }
        
    }

    bool isAdjacent(int r, int c)
    {
        int rDis = Mathf.Abs((r) - row);
        int cDis = Mathf.Abs(c - col);
        if ((rDis == 1 && cDis == 0) || (rDis == 0 && cDis == 1))
        {
            return true;
        }
        return false;
    }

    protected override bool AttemptMove<T>(int r, int c)
    {
        bool canMove = base.AttemptMove<T>(r,c);
        
        if(canMove)
        {
            return true;
        }
        return false;
    }

    protected override void OnCantMove<T>(T Component)
    {
        //INTERACTION
    }

    public void addMP(int i)
    {
        mp += i;
    }
    public void addHP(int i)
    {
        hp += i;
    }
    public void addExp(int i){
        exp += i;
        bool levelUp = false;
        prevLevel = expLevel;
        while(exp >= maxExp){
            exp -= maxExp;
            expLevel++;
            maxExp += (int)(0.5 * maxExp);
            levelUp = true;
        }
        if(levelUp){
            openLevel = true;
            createLevelPopup();
        }
        
    }

    void createLevelPopup(){
        Vector3 pos = new Vector3(0, 0, 0);

        GameObject levelPop = GameObject.Instantiate(levelPopup, pos, Quaternion.identity);
        levelPop.transform.SetParent(GameObject.FindGameObjectWithTag("LevelUp").transform, false);
        levelPopHolder = levelPop;

        levelPop.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = prevLevel + " -> " + expLevel;
        GameObject Strength = levelPop.transform.GetChild(3).gameObject;
        GameObject Defense = levelPop.transform.GetChild(4).gameObject;
        GameObject Crit = levelPop.transform.GetChild(5).gameObject;
        GameObject Intelligence = levelPop.transform.GetChild(6).gameObject;
        GameObject Evade = levelPop.transform.GetChild(7).gameObject;
        GameObject Block = levelPop.transform.GetChild(8).gameObject;

        Strength.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = damage.ToString();
        Defense.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = defense.ToString();
        Crit.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = critical.ToString();
        Intelligence.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = intelligence.ToString();
        Evade.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = evade.ToString();
        Block.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = blockStat.ToString();

        Strength.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(StrengthListener);
        Defense.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(DefenseListener);
        Crit.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(CritListener);
        Intelligence.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(IntelligenceListener);
        Evade.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(EvadeListener);
        Block.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(BlockListener);
        
        levelPop.transform.GetChild(9).gameObject.GetComponent<Button>().onClick.AddListener(LevelConfirmListener);
    }

    void StrengthListener(){
        ResetLevelStats();
        levelPopHolder.transform.GetChild(3).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (damage+1).ToString();
        levelPopHolder.transform.GetChild(3).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.green;
        levelStat = "strength";
    }
    void DefenseListener(){
        ResetLevelStats();
        levelPopHolder.transform.GetChild(4).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (defense+1).ToString();
        levelPopHolder.transform.GetChild(4).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.green;
        levelStat = "defense";
    }
    void CritListener(){
        Debug.Log("CRIT");
        ResetLevelStats();
        levelPopHolder.transform.GetChild(5).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (critical+1).ToString();
        levelPopHolder.transform.GetChild(5).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.green;
        levelStat = "crit";
    }
    void IntelligenceListener(){
        ResetLevelStats();
        levelPopHolder.transform.GetChild(6).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (intelligence+1).ToString();
        levelPopHolder.transform.GetChild(6).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.green;
        levelStat = "intelligence";
    }
    void EvadeListener(){
        ResetLevelStats();
        levelPopHolder.transform.GetChild(7).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (evade+1).ToString();
        levelPopHolder.transform.GetChild(7).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.green;
        levelStat = "evade";
    }
    void BlockListener(){
        ResetLevelStats();
        levelPopHolder.transform.GetChild(8).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (blockStat+1).ToString();
        levelPopHolder.transform.GetChild(8).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.green;
        levelStat = "block";
    }

    void ResetLevelStats(){
        levelPopHolder.transform.GetChild(3).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (damage).ToString();
        levelPopHolder.transform.GetChild(3).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.white;

        levelPopHolder.transform.GetChild(4).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (defense).ToString();
        levelPopHolder.transform.GetChild(4).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.white;

        levelPopHolder.transform.GetChild(5).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (critical).ToString();
        levelPopHolder.transform.GetChild(5).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.white;

        levelPopHolder.transform.GetChild(6).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (intelligence).ToString();
        levelPopHolder.transform.GetChild(6).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.white;

        levelPopHolder.transform.GetChild(7).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (evade).ToString();
        levelPopHolder.transform.GetChild(7).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.white;

        levelPopHolder.transform.GetChild(8).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().text = (blockStat).ToString();
        levelPopHolder.transform.GetChild(8).gameObject
            .transform.GetChild(1).gameObject
            .GetComponent<TMP_Text>().color = Color.white;
    }
    void LevelConfirmListener(){
        if(levelStat.Equals("strength")){
            damage++;
        }else if(levelStat.Equals("defense")){
            defense++;
        }else if(levelStat.Equals("crit")){
            critical++;
        }else if(levelStat.Equals("intelligence")){
            intelligence++;
        }else if(levelStat.Equals("evade")){
            evade++;
        }else if(levelStat.Equals("block")){
            blockStat++;
        }
        GameObject.Destroy(levelPopHolder);
        openLevel = false;
    }
    

    void updateUI()
    {
        if(openCharacter){
            charMenu.Update();
        }
        

        if (hp < 0)
            hp = 0;
        if (hp > maxHp)
            hp = maxHp;

        if (mp < 0)
            mp = 0;
        if (mp > maxMp)
            mp = maxMp;

        hpbar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)hp / (float)maxHp * 367);
        mpbar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)mp / (float)maxMp * 367);
    }

    public int getStrength(){
        return damage;
    }
    public int getDefense(){
        return defense;
    }
    public int getIntelligence(){
        return intelligence;
    }
    public int getCritical(){
        return critical;
    }
    public int getEvade(){
        return evade;
    }
    public int getBlock(){
        return blockStat;
    }
    public int getGold(){
        return gold;
    }
    public int getExpLevel(){
        return expLevel;
    }
    public int getExp(){
        return exp;
    }
    public int getMaxExp(){
        return maxExp;
    }
    public PlayerGear getGear(){
        return gear;
    }
    public void setGear(PlayerGear gear){
        this.gear = gear;
    }
    public Equipment setHelmet(Equipment i){
        Equipment equiped = gear.Helmet;
        gear.Helmet = i;
        return equiped;
    }
}