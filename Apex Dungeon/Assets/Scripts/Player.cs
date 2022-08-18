using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MovingEntity
{
    private GameObject hpbar;
    private GameObject xpbar;
    private Button character;
    private Button pause;
    public GameObject characterPanel;
    public GameObject slot;
    public GameObject block;
    public GameObject pblock;
    public GameObject mapArea;
    public GameObject itemPopup;
    public GameObject endingScreen;
    public Sprite[] frames;
    public Sprite[] tabs;
    public GameObject goldText;
    public LevelUp levelUp;
    public Pause pauseMenu;

    private CharacterMenu charMenu;
    private PlayerGear gear;
    private Animator animator;
    private GameObject stairsModal;
    private string playerName;
    private int gold;
    private bool openCharacter = false;
    public bool openLevel = false;
    public bool openPause = false;
    public bool ending = false;
    public bool fadeIn = true;
    private GameObject endScreenHolder;
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
        hp = 100;
        mp = 100;
        maxMp = 50;
        maxHp = 100;
        expLevel = 1;
        exp = 0;
        maxExp = 100;
        attack = 10;
        strength = 10;
        defense = 10;
        intelligence = 10;
        critical = 10;
        evade = 10;
        blockStat = 10;
        type = 1;
        gold = 0;
    }

    void initializeObjects() {
        SoundManager.sm.PlayDungeonMusic();

        animator = transform.GetChild(1).gameObject.transform.GetComponent<Animator>();
        gear = new PlayerGear();
        charMenu = new CharacterMenu(characterPanel, slot, mapArea, block, pblock, itemPopup, frames, tabs);

        hpbar = GameObject.FindGameObjectWithTag("hpbar");
        xpbar = GameObject.FindGameObjectWithTag("xpbar");
        character = GameObject.FindGameObjectWithTag("characterButton").GetComponent<Button>();
        pause = GameObject.FindGameObjectWithTag("PauseButton").GetComponent<Button>();

        stairsModal = GameObject.FindGameObjectWithTag("stairspopup");
        stairsModal.SetActive(false);
        stairsModal.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(stairsNo);
        stairsModal.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(stairsYes);

        character.onClick.AddListener(characterListener);
        pause.onClick.AddListener(pauseListener);
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
        if (!GameManager.gmInstance.playersTurn) return;

        updatePlayerStatus();

        GameManager.gmInstance.Dungeon.UpdateShadows(row, col);

        if (attacking)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDown")) {
                attacking = false;
                GameManager.gmInstance.playersTurn = false;
            }
            return;
        }

        if (moving && Input.GetButtonDown("Fire1")) {
            interrupt = true;
        }

        if (checkDead()) return;

        if (openCharacter)
        {
            charMenu.Update();
            if (charMenu.getClosed())
            {
                charMenu.setClosed(false);
                openCharacter = false;
            }
            return;
        }

        if (openLevel || openPause || stairsOpen) return;
    
        debugMenu();
        
        base.Update();
        checkMoving();
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (EventSystem.current.IsPointerOverGameObject())
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
                    return;
                }
                else
                {
                    if (attackController(clickRow, clickCol))
                        return;
                }
            }

            //move
            moveController(clickRow, clickCol);
        }
    }

    void debugMenu(){
        //Debugging tool
        if (Input.GetKeyDown("q"))
        {
            GameObject redbar = hpbar.transform.GetChild(0).gameObject;
            Vector3 pos = redbar.transform.GetComponent<RectTransform>().position;

            Vector2 sizeDelta = redbar.transform.GetComponent<RectTransform>().sizeDelta;
            Canvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
            Vector2 canvasScale = new Vector2(canvas.transform.localScale.x, canvas.transform.localScale.y);
            Vector2 finalScale = new Vector2(sizeDelta.x * canvasScale.x, sizeDelta.y * canvasScale.y);
            float lscale = hpbar.transform.GetComponent<RectTransform>().localScale.x;
            float fullWidth = finalScale.x * 3;

            float scale = redbar.transform.GetComponent<RectTransform>().localScale.x;
            redbar.transform.GetComponent<RectTransform>().position = new Vector3((pos.x - fullWidth/2), pos.y, pos.z);
        }
        if (Input.GetKeyDown("t"))
        {
            nextFloor();
        }
        if (Input.GetKeyDown("r"))
        {
            GameManager.gmInstance.FullReset();
        }
        if(Input.GetKeyDown("y")){
            GameManager.gmInstance.scores = Data.scores ?? new List<(string, int)>();
            GameManager.gmInstance.scores.Add((GameManager.gmInstance.playerName,GameManager.gmInstance.score));
            GameManager.gmInstance.state = "score";
            Data.inProgress = false;
            SceneManager.LoadScene("Scores", LoadSceneMode.Single);
        }
    }

    public void saveCharacterData(){
        Data.playerName = playerName;
        Data.hp = hp;
        Data.maxHp = maxHp;
        Data.mp = mp;
        Data.maxMp = maxMp;
        Data.exp = exp;
        Data.maxExp = maxExp;
        Data.expLevel = expLevel;
        Data.strength = strength;
        Data.defense = defense;
        Data.intelligence = intelligence;
        Data.crit = critical;
        Data.evade = evade;
        Data.block = blockStat;
        Data.gold = gold;
        Data.floor = GameManager.gmInstance.level;

        Data.equipment = charMenu.equipment;
        Data.consumables = charMenu.items;
        Data.gear = gear;
        Data.SaveCharacter();
    }

    public void loadCharacterData(){
        
        playerName = Data.playerName;
        hp = Data.hp;
        maxHp = Data.maxHp;
        mp = Data.mp;
        maxMp = Data.maxMp;
        exp = Data.exp;
        maxExp = Data.maxExp;
        expLevel = Data.expLevel;
        strength = Data.strength;
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
            if (charMenu.items.Count == charMenu.maxSlots) return;
            charMenu.addItem(other.GetComponent<Pickup>().GetItem());
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
            Destroy(other.gameObject);

        }
        if (other.gameObject.tag == "Equipment")
        {
            if (charMenu.equipment.Count == charMenu.maxSlots) return;
            charMenu.addEquipment(other.GetComponent<Pickup>().GetItem());
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

    void characterListener()
    {
        if (!openCharacter && !openPause)
        {
            charMenu.setClosed(false);
            SoundManager.sm.PlayBookOpen();
            charMenu.openStats();
            openCharacter = true;
        }
    }

    void pauseListener()
    {
        if (!openCharacter && !openPause)
        {
            pauseMenu.CreatePause(this);
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

        hpbar.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = hp + "/" + maxHp;
        xpbar.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = exp + "/" + maxExp;

        GameObject redbar = hpbar.transform.GetChild(0).gameObject;
        GameObject greenbar = xpbar.transform.GetChild(0).gameObject;

        Canvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        Vector2 canvasScale = new Vector2(canvas.transform.localScale.x, canvas.transform.localScale.y);

        Vector2 redSizeDelta = redbar.transform.GetComponent<RectTransform>().sizeDelta;
        Vector2 redFinalScale = new Vector2(redSizeDelta.x * canvasScale.x, redSizeDelta.y * canvasScale.y);
        float redWidth = redFinalScale.x * hpbar.transform.GetComponent<RectTransform>().localScale.x;

        Vector3 pos1 = redbar.transform.position;
        float redStartingPos = hpbar.transform.GetChild(2).gameObject.transform.position.x;
        float redPos = redStartingPos - (redWidth - (((float)hp / (float)maxHp) * redWidth));
        redbar.transform.GetComponent<RectTransform>().position = new Vector3(redPos, pos1.y, pos1.z);

        Vector2 greenSizeDelta = greenbar.transform.GetComponent<RectTransform>().sizeDelta;
        Vector2 greenFinalScale = new Vector2(greenSizeDelta.x * canvasScale.x, greenSizeDelta.y * canvasScale.y);
        float greenWidth = greenFinalScale.x * xpbar.transform.GetComponent<RectTransform>().localScale.x;

        Vector3 pos2 = greenbar.transform.position;
        float greenStartingPos = xpbar.transform.GetChild(2).gameObject.transform.position.x;
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
    public void addTotalHP(int i)
    {
        maxHp += i;
        hp += i;
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
            maxHp = (int)(maxHp * 1.1);
            hp = maxHp;
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
        attack += i;
    }
    public void addDefense(int i){
        defense += i;
    }
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
    }
    public void setCritical(int i)
    {
        critical = i;
    }
    public void setEvasion(int i)
    {
        evade = i;
    }
    public void closeInventory()
    {
        charMenu.closeInventory();
        openCharacter = false;
    }
    public string getName()
    {
        return playerName;
    }
}