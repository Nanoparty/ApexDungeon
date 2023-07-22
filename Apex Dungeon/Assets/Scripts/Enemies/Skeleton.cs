using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Skeleton";

        skills.Add(GameManager.gmInstance.SkillGenerator.Bite);
        skills.Add(GameManager.gmInstance.SkillGenerator.Pound);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);

    }
}
