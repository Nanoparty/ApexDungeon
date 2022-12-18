using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
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
    [SerializeField] private Button ArcherButton;
    [SerializeField] private Button KnightButton;
    [SerializeField] private Button WarriorButton;
    [SerializeField] private Button MonkButton;
    [SerializeField] private Button PriestButton;
    [SerializeField] private Button NecromancerButton;
    [SerializeField] private Button PaladinButton;
    [SerializeField] private Button MageButton;
    [SerializeField] private Button DruidButton;
    [SerializeField] private Button BardButton;
    [SerializeField] private Button ThiefButton;
    [SerializeField] private Button SwordsmanButton;

    // Character Creator Finish Buttons
    [SerializeField] private Button Accept;
    [SerializeField] private Button Cancel;

    // Class Animators
    [SerializeField] private AnimatorController Archer;
    [SerializeField] private Sprite ArcherSprite;

    [SerializeField] private AnimatorController Knight;
    [SerializeField] private AnimatorController Warrior;
    [SerializeField] private AnimatorController Monk;
    [SerializeField] private AnimatorController Priest;
    [SerializeField] private AnimatorController Necromancer;
    [SerializeField] private AnimatorController Paladin;
    [SerializeField] private AnimatorController Mage;
    [SerializeField] private AnimatorController Druid;
    [SerializeField] private AnimatorController Bard;
    [SerializeField] private AnimatorController Thief;
    [SerializeField] private AnimatorController Swordsman;

    private List<string> taken;
    private bool done;
    private List<CharacterData> charData;
    private ClassType characterClass;

    void Start()
    {
        taken = Data.names ?? new List<string>();
        done = false;
        charData = Data.charData ?? new List<CharacterData>();
        characterClass = ClassType.Archer;
        ArcherButton.gameObject.GetComponent<Image>().color = Color.green;

        NameError.SetActive(false);

        Accept.onClick.AddListener(acceptListener);
        Cancel.onClick.AddListener(cancelListener);

        ArcherButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Archer;
            DisableAllClasses();
            ArcherButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Archer;
        });
        WarriorButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Warrior;
            DisableAllClasses();
            WarriorButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Warrior;
        });
        PaladinButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Paladin;
            DisableAllClasses();
            PaladinButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Paladin;
        });
        ThiefButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Thief;
            DisableAllClasses();
            ThiefButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Thief;
        });
        PriestButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Priest;
            DisableAllClasses();
            PriestButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Priest;
        });
        MageButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Mage;
            DisableAllClasses();
            MageButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Mage;
        });
        MonkButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Monk;
            DisableAllClasses();
            MonkButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Monk;
        });
        NecromancerButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Necromancer;
            DisableAllClasses();
            NecromancerButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Necromancer;
        });
        BardButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Bard;
            DisableAllClasses();
            BardButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Bard;
        });
        KnightButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Knight;
            DisableAllClasses();
            KnightButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Knight;
        });
        DruidButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Druid;
            DisableAllClasses();
            DruidButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Druid;
        });
        SwordsmanButton.onClick.AddListener(() => {
            ClassAnimator.runtimeAnimatorController = Swordsman;
            DisableAllClasses();
            SwordsmanButton.gameObject.GetComponent<Image>().color = Color.green;
            characterClass = ClassType.Swordsman;
        });
    }

    private void Update()
    {
        Debug.Log("Class: " + characterClass);
    }

    void acceptListener()
    {
        SoundManager.sm.PlayMenuSound();
        string name = NameField.text.ToUpper();

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

        Debug.Log("CCM:" + characterClass);
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

    void DisableAllClasses()
    {
        ArcherButton.gameObject.GetComponent<Image>().color = Color.white;
        WarriorButton.gameObject.GetComponent<Image>().color = Color.white;
        KnightButton.gameObject.GetComponent<Image>().color = Color.white;
        MonkButton.gameObject.GetComponent<Image>().color = Color.white;
        MageButton.gameObject.GetComponent<Image>().color = Color.white;
        NecromancerButton.gameObject.GetComponent<Image>().color = Color.white;
        BardButton.gameObject.GetComponent<Image>().color = Color.white;
        ThiefButton.gameObject.GetComponent<Image>().color = Color.white;
        PaladinButton.gameObject.GetComponent<Image>().color = Color.white;
        SwordsmanButton.gameObject.GetComponent<Image>().color = Color.white;
        DruidButton.gameObject.GetComponent<Image>().color = Color.white;
        PriestButton.gameObject.GetComponent<Image>().color = Color.white;
    }
}
