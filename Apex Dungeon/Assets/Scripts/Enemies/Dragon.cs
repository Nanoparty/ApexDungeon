using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Dragon";

        skills.Add(GameManager.gmInstance.SkillGenerator.Fireball);
        skills.Add(GameManager.gmInstance.SkillGenerator.Lacerate);
        skills.Add(GameManager.gmInstance.SkillGenerator.Restore);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bash);
    }
}
