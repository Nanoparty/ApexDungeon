using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarrior : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Skeleton Warrior";

        skills.Add(GameManager.gmInstance.SkillGenerator.Slash);
        skills.Add(GameManager.gmInstance.SkillGenerator.FlamePalm);
        skills.Add(GameManager.gmInstance.SkillGenerator.ArmorPolish);

    }
}
