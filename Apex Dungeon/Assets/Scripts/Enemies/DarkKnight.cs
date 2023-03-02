using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnight : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Dark Knight";

        skills.Add(GameManager.gmInstance.SkillGenerator.Lacerate);
        skills.Add(GameManager.gmInstance.SkillGenerator.Fireball);
        skills.Add(GameManager.gmInstance.SkillGenerator.BloodCurse);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
    }
}
