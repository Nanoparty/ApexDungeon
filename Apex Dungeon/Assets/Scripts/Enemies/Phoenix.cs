using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoenix : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Phoenix";

        skills.Add(GameManager.gmInstance.SkillGenerator.Fireball);
        skills.Add(GameManager.gmInstance.SkillGenerator.Restore);
        skills.Add(GameManager.gmInstance.SkillGenerator.Slash);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
    }
}
