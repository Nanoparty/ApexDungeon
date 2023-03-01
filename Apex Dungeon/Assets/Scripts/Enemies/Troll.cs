using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Troll";

        skills.Add(GameManager.gmInstance.SkillGenerator.Bless);
        skills.Add(GameManager.gmInstance.SkillGenerator.PoisonPalm);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bash);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);

    }
}
