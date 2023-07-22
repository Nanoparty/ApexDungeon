using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Plant";

        skills.Add(GameManager.gmInstance.SkillGenerator.PoisonSpike);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bind);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bite);

    }
}
