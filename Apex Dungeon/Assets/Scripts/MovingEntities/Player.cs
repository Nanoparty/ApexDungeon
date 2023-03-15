using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.U2D.Animation;
using static CharacterClass;
using static StatusEffect;

public class Player : MovingEntity
{
    [Header("Prefabs")]
    public GameObject endingScreen;
    public GameObject goldText;
    public LevelUp levelUp;
    public Pause pauseMenu;
    public Journal journal;

    [Header("UI Buttons")]
    public GameObject journalButton;
    public GameObject pauseButton;
    public GameObject mapButton;
    public GameObject skillsButton;

    [Header("Active Skill")]
    public Skill activeSkill;

    private GameObject hpBar;
    private GameObject mpBar;
    private GameObject expBar;

    [HideInInspector]
    public GameObject tileHighlight;
    public List<GameObject> targetTiles;

    private PlayerGear gear;
    private SpriteLibrary sl;
    private GameObject stairsModal;
    private GameObject endScreenHolder;

    private string playerName;
    private int gold;

    [Header("Flags")]
    public bool openJournal = false;
    public bool openLevel = false;
    public bool openPause = false;
    public bool ending = false;
    public bool fadeIn = true;
    public bool attacking = false;
    public bool stairsOpen = false;
    public bool targetMode = false;
    public bool drawTargets = false;
    private bool turnStart = true;


    [Header("Character Sprite Libraries")]
    [SerializeField] private SpriteLibraryAsset ArcherLibrary;
    [SerializeField] private SpriteLibraryAsset WarriorLibrary;
    [SerializeField] private SpriteLibraryAsset PaladinLibrary;
    [SerializeField] private SpriteLibraryAsset KnightLibrary;
    [SerializeField] private SpriteLibraryAsset MonkLibrary;
    [SerializeField] private SpriteLibraryAsset NecromancerLibrary;
    [SerializeField] private SpriteLibraryAsset DruidLibrary;
    [SerializeField] private SpriteLibraryAsset SwordsmanLibrary;
    [SerializeField] private SpriteLibraryAsset BardLibrary;
    [SerializeField] private SpriteLibraryAsset MageLibrary;
    [SerializeField] private SpriteLibraryAsset ThiefLibrary;
    [SerializeField] private SpriteLibraryAsset PriestLibrary;

    [Header("Status Effects")]
    [SerializeField] private GameObject StatusEffectAlert;

    protected override void Start()
    {
        base.Start();
        InitializeObjects();
        SetInitialValues();

        if (GameManager.gmInstance.level > 1 || Data.loadData || GameManager.gmInstance.gameStarted){
            LoadCharacterData();
        }

        GameManager.gmInstance.gameStarted = true;

        GameManager.gmInstance.Log.AddLog($">{entityName} enters dungeon level " + GameManager.gmInstance.level + ".");
    }

