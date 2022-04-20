using UnityEngine;
using UnityEngine.EventSystems;
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
    public GameObject endingScreen;

    private CharacterMenu charMenu;
    private GameObject levelPopHolder;
    private PlayerGear gear;
    private Animator animator;
    private int gold;
    private bool openCharacter = false;
    private bool openLevel = false;
    private bool opening = true;
    public bool ending = false;
    private bool fadeOut = false;
    public bool fadeIn = true;
    private float start;
    private int prevLevel;
    private string levelStat;
    private GameObject endScreenHolder;
    protected override void Start()
    {
        setInitialValues();
        initializeObjects();
        
        if (GameManager.gmInstance.level > 1)
        {
            loadCharacterData();
        }

        base.Start();
    }

    void setInitialValues(){
        hp = 1;
        mp = 50;
        maxMp = 50;
        maxHp = 100;

        expLevel = 1;
        exp = 50;
        maxExp = 100;

        damage = 10;
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
        animator = GetComponent<Animator>();
        gear = new PlayerGear();
        charMenu = new CharacterMenu(characterPanel, slot, questLine, mapArea, block, pblock, itemPopup);

        hpbar = GameObject.FindGameObjectWithTag("hpbar").transform.GetChild(1).gameObject.GetComponent<Image>();
        mpbar = GameObject.FindGameObjectWithTag("mpbar").transform.GetChild(1).gameObject.GetComponent<Image>();
        character = GameObject.FindGameObjectWithTag("characterButton").GetComponent<Button>();

        character.onClick.AddListener(characterListener);
    }

    void moveController(int clickRow, int clickCol)
    {
        bool val;
        val = AttemptMove<Player>(clickRow, clickCol);
        GameManager.gmInstance.playersTurn = false;
    }

    private bool checkDead()
    {
        if (dead)
        {
            if(fadeIn){
                //Debug.Log("Starting fade in");
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
                endScreenHolder = op;
            }
            else if(ending){
                endScreenHolder.transform.GetChild(3).gameObject.SetActive(true);
                Debug.Log("ACTIVATE");
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

        if(checkDead()) return;

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

        if(openLevel) return;
    
        debugMenu();
        
        
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

    void debugMenu(){
        //Debugging tool
        if (Input.GetKeyDown("t"))
        {
            nextFloor();
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

    public void nextFloor(){
        GameManager.gmInstance.Dungeon.setFullBright(false);
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
        if(other.gameObject.tag == "Consumable")
        {
            charMenu.addItem(other.GetComponent<Pickup>().GetItem());
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
            charMenu.addEquipment(other.GetComponent<Pickup>().GetItem());
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
                enemy.takeDamage(calculateDamage());
                GameManager.gmInstance.playersTurn = false;
                return;
            }
        
    }

    public void takeAttack(float d){
        // float netDamage = d + (defense * 0.5f);
        // Debug.Log("PLAYER TAKE DAMANGE:"+netDamage);
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

    

    void createLevelPopup(){
        Vector3 pos = new Vector3(0, 0, 0);

        GameObject levelPop = GameObject.Instantiate(levelPopup, pos, Quaternion.identity);
        levelPop.transform.SetParent(GameObject.FindGameObjectWithTag("LevelUp").transform, false);
        levelPopHolder = levelPop;

        Util.setText(levelPop, prevLevel + "->" + expLevel, 1);

        GameObject Strength = Util.getChild(levelPop, 3);
        GameObject Defense = Util.getChild(levelPop, 4);
        GameObject Crit = Util.getChild(levelPop, 5);
        GameObject Intelligence = Util.getChild(levelPop, 6);
        GameObject Evade = Util.getChild(levelPop, 7);
        GameObject Block = Util.getChild(levelPop, 8);

        Util.setText(Strength, damage.ToString(), 1);
        Util.setText(Defense, defense.ToString(), 1);
        Util.setText(Crit, critical.ToString(), 1);
        Util.setText(Intelligence, intelligence.ToString(), 1);
        Util.setText(Evade, evade.ToString(), 1);
        Util.setText(Block, blockStat.ToString(), 1);

        Util.setListener(Strength, StrengthListener, 2);
        Util.setListener(Defense, DefenseListener, 2);
        Util.setListener(Crit, CritListener, 2);
        Util.setListener(Intelligence, IntelligenceListener, 2);
        Util.setListener(Evade, EvadeListener, 2);
        Util.setListener(Block, BlockListener, 2);

        Util.setListener(levelPop, LevelConfirmListener, 9);
    }

    void StrengthListener(){
        ResetLevelStats();
        Util.setText(levelPopHolder, (damage+1).ToString(), 3, 1);
        Util.setColor(levelPopHolder, Color.green, 3, 1);
        levelStat = "strength";
    }
    void DefenseListener(){
        ResetLevelStats();
        Util.setText(levelPopHolder, (defense+1).ToString(), 4, 1);
        Util.setColor(levelPopHolder, Color.green, 4, 1);
        levelStat = "defense";
    }
    void CritListener(){
        ResetLevelStats();
        Util.setText(levelPopHolder, (critical+1).ToString(), 5, 1);
        Util.setColor(levelPopHolder, Color.green, 5, 1);
        levelStat = "crit";
    }
    void IntelligenceListener(){
        ResetLevelStats();
        Util.setText(levelPopHolder, (intelligence+1).ToString(), 6, 1);
        Util.setColor(levelPopHolder, Color.green, 6, 1);
        levelStat = "intelligence";
    }
    void EvadeListener(){
        ResetLevelStats();
        Util.setText(levelPopHolder, (evade+1).ToString(), 7, 1);
        Util.setColor(levelPopHolder, Color.green, 7, 1);
        levelStat = "evade";
    }
    void BlockListener(){
        ResetLevelStats();
        Util.setText(levelPopHolder, (blockStat+1).ToString(), 8, 1);
        Util.setColor(levelPopHolder, Color.green, 8, 1);
        levelStat = "block";
    }

    void ResetLevelStats(){
        Util.setText(levelPopHolder, (damage).ToString(), 3, 1);
        Util.setColor(levelPopHolder, Color.white, 3, 1);

        Util.setText(levelPopHolder, (defense).ToString(), 4, 1);
        Util.setColor(levelPopHolder, Color.white, 4, 1);

        Util.setText(levelPopHolder, (critical).ToString(), 5, 1);
        Util.setColor(levelPopHolder, Color.white, 5, 1);

        Util.setText(levelPopHolder, (intelligence).ToString(), 6, 1);
        Util.setColor(levelPopHolder, Color.white, 6, 1);

        Util.setText(levelPopHolder, (evade).ToString(), 7, 1);
        Util.setColor(levelPopHolder, Color.white, 7, 1);

        Util.setText(levelPopHolder, (blockStat).ToString(), 8, 1);
        Util.setColor(levelPopHolder, Color.white, 8, 1);
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
    

    void updatePlayerStatus()
    {
        if (hp < 0) hp = 0;
        if (hp > maxHp) hp = maxHp;

        if (mp < 0) mp = 0;
        if (mp > maxMp) mp = maxMp;

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
    }
    public void addMaxHP(int i){
        maxHp += i;
    }
    public void addExp(int i){    //////TODO -- fix so that you can get multiple points if level multiple times at once
        exp += i;
        GameManager.gmInstance.score += i;
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
    public void addStrength(int i){
        damage += i;
        Debug.Log("ADDING "+i + " STRENGTH");
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
}