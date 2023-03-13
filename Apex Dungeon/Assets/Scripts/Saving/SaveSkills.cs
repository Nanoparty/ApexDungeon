using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveSkill
{
    public string skillType;

    public SaveSkill(string skillType)
    {
        this.skillType = skillType;
    }
}
