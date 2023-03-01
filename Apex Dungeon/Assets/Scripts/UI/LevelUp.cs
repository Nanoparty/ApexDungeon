using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUp", menuName = "ScriptableObjects/LevelUp")]
public class LevelUp : ScriptableObject
{
    public GameObject levelPopup;

    private GameObject levelRoot;

    private int strength;
    private int defense;
    private int critical;
    private int evasion;

    private int tempStrength;
    private int tempDefense;
    private int tempCritical;
    private int tempEvasion;

    private int prevLevel;
    private int expLevel;
    private int levelPoints;

    public void CreatePopup(Player player, int points)
    {
        strength = player.getStrength();
        defense = player.getDefense();
        critical = player.getCritical();
        evasion = player.getEvade();
        expLevel = player.getExpLevel();
        prevLevel = expLevel - points;
        levelPoints = points;

        Vector3 pos = new Vector3(0, 0, 0);
        GameObject levelPop = GameObject.Instantiate(levelPopup, pos, Quaternion.identity);
        levelPop.transform.SetParent(GameObject.FindGameObjectWithTag("LevelUp").transform, false);
        levelRoot = levelPop;

        Util.setText(levelPop, prevLevel + "->" + expLevel, 1);
        Util.setText(levelPop, "Points Remaining:" + levelPoints, 2);

        GameObject Strength = Util.getChild(levelPop, 3);
        GameObject Defense = Util.getChild(levelPop, 4);
        GameObject Crit = Util.getChild(levelPop, 5);
        GameObject Evade = Util.getChild(levelPop, 6);

        Util.setText(Strength, strength.ToString(), 1);
        Util.setText(Defense, defense.ToString(), 1);
        Util.setText(Crit, critical.ToString(), 1);
        Util.setText(Evade, evasion.ToString(), 1);

        Util.setListener(Strength, StrengthAddListener, 2);
        Util.setListener(Strength, StrengthSubListener, 3);

        Util.setListener(Defense, DefenseAddListener, 2);
        Util.setListener(Defense, DefenseSubListener, 3);

        Util.setListener(Crit, CritAddListener, 2);
        Util.setListener(Crit, CritSubListener, 3);

        Util.setListener(Evade, EvadeAddListener, 2);
        Util.setListener(Evade, EvadeSubListener, 3);

        Util.setListener(levelPop, LevelConfirmListener, 7);

        tempStrength = strength;
        tempDefense = defense;
        tempCritical = critical;
        tempEvasion = evasion;
    }

    void StrengthAddListener()
    {
        SoundManager.sm.PlayMenuSound();
        if (levelPoints == 0) return;

        tempStrength++;
        levelPoints--;
        Util.setText(levelRoot, (tempStrength).ToString(), 3, 1);
        Util.setColor(levelRoot, Color.green, 3, 1);
        Util.setText(levelRoot, "Points Remaining:" + levelPoints, 2);
    }
    void StrengthSubListener()
    {
        SoundManager.sm.PlayMenuSound();
        if (tempStrength == strength) return;

        tempStrength--;
        levelPoints++;
        if (tempStrength == strength)
        {
            Util.setColor(levelRoot, new Color(94f / 255f, 52f / 255f, 0f), 3, 1);
        }
        Util.setText(levelRoot, (tempStrength).ToString(), 3, 1);
        Util.setText(levelRoot, "Points Remaining:" + levelPoints, 2);
    }
    void DefenseAddListener()
    {
        SoundManager.sm.PlayMenuSound();
        if (levelPoints == 0) return;

        tempDefense++;
        levelPoints--;
        Util.setText(levelRoot, tempDefense.ToString(), 4, 1);
        Util.setColor(levelRoot, Color.green, 4, 1);
        Util.setText(levelRoot, "Points Remaining:" + levelPoints, 2);
    }
    void DefenseSubListener()
    {
        SoundManager.sm.PlayMenuSound();
        if (tempDefense == defense) return;

        tempDefense--;
        levelPoints++;
        Util.setText(levelRoot, tempDefense.ToString(), 4, 1);
        if (tempDefense == defense)
        {
            Util.setColor(levelRoot, new Color(94f / 255f, 52f / 255f, 0f), 4, 1);
        }
        Util.setText(levelRoot, "Points Remaining:" + levelPoints, 2);
    }
    void CritAddListener()
    {
        SoundManager.sm.PlayMenuSound();
        if (levelPoints == 0) return;

        tempCritical++;
        levelPoints--;
        Util.setText(levelRoot, tempCritical.ToString(), 5, 1);
        Util.setColor(levelRoot, Color.green, 5, 1);
        Util.setText(levelRoot, "Points Remaining:" + levelPoints, 2);
    }
    void CritSubListener()
    {
        SoundManager.sm.PlayMenuSound();
        if (tempCritical == critical) return;

        tempCritical--;
        levelPoints++;
        Util.setText(levelRoot, tempCritical.ToString(), 5, 1);
        if (tempDefense == defense)
        {
            Util.setColor(levelRoot, new Color(94f / 255f, 52f / 255f, 0f), 5, 1);
        }
        Util.setText(levelRoot, "Points Remaining:" + levelPoints, 2);
    }
    void EvadeAddListener()
    {
        SoundManager.sm.PlayMenuSound();
        if (levelPoints == 0) return;

        levelPoints--;
        tempEvasion++;
        Util.setText(levelRoot, tempEvasion.ToString(), 6, 1);
        Util.setColor(levelRoot, Color.green, 6, 1);
        Util.setText(levelRoot, "Points Remaining:" + levelPoints, 2);
    }
    void EvadeSubListener()
    {
        SoundManager.sm.PlayMenuSound();
        if (tempEvasion == evasion) return;

        tempEvasion--;
        levelPoints++;
        Util.setText(levelRoot, tempEvasion.ToString(), 6, 1);
        if (tempDefense == defense)
        {
            Util.setColor(levelRoot, new Color(94f / 255f, 52f / 255f, 0f), 6, 1);
        }
        Util.setText(levelRoot, "Points Remaining:" + levelPoints, 2);
    }

    void LevelConfirmListener()
    {
        SoundManager.sm.PlayMenuSound();
        if (levelPoints > 0) return;

        strength = tempStrength;
        defense = tempDefense;
        critical = tempCritical;
        evasion = tempEvasion;
        ClosePopup();
    }

    void ClosePopup()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.setStrength(strength);
        player.setDefense(defense);
        player.setCritical(critical);
        player.setEvasion(evasion);
        player.openLevel = false;
        GameObject.Destroy(levelRoot);
    }
}
