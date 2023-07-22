using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static CharacterClass;

public class CharacterCreatorManager : MonoBehaviour
{
    // Character Name
    [SerializeField] private TMP_InputField NameField;
    [SerializeField] private GameObject NameError;

    // Character Class
    [SerializeField] private Animator ClassAnimator;
    //[SerializeField] private Button ArcherButton;
    //[SerializeField] private Button KnightButton;
    //[SerializeField] private Button WarriorButton;
    //[SerializeField] private Button MonkButton;
    //[SerializeField] private Button PriestButton;
    //[SerializeField] private Button NecromancerButton;
    //[SerializeField] private Button PaladinButton;
    //[SerializeField] private Button MageButton;
    //[SerializeField] private Button DruidButton;
    //[SerializeField] private Button BardButton;
    //[SerializeField] private Button ThiefButton;
    //[SerializeField] private Button SwordsmanButton;

    [SerializeField] private Button Left;
    [SerializeField] private Button Right;

    [SerializeField] private Button StrengthInfo;
    [SerializeField] private Button DefenseInfo;
    [SerializeField] private Button CriticalInfo;
    [SerializeField] private Button EvasionInfo;

    [SerializeField] private GameObject StrengthDescription;
    [SerializeField] private GameObject DefenseDescription;
    [SerializeField] private GameObject EvasionDescription;
    [SerializeField] private GameObject CriticalDescription;

    [SerializeField] private TMP_Text ClassNameText;
    [SerializeField] private TMP_Text Stats;
    
    [SerializeField] private TMP_Text EquipmentText;
    [SerializeField] private TMP_Text SkillsText;

    // Character Creator Finish Buttons
    [SerializeField] private Button Accept;
    [SerializeField] private Button Cancel;

    // Class Animators
    [SerializeField] private RuntimeAnimatorController Archer;
    [SerializeField] private RuntimeAnimatorController Knight;
    [SerializeField] private RuntimeAnimatorController Warrior;
    [SerializeField] private RuntimeAnimatorController Monk;
    [SerializeField] private RuntimeAnimatorController Priest;
    [SerializeField] private RuntimeAnimatorController Necromancer;
    [SerializeField] private RuntimeAnimatorController Paladin;
    [SerializeField] private RuntimeAnimatorController Mage;
    [SerializeField] private RuntimeAnimatorController Druid;
    [SerializeField] private RuntimeAnimatorController Bard;
    [SerializeField] private RuntimeAnimatorController Thief;
    [SerializeField] private RuntimeAnimatorController Swordsman;

    private List<string> taken;
    private bool done;
    private List<CharacterData> charData;
    private ClassType characterClass;
    private int numClasses = Enum.GetNames(typeof(ClassType)).Length;

    void Start()
    {
        StrengthDescription.SetActive(false);
        DefenseDescription.SetActive(false);
        EvasionDescription.SetActive(false);
        CriticalDescription.SetActive(false);

        taken = Data.names ?? new List<string>();
        done = false;
        charData = Data.charData ?? new List<CharacterData>();
        characterClass = ClassType.Knight;
        
        NameError.SetActive(false);

        Accept.onClick.AddListener(acceptListener);
        Cancel.onClick.AddListener(cancelListener);

        Left.onClick.AddListener(LeftListener);
        Right.onClick.AddListener(RightListener);

        StrengthInfo.onClick.AddListener(() => { StrengthDescription.SetActive(true); });
        DefenseInfo.onClick.AddListener(() => { DefenseDescription.SetActive(true); });
        CriticalInfo.onClick.AddListener(() => { CriticalDescription.SetActive(true); });
        EvasionInfo.onClick.AddListener(() => { EvasionDescription.SetActive(true); });

        PopulateClassInfo();
    }

    private void Update()
    {
    }

    private void LeftListener()
    {
        SoundManager.sm.PlayMenuSound();
        int classInt = (int) characterClass - 1;
        if (classInt < 0) classInt = numClasses - 1;
        characterClass = (ClassType)(classInt);

        PopulateClassInfo();
    }

    private void RightListener()
    {
        SoundManager.sm.PlayMenuSound();
        int classInt = (int)characterClass + 1;
        if (classInt >= numClasses) classInt = 0;
        characterClass = (ClassType)(classInt);

        PopulateClassInfo();
    }

    private void PopulateClassInfo()
    {
        if (characterClass == ClassType.Archer)
        {
            ClassAnimator.runtimeAnimatorController = Archer;
        }
        if (characterClass == ClassType.Warrior)
        {
            ClassAnimator.runtimeAnimatorController = Warrior;
        }
        if (characterClass == ClassType.Priest)
        {
            ClassAnimator.runtimeAnimatorController = Priest;
        }
        if (characterClass == ClassType.Paladin)
        {
            ClassAnimator.runtimeAnimatorController = Paladin;
        }
        if (characterClass == ClassType.Necromancer)
        {
            ClassAnimator.runtimeAnimatorController = Necromancer;
        }
        if (characterClass == ClassType.Thief)
        {
            ClassAnimator.runtimeAnimatorController = Thief;
        }
        if (characterClass == ClassType.Mage)
        {
            ClassAnimator.runtimeAnimatorController = Mage;
        }
        if (characterClass == ClassType.Monk)
        {
            ClassAnimator.runtimeAnimatorController = Monk;
        }
        if (characterClass == ClassType.Druid)
        {
            ClassAnimator.runtimeAnimatorController = Druid;
        }
        if (characterClass == ClassType.Bard)
        {
            ClassAnimator.runtimeAnimatorController = Bard;
        }
        if (characterClass == ClassType.Knight)
        {
            ClassAnimator.runtimeAnimatorController = Knight;
        }
        if (characterClass == ClassType.Swordsman)
        {
            ClassAnimator.runtimeAnimatorController = Swordsman;
        }
        ClassNameText.text = characterClass.ToString();

        ClassStats stats = CharacterClass.GetClassStats(characterClass);

        Stats.text = $"{stats.strength}\n{stats.defense}\n{stats.critical}\n{stats.evasion}";

        if (stats.equipment == null) return;

        String equipmentText = "";
        foreach(Equipment e in stats.equipment)
        {
            equipmentText += $"-{e.itemName}\n";
        }

        String skillText = "";
        foreach(Skill s in stats.skills)
        {
            skillText += $"-{s.skillName}\n";
        }

        EquipmentText.text = equipmentText;
        SkillsText.text = skillText;
    }

    void acceptListener()
    {
        SoundManager.sm.PlayMenuSound();
        string name = NameField.text;

        if (!(name.Length > 0)) return;
        if (taken.Contains(name) && !done)
        {
            NameError.SetActive(true);
            return;
        }

        done = true;
        Data.playerName = name;
        taken.Add(name);
        Data.names = taken;
        Data.activeCharacter = name;

        charData.Add(new CharacterData(name, characterClass));
        Data.charData = charData;
        Data.LoadActiveData();
        Data.loadData = false;
        SoundManager.sm.StopMusic();
        SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
    }

    void cancelListener()
    {
        SoundManager.sm.PlayMenuSound();
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    public void CloseDescriptions()
    {
        StrengthDescription.SetActive(false);
        DefenseDescription.SetActive(false);
        EvasionDescription.SetActive(false);
        CriticalDescription.SetActive(false);
    }

    
}
