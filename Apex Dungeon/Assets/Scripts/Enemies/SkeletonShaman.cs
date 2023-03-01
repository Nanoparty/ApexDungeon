using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShaman : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Skeleton Shaman";

        skills.Add(GameManager.gmInstance.SkillGenerator.Fireball);
        skills.Add(GameManager.gmInstance.SkillGenerator.MagicMissile);
        skills.Add(GameManager.gmInstance.SkillGenerator.ManaDrain);

    }
}
