﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MovingEntity
{
    public GameObject endingScreen;
    public GameObject goldText;
    public LevelUp levelUp;
    public Pause pauseMenu;
    public Journal journal;

    private GameObject hpBar;
    private GameObject expBar;

    private GameObject journalButton;
    private GameObject pauseButton;

    private PlayerGear gear;
    private Animator animator;
    private GameObject stairsModal;
    private GameObject endScreenHolder;

    private string playerName;
    private int gold;
    public bool openJournal = false;
    public bool openLevel = false;
    public bool openPause = false;
    public bool ending = false;
    public bool fadeIn = true;
    private bool interrupt = false;
    private bool attacking = false;

    private bool stairsOpen = false;

    protected override void Start()
    {
        setInitialValues();
        initializeObjects();
        if (GameManager.gmInstance.level > 1 || Data.loadData)
        {
            loadCharacterData();
        }

        base.Start();
    }

    void setInitialValues() {
        playerName = Data.activeCharacter ?? "bob";
        
        mp = 100;
        maxMp = 50;
        expLevel = 1;
        exp = 0;
        maxExp = 100;
        attack = 10;
        strength = 10;
        attack = 10;
        defense = 10;
        intelligence = 10;
        critical = 10;
        evade = 10;
        blockStat = 10;
        type = 1;
        gold = 0;

        baseHp = 100;
        hp = baseHp + (int)((float)baseHp * 0.02f * defense);
        maxHp = hp;
    }

    void initializeObjects() {
        SoundManager.sm.PlayDungeonMusic();

        animator = transform.GetChild(1).gameObject.transform.GetComponent<Animator>();
        gear = new PlayerGear();

        hpBar = GameObject.FindGameObjectWithTag("hpbar");
        expBar = GameObject.FindGameObjectWithTag("xpbar");

        journalButton = GameObject.FindGameObjectWithTag("characterButton");
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton");
        journalButton.GetComponent<Button>().onClick.AddListener(journalListener);
        pauseButton.GetComponent<Button>().onClick.AddListener(pauseListener);

        stairsModal = GameObject.FindGameObjectWithTag("stairspopup");
        stairsModal.SetActive(false);
        stairsModal.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(stairsNo);
        stairsModal.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(stairsYes);
    }

    void moveController(int clickRow, int clickCol)
    {
        bool val;
        val = AttemptMove<Player>(clickRow, clickCol);
        GameManager.gmInstance.playersTurn = false;
    }

    void stairsYes()
    {
        nextFloor();
    }

    void stairsNo()
    {
        stairsModal.SetActive(false);
        stairsOpen = false;
    }

    public void saveScores()
    {
        endScreenHolder.transform.GetChild(3).gameObject.SetActive(true);
        endScreenHolder.transform.GetChild(4).gameObject.SetActive(true);
        Data.inProgress = false;
        Data.RemoveActive();
    }

    private bool checkDead()
    {
        if (dead)
        {
            
            if (fadeIn) {
                SoundManager.sm.PlayDeathSound();
                //calculate and save score
                GameManager.gmInstance.score += gold;
                List<(string, int)> scores = Data.scores ?? new List<(string, int)>();
                scores.Add((GameManager.gmInstance.playerName, GameManager.gmInstance.score));
                Data.scores = scores;
                fadeIn = false;

                //death screen
                GameObject op = GameObject.Instantiate(endingScreen, new Vector3(0, 0, 0), Quaternion.identity);
                op.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Score: " + GameManager.gmInstance.score.ToString();
                op.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Floor " + GameManager.gmInstance.level;
                op.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                op.transform.GetChild(3).gameObject.SetActive(false);
                op.transform.GetChild(4).gameObject.SetActive(false);
                endScreenHolder = op;
            }
            
            return true;
        }
        return false;
    }

    protected override void Update()
    {
        if (!GameManager.gmInstance.playersTurn)
        {
            return;
        }

        updatePlayerStatus();

        GameManager.gmInstance.Dungeon.UpdateShadows(row, col);

        if (attacking)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDown")) {
                attacking = false;
                GameManager.gmInstance.playersTurn = false;
                GameManager.gmInstance.UpdateCursor("Done");
            }
            return;
        }

        if (moving && Input.GetButtonDown("Fire1")) {
            interrupt = true;
            GameManager.gmInstance.UpdateCursor("Interrupt");
        }

        if (checkDead()) return;

        if (openJournal || journal.isOpen())
        {
            journal.Update();
            return;
        }

        if (openLevel || openPause || stairsOpen) return;
    
        debugMenu();
        
        base.Update();
        checkMoving();
        
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Detect Click");
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log("UI");
                return;
            }

            //check for UI Button
            if (openJournal || openPause ||
                journalButton.GetComponent<Clickable>().getClicked() ||
                pauseButton.GetComponent<Clickable>().getClicked())
            {
                return;
            }

            int clickRow = (int)GameManager.gmInstance.mRow;
            int clickCol = (int)GameManager.gmInstance.mCol;

            //attack or interact
            if (isAdjacent(clickRow, clickCol))
            {
                if(isFurniture(clickRow, clickCol))
                {
                    GameManager.gmInstance.UpdateCursor("Furniture", clickRow, clickCol);
                    return;
                }
                else
                {
                    if (attackController(clickRow, clickCol))
                    {
                        GameManager.gmInstance.UpdateCursor("Attack", clickRow, clickCol);
                        return;
                    }
                }
            }

            //move
            //if is player skip turn
            if (isPlayer(clickRow, clickCol))
            {
                GameManager.gmInstance.UpdateCursor("Player", clickRow, clickCol);
                moveController(clickRow, clickCol);
                return;
            }
            //if valid location move
            if (!isBlocked(clickRow, clickCol))
            {
                GameManager.gmInstance.UpdateCursor("Move", clickRow, clickCol);
                moveController(clickRow, clickCol);
                return;
            }
            else
            {
                GameManager.gmInstance.UpdateCursor("Blocked", clickRow, clickCol);
                return;
            }
        }
    }

    void debugMenu(){
        //Debugging tool
        if (Input.GetKeyDown("t"))
        {
            nextFloor();
        }
    }

    public bool isBlocked(int r, int c)
    {
        return GameManager.gmInstance.Dungeon.tileMap[r, c].getWall();
    }

    public void saveCharacterData(){
        Data.playerName = playerName;
        Data.baseHp = baseHp;
        Data.hp = hp;
        Data.maxHp = maxHp;
        Data.mp = mp;
        Data.maxMp = maxMp;
        Data.exp = exp;
        Data.maxExp = maxExp;
        Data.expLevel = expLevel;
        Data.strength = strength;
        Data.attack = attack;
        Data.defense = defense;
        Data.intelligence = intelligence;
        Data.crit = critical;
        Data.evade = evade;
        Data.block = blockStat;
        Data.gold = gold;
        Data.floor = GameManager.gmInstance.level;
        Data.equipment = journal.getEquipment();
        Data.consumables = journal.getItems();
        Data.gear = gear;
        Data.SaveCharacter();
    }

    public void loadCharacterData(){
        playerName = Data.playerName;
        baseHp = Data.baseHp;
        hp = Data.hp;
        maxHp = Data.maxHp;
        mp = Data.mp;
        maxMp = Data.maxMp;
        exp = Data.exp;
        maxExp = Data.maxExp;
        expLevel = Data.expLevel;
        strength = Data.strength;
        attack = Data.attack;
        defense = Data.defense;
        intelligence = Data.intelligence;
        critical = Data.crit;
        evade = Data.evade;
        blockStat = Data.block;
        gold = Data.gold;
        gear = Data.gear;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            saveCharacterData();
            Data.SaveToFile();
        }
    }

    public void CloseJournal()
    {
        journal.closeJournal();
    }

    public void nextFloor(){
        stairsModal.SetActive(true);
        GameManager.gmInstance.Dungeon.setFullBright(false);
        GameManager.gmInstance.level++;
        saveCharacterData();
        Data.SaveToFile();
        GameManager.gmInstance.Reset();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Stairs")
        {
            SoundManager.sm.PlayPageTurn();
            stairsOpen = true;
            stairsModal.SetActive(true);
        }
        if(other.gameObject.tag == "Consumable")
        {
            if (journal.getItems().Count == Journal.maxSlots) return;
            journal.addItem(other.GetComponent<Pickup>().GetItem());
            Destroy(other.gameObject);
            GameManager.gmInstance.Dungeon.removeFromItemList(row, col);
            SoundManager.sm.PlayPickupSound();
        }
        if (other.gameObject.tag == "Gold" || other.gameObject.tag == "Silver" || other.gameObject.tag == "Copper")
        {
            int amount = other.GetComponent<Money>().amount;
            gold += amount;
            SoundManager.sm.PlayCoinSound();
            GameObject goldNum = GameObject.Instantiate(goldText, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity, this.transform);
            goldNum.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"+{amount}";
            GameManager.gmInstance.Dungeon.removeFromItemList(row, col);
            Destroy(other.gameObject);

        }
        if (other.gameObject.tag == "Equipment")
        {
            if (journal.getEquipment().Count == Journal.maxSlots) return;
            journal.addEquipment(other.GetComponent<Pickup>().GetItem());
            Destroy(other.gameObject);
            GameManager.gmInstance.Dungeon.removeFromItemList(row, col);
            SoundManager.sm.PlayPickupSound();
        }
    }

    bool isFurniture(int row, int col)
    {
        Furniture f = GameManager.gmInstance.getFurnitureAtLoc(row, col);
        if (f != null)
        {
            f.setDamage(-1);
            GameManager.gmInstance.playersTurn = false;
            SoundManager.sm.PlayStickSounds();
            return true;
        }
        else return false;
    }

    bool isPlayer(int nRow, int nCol)
    {
        return (nRow == row && nCol == col);
    }

    void journalListener()
    {
        if (!openJournal && !openPause)
        {
            SoundManager.sm.PlayBookOpen();
            journal.CreateJournal(this);
            openJournal = true;
            journalButton.GetComponent<Clickable>().setClicked(false);
        }
    }

    void pauseListener()
    {
        if (!openJournal && !openPause)
        {
            pauseMenu.CreatePause(this);
            openPause = true;
            pauseButton.GetComponent<Clickable>().setClicked(false);
        }
    }

    void checkMoving()
    {
        if (moving)
        {
            if (atTarget && !interrupt)
            {
                setNextTarget();
                GameManager.gmInstance.playersTurn = false;
                SoundManager.sm.PlayStepSound();
            }
            else if(atTarget){
                moving = false;
                interrupt = false;
            }

            return;
        }
    }

    bool attackController(int clickRow, int clickCol)
    {
        Debug.Log($"Attacking attack:{attack} strength:{strength}");
        Enemy enemy = GameManager.gmInstance.getEnemyAtLoc(clickRow, clickCol);
        if (enemy != null && attacking == false)
        {
            setAttackAnimation(clickRow, clickCol);
            attacking = true;
            int dice = Random.Range(0, 100);
            if (dice <= critical)
            {
                enemy.takeDamage(calculateDamage(3), true);
            }
            else
            {
                enemy.takeDamage(calculateDamage());
            }
                
            return true;
        }
        return false;
    }

    public void setAttackAnimation(int enemyRow, int enemyCol)
    {
        if (enemyRow > row) animator.Play("AttackUp");
        if (enemyRow < row) animator.Play("AttackDown");
        if (enemyCol > col) animator.Play("AttackRight");
        if (enemyCol < col) animator.Play("AttackLeft");
    }

    public void takeAttack(float d){
        SoundManager.sm.PlayHitSound();
        
        int dice = Random.Range(1, 101);
        if(dice <= evade)
        {
            GameObject evadeText = GameObject.Instantiate(damageText, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            evadeText.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"Evade";
            evadeText.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = new Color(50f / 255f, 205f / 255f, 50f / 255f);
            return;
        }
        base.takeDamage(d);
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
            SoundManager.sm.PlayStepSound();
            return true;
        }
        return false;
    }

    protected override void OnCantMove<T>(T Component)
    {
        //INTERACTION
    }

    void updatePlayerStatus()
    {
        if (hp < 0) hp = 0;
        if (hp > maxHp) hp = maxHp;

        if (mp < 0) mp = 0;
        if (mp > maxMp) mp = maxMp;

        hpBar.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = hp + "/" + maxHp;
        expBar.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = exp + "/" + maxExp;

        GameObject redbar = hpBar.transform.GetChild(0).gameObject;
        GameObject greenbar = expBar.transform.GetChild(0).gameObject;

        Canvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        Vector2 canvasScale = new Vector2(canvas.transform.localScale.x, canvas.transform.localScale.y);

        Vector2 redSizeDelta = redbar.transform.GetComponent<RectTransform>().sizeDelta;
        Vector2 redFinalScale = new Vector2(redSizeDelta.x * canvasScale.x, redSizeDelta.y * canvasScale.y);
        float redWidth = redFinalScale.x * hpBar.transform.GetComponent<RectTransform>().localScale.x;

        Vector3 pos1 = redbar.transform.position;
        float redStartingPos = hpBar.transform.GetChild(2).gameObject.transform.position.x;
        float redPos = redStartingPos - (redWidth - (((float)hp / (float)maxHp) * redWidth));
        redbar.transform.GetComponent<RectTransform>().position = new Vector3(redPos, pos1.y, pos1.z);

        Vector2 greenSizeDelta = greenbar.transform.GetComponent<RectTransform>().sizeDelta;
        Vector2 greenFinalScale = new Vector2(greenSizeDelta.x * canvasScale.x, greenSizeDelta.y * canvasScale.y);
        float greenWidth = greenFinalScale.x * expBar.transform.GetComponent<RectTransform>().localScale.x;

        Vector3 pos2 = greenbar.transform.position;
        float greenStartingPos = expBar.transform.GetChild(2).gameObject.transform.position.x;
        float greenPos = greenStartingPos + (((float)exp / (float)maxExp) * greenWidth);
        greenbar.transform.GetComponent<RectTransform>().position = new Vector3(greenPos, pos2.y, pos2.z);

    }

    public int getStrength(){
        return strength;
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

    public void addMP(int i)
    {
        mp += i;
        if(mp > maxMp) mp = maxMp;
    }
    public void addMaxMP(int i){
        maxMp += i;
    }
    public void addHP(int i)
    {
        hp += i;
        if(hp > maxHp) hp = maxHp;
        GameObject damageNum = GameObject.Instantiate(damageText, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity, this.transform);
        damageNum.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = new Color(50f / 255f, 205f / 255f, 50f / 255f);
        damageNum.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"+{i}";
    }
    public void addMaxHP(int i){
        maxHp += i;
        if(hp > maxHp)
        {
            hp = maxHp;
        }
    }
    public void addBaseHP(int i)
    {
        //Debug.Log($"Starting:{baseHp}:{maxHp}:{hp}");
        baseHp += i;
        int newHp = baseHp + (int)((float)baseHp * 0.02f * defense);
        int diff = newHp - maxHp;
        maxHp += diff;
        hp += diff;
        //Debug.Log($"new:{newHp} diff{diff}");
        //Debug.Log($"Ending:{baseHp}:{maxHp}:{hp}");
    }
    public void addExp(int i){
        exp += i;
        int levelPoints = 0;
        GameManager.gmInstance.score += i;
        bool didLevel = false;
        while(exp >= maxExp){
            SoundManager.sm.PlayLevelUpSound();
            exp -= maxExp;
            expLevel++;
            baseHp = (int)(baseHp * 1.1);
            hp = baseHp + (int)((float)baseHp * 0.02f * defense);
            maxHp = hp;
            maxExp += (int)(0.5 * maxExp);
            didLevel = true;
            levelPoints++;
        }
        if(didLevel)
        {
            openLevel = true;
            levelUp.CreatePopup(this, levelPoints);
        }
    }
    public void addStrength(int i){
        strength += i;
    }
    public void addAttack(int i)
    {
        Debug.Log($"Before Attack-- attack:{attack} strength:{strength}");
        attack += i;
        Debug.Log($"After Attack-- attack:{attack} strength:{strength}");
    }
    //public void addDefense(int i){
    //    defense += i;
    //    int newHp = baseHp + (int)((float)baseHp * Mathf.Pow(0.02f, defense));
    //    int diff = newHp - maxHp;
    //    maxHp += diff;
    //    hp += diff;
    //}
    public void addCrit(int i){
        critical += i;
    }
    public void addIntelligence(int i){
        intelligence += i;
    }
    public void addBlock(int i){
        blockStat += i;
    }
    public void addEvade(int i){
        evade += i;
    }
    public void setStrength(int i)
    {
        strength = i;
    }
    public void setDefense(int i)
    {
        defense = i;
        int newHp = baseHp + (int)((float)baseHp * 0.02f * defense);
        int diff = newHp - maxHp;
        maxHp += diff;
        hp += diff;

        
    }
    public void setCritical(int i)
    {
        critical = i;
    }
    public void setEvasion(int i)
    {
        evade = i;
    }
    public string getName()
    {
        return playerName;
    }
}