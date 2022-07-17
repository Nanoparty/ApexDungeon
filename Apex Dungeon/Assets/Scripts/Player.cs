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
    public GameObject pausePanel;
    public GameObject slot;
    public GameObject block;
    public GameObject pblock;
    public GameObject mapHolder;
    public GameObject questLine;
    public GameObject mapArea;
    public GameObject itemPopup;
    public GameObject levelPopup;
    public GameObject endingScreen;
    public Sprite[] frames;

    private CharacterMenu charMenu;
    private GameObject pauseMenu;
    private GameObject levelPopHolder;
    private PlayerGear gear;
    private Animator animator;
    private string playerName;
    private int gold;
    private bool openCharacter = false;
    private bool openLevel = false;
    private bool openPause = false;
    private bool opening = true;
    public bool ending = false;
    private bool fadeOut = false;
    public bool fadeIn = true;
    private float start;
    private int prevLevel;
    private string levelStat;
    private int levelPoints;
    private GameObject endScreenHolder;
    private bool interrupt = false;
    private bool attacking = false;

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

    void calculateAttack()
    {
        attack = attack + (int)(attack * (strength * 0.02));
    }

    void setInitialValues(){
        playerName = Data.activeCharacter ?? "bob";
        hp = 90;
        mp = 100;
        maxMp = 50;
        maxHp = 100;

        expLevel = 1;
        exp = 0;
        maxExp = 100;
        levelPoints = 0;

        attack = 10;
        strength = 10;
        defense = 10;
        intelligence = 10;
        critical = 10;
        evade = 10;
        blockStat = 10;

        type = 1;
        gold = 0;
        start = 0;
    }

    void initializeObjects(){
        SoundManager.sm.PlayDungeonMusic();

        animator = transform.GetChild(1).gameObject.transform.GetComponent<Animator>();
        gear = new PlayerGear();
        charMenu = new CharacterMenu(characterPanel, slot, questLine, mapArea, block, pblock, itemPopup, frames);

        hpbar = GameObject.FindGameObjectWithTag("hpbar");
        xpbar = GameObject.FindGameObjectWithTag("xpbar");
        character = GameObject.FindGameObjectWithTag("characterButton").GetComponent<Button>();
        pause = GameObject.FindGameObjectWithTag("PauseButton").GetComponent<Button>();

        character.onClick.AddListener(characterListener);
        pause.onClick.AddListener(pauseListener);
    }

    void moveController(int clickRow, int clickCol)
    {
        bool val;
        val = AttemptMove<Player>(clickRow, clickCol);
        GameManager.gmInstance.playersTurn = false;
        Debug.Log("Move controller end turn");
    }

    public void saveScores()
    {
        endScreenHolder.transform.GetChild(3).gameObject.SetActive(true);
        endScreenHolder.transform.GetChild(4).gameObject.SetActive(true);
        GameManager.gmInstance.scores = Data.scores ?? new List<(string, int)>();
        GameManager.gmInstance.scores.Add((GameManager.gmInstance.playerName, GameManager.gmInstance.score));
        GameManager.gmInstance.state = "score";
        Data.inProgress = false;
        Data.RemoveActive();
        //SceneManager.LoadScene("Scores", LoadSceneMode.Single);
    }

    private bool checkDead()
    {
        if (dead)
        {
            if(fadeIn){
                //calculate and save score
                GameManager.gmInstance.score += gold;
                fadeIn = false;

                //reset character stats

                //death screen
                GameObject op = GameObject.Instantiate(endingScreen, new Vector3(0, 0, 0), Quaternion.identity);
                op.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Score: " + GameManager.gmInstance.score.ToString();
                op.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Floor " + GameManager.gmInstance.level;
                op.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                op.transform.GetChild(3).gameObject.SetActive(false);
                op.transform.GetChild(4).gameObject.SetActive(false);
                endScreenHolder = op;
            }
            else if(ending){
                endScreenHolder.transform.GetChild(3).gameObject.SetActive(true);
                if (Input.GetButtonDown("Fire1"))
                {
                    GameManager.gmInstance.scores = Data.scores ?? new List<(string, int)>();
                    GameManager.gmInstance.scores.Add((GameManager.gmInstance.playerName, GameManager.gmInstance.score));
                    GameManager.gmInstance.state = "score";
                    Data.inProgress = false;
                    Data.RemoveActive();
                    SceneManager.LoadScene("Scores", LoadSceneMode.Single);
                }
            }
            

            return true;
        }
        return false;
    }

    void setFadeIn()
    {

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

        if (moving && Input.GetButtonDown("Fire1")){
            interrupt = true;
        }

        if(checkDead()) return;

        if (openCharacter)
        {
            //Debug.Log("charmenu UPdate");
            charMenu.Update();
            if (charMenu.getClosed())
            {
                charMenu.setClosed(false);
                openCharacter = false;
            }
            return;
        }

        if(openLevel || openPause) return;
    
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
            //Debug.Log("WORLD POS:" + finalScale.x);
            float lscale = hpbar.transform.GetComponent<RectTransform>().localScale.x;
            //Debug.Log("LOCAL SCALE=" + lscale);
            float fullWidth = finalScale.x * 3;

            float scale = redbar.transform.GetComponent<RectTransform>().localScale.x;
            redbar.transform.GetComponent<RectTransform>().position = new Vector3((pos.x - fullWidth/2), pos.y, pos.z);
            //Debug.Log("New Pos=" + redbar.transform.GetComponent<RectTransform>().position.x);
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

        Data.charMenu = charMenu;
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

        charMenu = Data.charMenu;
        gear = Data.gear;
    }

    public void nextFloor(){
        GameManager.gmInstance.Dungeon.setFullBright(false);
        GameManager.gmInstance.level++;
        saveCharacterData();
        GameManager.gmInstance.Reset();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SoundManager.sm.PlayPickupSound();
        if(other.gameObject.tag == "Stairs")
        {
            nextFloor();
        }
        if(other.gameObject.tag == "Consumable")
        {
            charMenu.addItem(other.GetComponent<Pickup>().GetItem());
            Destroy(other.gameObject);
            GameManager.gmInstance.Dungeon.removeFromItemList(row, col);
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
            charMenu.addEquipment(other.GetComponent<Pickup>().GetItem());
            Destroy(other.gameObject);
            GameManager.gmInstance.Dungeon.removeFromItemList(row, col);
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
        if (!openCharacter && !openPause)
        {
            charMenu.setClosed(false);
            SoundManager.sm.PlayMenuSound();
            charMenu.openStats();
            openCharacter = true;
        }
    }

    void pauseListener()
    {
        if (!openCharacter && !openPause)
        {
            SoundManager.sm.PlayMenuSound();
            openPause = true;
            GameObject parent = GameObject.FindGameObjectWithTag("Pause");
            pauseMenu = GameObject.Instantiate(pausePanel, Vector3.zero, Quaternion.identity);
            pauseMenu.transform.SetParent(parent.transform, false);

            GameObject buttons = pauseMenu.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
            GameObject options = pauseMenu.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
            GameObject music = options.transform.GetChild(0).gameObject;
            GameObject sound = options.transform.GetChild(1).gameObject;

            buttons.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ResumeListener);
            buttons.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(MenuListener);

            Toggle musicToggle = music.transform.GetChild(1).gameObject.GetComponent<Toggle>();
            Toggle soundToggle = sound.transform.GetChild(1).gameObject.GetComponent<Toggle>();
            Slider musicSlider = music.transform.GetChild(2).gameObject.GetComponent<Slider>();
            Slider soundSlider = sound.transform.GetChild(2).gameObject.GetComponent<Slider>();

            musicToggle.isOn = Data.music;
            soundToggle.isOn = Data.sound;
            musicSlider.value = Data.musicVolume;
            soundSlider.value = Data.soundVolume;

            musicToggle.onValueChanged.AddListener((value) => { musicToggleListener(value); });
            soundToggle.onValueChanged.AddListener((value) => { soundToggleListener(value); });
            musicSlider.onValueChanged.AddListener((value) => { musicSliderListener(value); });
            soundSlider.onValueChanged.AddListener((value) => { soundSliderListener(value); });

        }
    }

    void musicToggleListener(bool value)
    {
        Data.music = value;
        SoundManager.sm.UpdatePlaying();
        SoundManager.sm.PlayMenuSound();
    }

    void musicSliderListener(float value)
    {
        Data.musicVolume = value;
        SoundManager.sm.UpdateVolume();
    }

    void soundToggleListener(bool value)
    {
        SoundManager.sm.PlayMenuSound();
        Data.sound = value;
    }

    void soundSliderListener(float value)
    {
        Data.soundVolume = value;
    }

    void checkMoving()
    {
        if (moving)
        {
            if (atTarget && !interrupt)
            {
                setNextTarget();
                GameManager.gmInstance.playersTurn = false;
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
                animator.Play("AttackLeft");
                setAttackAnimation(clickRow, clickCol);
                attacking = true;
                enemy.takeDamage(calculateDamage());
                //GameManager.gmInstance.playersTurn = false;
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
        //GameObject damageNum = GameObject.Instantiate(damageText, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
        //damageNum.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"{d}";
        Debug.Log("Player take damage");
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
            return true;
        }
        return false;
    }

    protected override void OnCantMove<T>(T Component)
    {
        //INTERACTION
    }

    void ResumeListener(){
        SoundManager.sm.PlayMenuSound();
        openPause = false;
        GameObject.Destroy(pauseMenu);
    }

    void MenuListener(){
        SoundManager.sm.PlayMenuSound();
        saveCharacterData();
        Destroy(GameManager.gmInstance);
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    

    void createLevelPopup(){
        Vector3 pos = new Vector3(0, 0, 0);

        GameObject levelPop = GameObject.Instantiate(levelPopup, pos, Quaternion.identity);
        levelPop.transform.SetParent(GameObject.FindGameObjectWithTag("LevelUp").transform, false);
        levelPopHolder = levelPop;

        Util.setText(levelPop, prevLevel + "->" + expLevel, 1);

        GameObject Strength = Util.getChild(levelPop, 3);
        GameObject Defense = Util.getChild(levelPop, 4);
        GameObject Crit = Util.getChild(levelPop, 5);
        //GameObject Intelligence = Util.getChild(levelPop, 6);
        GameObject Evade = Util.getChild(levelPop, 6);
        //GameObject Block = Util.getChild(levelPop, 8);

        Util.setText(Strength, strength.ToString(), 1);
        Util.setText(Defense, defense.ToString(), 1);
        Util.setText(Crit, critical.ToString(), 1);
        //Util.setText(Intelligence, intelligence.ToString(), 1);
        Util.setText(Evade, evade.ToString(), 1);
        //Util.setText(Block, blockStat.ToString(), 1);

        Util.setListener(Strength, StrengthListener, 2);
        Util.setListener(Defense, DefenseListener, 2);
        Util.setListener(Crit, CritListener, 2);
        //Util.setListener(Intelligence, IntelligenceListener, 2);
        Util.setListener(Evade, EvadeListener, 2);
        //Util.setListener(Block, BlockListener, 2);

        Util.setListener(levelPop, LevelConfirmListener, 7);
    }

    void StrengthListener(){
        SoundManager.sm.PlayMenuSound();
        ResetLevelStats();
        Util.setText(levelPopHolder, (strength+1).ToString(), 3, 1);
        Util.setColor(levelPopHolder, Color.green, 3, 1);
        levelStat = "strength";
    }
    void DefenseListener(){
        SoundManager.sm.PlayMenuSound();
        ResetLevelStats();
        Util.setText(levelPopHolder, (defense+1).ToString(), 4, 1);
        Util.setColor(levelPopHolder, Color.green, 4, 1);
        levelStat = "defense";
    }
    void CritListener(){
        SoundManager.sm.PlayMenuSound();
        ResetLevelStats();
        Util.setText(levelPopHolder, (critical+1).ToString(), 5, 1);
        Util.setColor(levelPopHolder, Color.green, 5, 1);
        levelStat = "crit";
    }
    // void IntelligenceListener(){
    //     ResetLevelStats();
    //     Util.setText(levelPopHolder, (intelligence+1).ToString(), 6, 1);
    //     Util.setColor(levelPopHolder, Color.green, 6, 1);
    //     levelStat = "intelligence";
    // }
    void EvadeListener(){
        SoundManager.sm.PlayMenuSound();
        ResetLevelStats();
        Util.setText(levelPopHolder, (evade+1).ToString(), 6, 1);
        Util.setColor(levelPopHolder, Color.green, 6, 1);
        levelStat = "evade";
    }
    // void BlockListener(){
    //     ResetLevelStats();
    //     Util.setText(levelPopHolder, (blockStat+1).ToString(), 8, 1);
    //     Util.setColor(levelPopHolder, Color.green, 8, 1);
    //     levelStat = "block";
    // }

    void ResetLevelStats(){
        Util.setText(levelPopHolder, (strength).ToString(), 3, 1);
        Util.setColor(levelPopHolder, Color.white, 3, 1);

        Util.setText(levelPopHolder, (defense).ToString(), 4, 1);
        Util.setColor(levelPopHolder, Color.white, 4, 1);

        Util.setText(levelPopHolder, (critical).ToString(), 5, 1);
        Util.setColor(levelPopHolder, Color.white, 5, 1);

        // Util.setText(levelPopHolder, (intelligence).ToString(), 6, 1);
        // Util.setColor(levelPopHolder, Color.white, 6, 1);

        Util.setText(levelPopHolder, (evade).ToString(), 6, 1);
        Util.setColor(levelPopHolder, Color.white, 6, 1);

        // Util.setText(levelPopHolder, (blockStat).ToString(), 8, 1);
        // Util.setColor(levelPopHolder, Color.white, 8, 1);
    }
    void LevelConfirmListener(){
        SoundManager.sm.PlayMenuSound();
        if(levelStat.Equals("strength")){
            strength++;
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
        //Debug.Log($"{redStartingPos} - {redWidth} - {(float)hp / (float)maxHp} * {redWidth} = {redPos}");
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
    public void addExp(int i){    //////TODO -- fix so that you can get multiple points if level multiple times at once
        exp += i;
        GameManager.gmInstance.score += i;
        bool levelUp = false;
        prevLevel = expLevel;
        while(exp >= maxExp){
            exp -= maxExp;
            expLevel++;
            maxHp = (int)(maxHp * 1.1);
            hp = maxHp;
            maxExp += (int)(0.5 * maxExp);
            levelUp = true;
            levelPoints++;
        }
        if(levelUp){
            openLevel = true;
            createLevelPopup();
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