    protected override void Update()
    {
        if (!GameManager.gmInstance.playersTurn) { return; }

        if (turnStart) { PlayerStart(); }

        UpdatePlayerStatus();

        GameManager.gmInstance.Dungeon.UpdateShadows(row, col);

        if (attacking)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                attacking = false;
                PlayerEnd();
            }
            return;
        }

        if (CheckDead()) return;


        if (openJournal || journal.isOpen())
        {
            journal.Update();
            return;
        }

        if (openLevel || openPause || stairsOpen || GameManager.gmInstance.Log.isFullscreen) return;

        DebugMenu();

        if (targetMode)
        {
            if (activeSkill == null)
            {
                targetMode = false;
                return;
            }
            if (activeSkill.range == 0)
            {
                if (activeSkill.Activate(this, row, col))
                {
                    PlayerEnd();
                }

                targetMode = false;
                activeSkill = null;
                return;
            }

            DrawTargetHighlights();

            if (TargetSelection()) {
                return; 
            }

            return;
        }

        base.Update();

        if (moving)
        {
            // Look for interrupts
            if (Input.GetButtonDown("Fire1"))
            {
                int clickRow = (int)GameManager.gmInstance.mRow;
                int clickCol = (int)GameManager.gmInstance.mCol;

                if (IsPlayer(clickRow, clickCol))
                {
                    interrupt = true;
                }
                else if (!IsBlocked(clickRow, clickCol))
                {
                    if (!SetNewPath(clickRow, clickCol))
                    {
                        interrupt = true;
                    }
                }
                else
                {
                    interrupt = true;
                }
            }

            UpdatePathing();
        }
        if (!moving)
        {
            // Look for inputs
            if (Input.GetButtonDown("Fire1"))
            {
                int clickRow = (int)GameManager.gmInstance.mRow;
                int clickCol = (int)GameManager.gmInstance.mCol;

                // Check for UI click
                if (EventSystem.current.IsPointerOverGameObject()) { return; }

                //check for UI Button
                if (openJournal || openPause ||
                    journalButton.GetComponent<Clickable>().getClicked() ||
                    pauseButton.GetComponent<Clickable>().getClicked())
                {
                    return;
                }

                // Check for Attacking/Interraction
                if (IsInAttackRange(clickRow, clickCol))
                {
                    // Attack Furniture
                    if (IsFurniture(clickRow, clickCol)) { return; }
                    // Open Chest
                    else if (IsChest(clickRow, clickCol)) { return; }
                    // Attack Enemy
                    else if (AttackController(clickRow, clickCol)) { return; }
                    // Disarm Trap
                    else if (IsAdjacent(clickRow, clickCol) && GameManager.gmInstance.GetTrapAtLoc(clickRow, clickCol) != null)
                    {
                        Trap t = GameManager.gmInstance.GetTrapAtLoc(clickRow, clickCol);
                        if (t.DisarmTrap(this)) { return; }
                    }
                }

                // Check for Movement
                {
                    if (IsPlayer(clickRow, clickCol))
                    {
                        PlayerEnd();
                        return;
                    }
                    if (!IsBlocked(clickRow, clickCol))
                    {
                        if (AttemptMove<Player>(clickRow, clickCol))
                        {
                            PlayerEnd();
                        }
                        return;
                    }
                    else { return; }
                }
            }
        }
    }

    protected void SetInitialValues()
    {
        playerName = Data.activeCharacter ?? "Bob";
        entityName = playerName;

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

        attackRange = 1;

        baseHp = 100;
        hp = baseHp + (int)((float)baseHp * 0.05f * (int)(defense * defenseScale));
        maxHp = hp;

        skills.Add(GameManager.gmInstance.SkillGenerator.Fireball);


        // Set ClassType Variables
        {
            if (Data.characterClass == ClassType.Archer)
            {
                sl.spriteLibraryAsset = ArcherLibrary;
            }
            if (Data.characterClass == ClassType.Warrior)
            {
                sl.spriteLibraryAsset = WarriorLibrary;
            }
            if (Data.characterClass == ClassType.Paladin)
            {
                sl.spriteLibraryAsset = PaladinLibrary;
            }
            if (Data.characterClass == ClassType.Thief)
            {
                sl.spriteLibraryAsset = ThiefLibrary;
            }
            if (Data.characterClass == ClassType.Mage)
            {
                sl.spriteLibraryAsset = MageLibrary;
            }
            if (Data.characterClass == ClassType.Necromancer)
            {
                sl.spriteLibraryAsset = NecromancerLibrary;
            }
            if (Data.characterClass == ClassType.Druid)
            {
                sl.spriteLibraryAsset = DruidLibrary;
            }
            if (Data.characterClass == ClassType.Monk)
            {
                sl.spriteLibraryAsset = MonkLibrary;
            }
            if (Data.characterClass == ClassType.Bard)
            {
                sl.spriteLibraryAsset = BardLibrary;
            }
            if (Data.characterClass == ClassType.Knight)
            {
                sl.spriteLibraryAsset = KnightLibrary;
            }
            if (Data.characterClass == ClassType.Swordsman)
            {
                sl.spriteLibraryAsset = SwordsmanLibrary;
            }
            if (Data.characterClass == ClassType.Priest)
            {
                sl.spriteLibraryAsset = PriestLibrary;
            }
        }
    }

    protected  void InitializeObjects() {

        SoundManager.sm.PlayDungeonMusic();

        journal.SetPlayer(this);

        sl = transform.GetChild(1).gameObject.GetComponent<SpriteLibrary>();
        gear = new PlayerGear();
        targetTiles = new List<GameObject>();

        hpBar = GameObject.FindGameObjectWithTag("hpbar");
        mpBar = GameObject.FindGameObjectWithTag("mpbar");
        expBar = GameObject.FindGameObjectWithTag("xpbar");

        journalButton = GameObject.FindGameObjectWithTag("characterButton");
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton");
        mapButton = GameObject.FindGameObjectWithTag("MapButton");
        skillsButton = GameObject.FindGameObjectWithTag("SkillsButton");
        journalButton.GetComponent<Button>().onClick.AddListener(JournalListener);
        pauseButton.GetComponent<Button>().onClick.AddListener(PauseListener);
        mapButton.GetComponent<Button>().onClick.AddListener(MapListener);
        skillsButton.GetComponent<Button>().onClick.AddListener(SkillsListener);

        stairsModal = GameObject.FindGameObjectWithTag("stairspopup");
        stairsModal.SetActive(false);
        stairsModal.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(StairsReject);
        stairsModal.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(StairsConfirm);
    }

    private void StairsConfirm()
    {
        NextFloor();
    }

    private void StairsReject()
    {
        stairsModal.SetActive(false);
        stairsOpen = false;
    }

    public void SaveScores()
    {
        endScreenHolder.transform.GetChild(3).gameObject.SetActive(true);
        endScreenHolder.transform.GetChild(4).gameObject.SetActive(true);
        Data.inProgress = false;
        Data.RemoveActive();
    }

    private bool CheckDead()
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
                GameObject op = Instantiate(endingScreen, new Vector3(0, 0, 0), Quaternion.identity);
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

    void DrawTargetHighlights()
    {
        if (!drawTargets)
        {
            drawTargets = true;
            int range = activeSkill.range;
            if (activeSkill.canTargetSelf)
            {
                GameObject t = Instantiate(tileHighlight, transform.position, Quaternion.identity);
                targetTiles.Add(t);
            }
            for (int r = row - range; r <= row + range; r++)
            {
                for (int c = col - range; c <= col + range; c++)
                {
                    if (Mathf.Abs(row - r) + Mathf.Abs(col - c) <= range && GameManager.gmInstance.Dungeon.tileMap[r, c].getFloor())
                    {
                        if (r == row && c == col && !activeSkill.canTargetSelf) continue;
                        GameObject t = Instantiate(tileHighlight, new Vector2(c, r), Quaternion.identity);
                        targetTiles.Add(t);
                    }
                }
            }
        }
    }

    bool TargetSelection()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            int clickRow = (int)GameManager.gmInstance.mRow;
            int clickCol = (int)GameManager.gmInstance.mCol;
            foreach (GameObject t in targetTiles)
            {
                if (t.transform.position.y == clickRow && t.transform.position.x == clickCol)
                {
                    bool castSuccessful = activeSkill.Activate(this, clickRow, clickCol);


                    // Finish Casting
                    targetMode = false;
                    foreach (GameObject o in targetTiles)
                    {
                        Destroy(o);
                    }
                    targetTiles.Clear();
                    activeSkill = null;
                    drawTargets = false;

                    if (castSuccessful)
                    {
                        if (clickRow != row || clickCol != col)
                        {
                            int rowDiff = Mathf.Abs(clickRow - row);
                            int colDiff = Mathf.Abs(clickCol - col);
                            if (rowDiff > colDiff)
                            {
                                SetAttackAnimation(clickRow, col);
                            }
                            if (colDiff >= rowDiff)
                            {
                                SetAttackAnimation(row, clickCol);
                            }
                            attacking = true;
                        }
                    }
                    
                    return castSuccessful;
                }
            }
            // Cancel Casting
            targetMode = false;
            foreach (GameObject o in targetTiles)
            {
                Destroy(o);
            }
            targetTiles.Clear();
            activeSkill = null;
            drawTargets = false;
            return false;
        }
        return false;
    }

    public void PlayerStart()
    {
        turnStart = false;
        ApplyStatusEffects("start");
        UpdatePlayerStatusEffectAlerts();
    }

    public void PlayerEnd()
    {
        GameManager.gmInstance.playersTurn = false;
        turnStart = true;
        ApplyStatusEffects("end");
        UpdateStatusEffectDuration();
        UpdatePlayerStatusEffectAlerts();
    }

    void DebugMenu() {
        //Debugging tool
        if (Input.GetKeyDown("t"))
        {
            NextFloor();
        }
        if (Input.GetKeyDown("b"))
        {
            AddStatusEffect(new StatusEffect(EffectType.paralysis, 5, EffectOrder.Start));
        }
        if (Input.GetKeyDown("n"))
        {
            AddStatusEffect(new StatusEffect(EffectType.defense_down, 5, EffectOrder.Status));
        }
    }

    public void SaveCharacterData(){
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
        Data.statusEffects = statusEffects;
        Data.skills = skills;
        Data.gear = gear;
        Data.SaveCharacter();
    }

    public void LoadCharacterData(){
        
        
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
        statusEffects = Data.statusEffects ?? new List<StatusEffect>();
        skills = Data.skills ?? new List<Skill>();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveCharacterData();
            Data.SaveToFile();
        }
    }

    public void CloseJournal()
    {
        journal.closeJournal();
    }

    public void NextFloor(){
        stairsModal.SetActive(true);
        GameManager.gmInstance.Dungeon.setFullBright(false);
        GameManager.gmInstance.level++;
        SaveCharacterData();
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
            GameManager.gmInstance.Log.AddLog($">{entityName} picks up +{amount} gold.");
            SoundManager.sm.PlayCoinSound();
            AddTextPopup($"+{amount}", new Color(255f / 255f, 238f / 255f, 0f / 255f));
            GameManager.gmInstance.Dungeon.removeFromItemList(row, col);
            Destroy(other.gameObject);

        }
        if (other.gameObject.tag == "Equipment")
        {
            if (journal.getEquipment().Count == Journal.maxSlots) return;
            journal.addEquipment(other.GetComponent<Pickup>().GetItem());
            Debug.Log("ITEM:" + other.GetComponent<Pickup>().GetItem().itemName);
            Destroy(other.gameObject);
            GameManager.gmInstance.Dungeon.removeFromItemList(row, col);
            SoundManager.sm.PlayPickupSound();
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            GameManager.gmInstance.Log.AddLog($">{entityName} activates {other.GetComponent<Trap>().trapName}.");
            other.GetComponent<Trap>().TriggerTrap(this);
        }
    }

    bool IsFurniture(int row, int col)
    {
        Furniture f = GameManager.gmInstance.GetFurnitureAtLoc(row, col);
        if (f != null)
        {
            f.setDamage(-1);
            PlayerEnd();
            SoundManager.sm.PlayStickSounds();
            return true;
        }
        else return false;
    }

    bool IsChest(int row, int col)
    {
        Chest c = GameManager.gmInstance.GetChestAtLoc(row, col);
        if (c != null)
        {
            c.OpenChest();
            PlayerEnd();
            SoundManager.sm.PlayStickSounds();
            return true;
        }
        else return false;
    }

    bool IsPlayer(int nRow, int nCol)
    {
        return (nRow == row && nCol == col);
    }

    void JournalListener()
    {
        if (!openJournal && !openPause)
        {
            SoundManager.sm.PlayBookOpen();
            journal.CreateJournal(this);
            openJournal = true;
            journalButton.GetComponent<Clickable>().setClicked(false);
        }
    }

    void SkillsListener()
    {
        if (!openJournal && !openPause)
        {
            journal.tab = 5;
            SoundManager.sm.PlayBookOpen();
            journal.CreateJournal(this);
            openJournal = true;
            journalButton.GetComponent<Clickable>().setClicked(false);
        }
    }

    void MapListener()
    {
        if (!openJournal && !openPause)
        {
            journal.tab = 3;
            SoundManager.sm.PlayBookOpen();
            journal.CreateJournal(this);
            openJournal = true;
            journalButton.GetComponent<Clickable>().setClicked(false);
        }
    }

    void PauseListener()
    {
        if (!openJournal && !openPause)
        {
            pauseMenu.CreatePause(this);
            openPause = true;
            pauseButton.GetComponent<Clickable>().setClicked(false);
        }
    }

    void UpdatePathing()
    {
        if (atTarget) {
            if (interrupt)
            {
                moving = false;
                interrupt = false;
                path = null;
            }
            else
            {
                SetNextTarget();
                PlayerEnd();
                SoundManager.sm.PlayStepSound();
            }
        }
        return;
    }

    bool AttackController(int clickRow, int clickCol)
    {
        Enemy enemy = GameManager.gmInstance.GetEnemyAtLoc(clickRow, clickCol);
        if (enemy != null && attacking == false)
        {
            SetAttackAnimation(clickRow, clickCol);
            attacking = true;
            float targetDamage = 0.0f;
            int dice = Random.Range(0, 100);
            if (dice <= (int)(critical * criticalScale))
            {
                targetDamage = CalculateDamage(3);
                enemy.TakeDamage(targetDamage, Color.red, true);
                
            }
            else
            {
                targetDamage = CalculateDamage(3);
                enemy.TakeDamage(CalculateDamage(), Color.red);
            }

            GameManager.gmInstance.Log.AddLog($">{entityName} attacks {enemy.entityName} for {(int)targetDamage} HP.");

            if (Random.Range(0f, 1f) <= 0.05f)
            {
                enemy.AddStatusEffect(new StatusEffect(EffectType.bleed, 5, EffectOrder.End));
            }
            
                
            return true;
        }
        return false;
    }

    public override void TakeDamage(float d, Color c, bool critical = false, bool canDodge = true){
        if (canDodge)
        {
            int dice = Random.Range(1, 101);
            if (dice <= (int)(evade * evadeScale))
            {
                AddTextPopup("Evade", new Color(50f / 255f, 205f / 255f, 50f / 255f));
                GameManager.gmInstance.Log.AddLog($">{entityName} evades attack.");
                return;
            }
        }
        

        hp += (int)d;

        if (d < 0)
        {
            SoundManager.sm.PlayHitSound();
            moving = false;
            AddTextPopup($"{d}", c);
            SpawnBlood();
            GameManager.gmInstance.Log.AddLog($">Enemy hits {entityName} for " + (int)d + " damage.");
            path = null;
        }
        else
        {
            SoundManager.sm.PlayHealSound();
            AddTextPopup($"+{d}", c);
            GameManager.gmInstance.Log.AddLog($">{entityName} healed for +" + (int)d + " HP.");
        }

        if (hp <= 0)
        {
            dead = true;
            SpawnBlood();
            GameManager.gmInstance.Log.AddLog($">{entityName} died.");
        }
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        
    }

    public override void AddStatusEffect(StatusEffect se)
    {
        base.AddStatusEffect(se);
        UpdatePlayerStatusEffectAlerts();
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

    void UpdatePlayerStatus()
    {
        if (hp < 0) hp = 0;
        if (hp > maxHp) hp = maxHp;

        if (mp < 0) mp = 0;
        if (mp > maxMp) mp = maxMp;

        hpBar.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = hp + "/" + maxHp;
        mpBar.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = mp + "/" + maxMp;
        expBar.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = exp + "/" + maxExp;

        GameObject redbar = hpBar.transform.GetChild(0).gameObject;
        GameObject bluebar = mpBar.transform.GetChild(0).gameObject;
        GameObject greenbar = expBar.transform.GetChild(0).gameObject;

        Canvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        Vector2 canvasScale = new Vector2(canvas.transform.localScale.x, canvas.transform.localScale.y);

        // Health Bar
        Vector2 redSizeDelta = redbar.transform.GetComponent<RectTransform>().sizeDelta;
        Vector2 redFinalScale = new Vector2(redSizeDelta.x * canvasScale.x, redSizeDelta.y * canvasScale.y);
        float redWidth = redFinalScale.x * hpBar.transform.GetComponent<RectTransform>().localScale.x;

        Vector3 pos1 = redbar.transform.position;
        float redStartingPos = hpBar.transform.GetChild(2).gameObject.transform.position.x;
        float redPos = redStartingPos - (redWidth - (((float)hp / (float)maxHp) * redWidth));
        redbar.transform.GetComponent<RectTransform>().position = new Vector3(redPos, pos1.y, pos1.z);

        // Mana Bar
        Vector2 blueSizeDelta = bluebar.transform.GetComponent<RectTransform>().sizeDelta;
        Vector2 blueFinalScale = new Vector2(blueSizeDelta.x * canvasScale.x, blueSizeDelta.y * canvasScale.y);
        float blueWidth = blueFinalScale.x * mpBar.transform.GetComponent<RectTransform>().localScale.x;

        Vector3 pos3 = bluebar.transform.position;
        float blueStartingPos = mpBar.transform.GetChild(2).gameObject.transform.position.x;
        float bluePos = blueStartingPos - (blueWidth - (((float)mp / (float)maxMp) * blueWidth));
        bluebar.transform.GetComponent<RectTransform>().position = new Vector3(bluePos, pos3.y, pos3.z);

        // Experience Bar
        Vector2 greenSizeDelta = greenbar.transform.GetComponent<RectTransform>().sizeDelta;
        Vector2 greenFinalScale = new Vector2(greenSizeDelta.x * canvasScale.x, greenSizeDelta.y * canvasScale.y);
        float greenWidth = greenFinalScale.x * expBar.transform.GetComponent<RectTransform>().localScale.x;

        Vector3 pos2 = greenbar.transform.position;
        float greenStartingPos = expBar.transform.GetChild(2).gameObject.transform.position.x;
        float greenPos = greenStartingPos + (((float)exp / (float)maxExp) * greenWidth);
        greenbar.transform.GetComponent<RectTransform>().position = new Vector3(greenPos, pos2.y, pos2.z);

    }

    private void UpdatePlayerStatusEffectAlerts()
    {
        GameObject StatusEffectBar = GameObject.FindGameObjectWithTag("StatusBar");

        foreach (Transform t in StatusEffectBar.transform)
        {
            Destroy(t.gameObject);
        }
        
        foreach (StatusEffect e in statusEffects)
        {
            GameObject alert = Instantiate(StatusEffectAlert, new Vector3(0,0,0), Quaternion.identity);
            alert.transform.parent = StatusEffectBar.transform;
            alert.GetComponent<StatusEffectAlert>().Setup(e.effectId, e.duration);
        }
    }

    public override void RemoveAllStatusEffect(EffectType type)
    {
        base.RemoveAllStatusEffect(type);
        UpdatePlayerStatusEffectAlerts();
    }

    public int GetGold(){
        return gold;
    }
    public int GetExpLevel(){
        return expLevel;
    }
    public int GetExp(){
        return exp;
    }
    public int GetMaxExp(){
        return maxExp;
    }
    public PlayerGear GetGear(){
        return gear;
    }
    public string GetName()
    {
        return playerName;
    }
    public void AddExp(int i){
        exp += i;
        GameManager.gmInstance.Log.AddLog($">{entityName} receives +{i} experience.");
        int levelPoints = 0;
        GameManager.gmInstance.score += i;
        bool didLevel = false;
        while(exp >= maxExp){
            SoundManager.sm.PlayLevelUpSound();
            exp -= maxExp;
            expLevel++;
            GameManager.gmInstance.Log.AddLog($">{entityName} reaches level {expLevel}!");
            baseHp = (int)(baseHp * 1.1);
            hp = baseHp + (int)((float)baseHp * 0.05f * (int)(defense * defenseScale));
            maxHp = hp;
            mp = maxMp;
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
    
    public void SetGear(PlayerGear gear)
    {
        this.gear = gear;
    }

    public void UpdateHealthAndDefense()
    {
        var newHp = baseHp + (int)((float)baseHp * 0.05f * (int)(defense * defenseScale));
        var diff = maxHp - newHp;
        if (diff > 0) // decrease health
        {
            maxHp = newHp;
            hp -= diff;
            if (hp <= 0) hp = 1;
        }
        if (diff < 0) //increase health
        {
            maxHp = newHp;
            hp -= diff;
            if (hp > maxHp) hp = maxHp;
        }
    }
}