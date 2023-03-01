using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Snake";

        skills.Add(GameManager.gmInstance.SkillGenerator.Bite);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bind);
        skills.Add(GameManager.gmInstance.SkillGenerator.PoisonSpike);
    }
}
