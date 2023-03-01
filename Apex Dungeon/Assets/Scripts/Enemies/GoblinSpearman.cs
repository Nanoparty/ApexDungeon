using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSpearman : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Goblin Spearman";

        skills.Add(GameManager.gmInstance.SkillGenerator.Thrust);
        skills.Add(GameManager.gmInstance.SkillGenerator.PoisonPalm);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
    }
}
