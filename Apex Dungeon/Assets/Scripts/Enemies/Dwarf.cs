using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Dwarf";

        skills.Add(GameManager.gmInstance.SkillGenerator.FlamePalm);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bash);
    }
}
