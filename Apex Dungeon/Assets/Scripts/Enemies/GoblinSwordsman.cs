using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSwordsman : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Goblin Swordsman";

        skills.Add(GameManager.gmInstance.SkillGenerator.Slash);
        skills.Add(GameManager.gmInstance.SkillGenerator.PoisonPalm);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
    }
}
