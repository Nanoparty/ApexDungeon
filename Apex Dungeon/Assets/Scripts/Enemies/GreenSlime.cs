using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Green Slime";

        skills.Add(GameManager.gmInstance.SkillGenerator.LifeDrain);
        skills.Add(GameManager.gmInstance.SkillGenerator.Headbutt);
        skills.Add(GameManager.gmInstance.SkillGenerator.PoisonSpike);
    }
}
