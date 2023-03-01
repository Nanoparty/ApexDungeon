using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satyr : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Satyr";

        skills.Add(GameManager.gmInstance.SkillGenerator.PoisonSpike);
        skills.Add(GameManager.gmInstance.SkillGenerator.Stun);
        skills.Add(GameManager.gmInstance.SkillGenerator.Stomp);
    }
}
