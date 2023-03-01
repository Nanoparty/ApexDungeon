using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griffin : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Griffin";

        skills.Add(GameManager.gmInstance.SkillGenerator.LightningBolt);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bite);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
        skills.Add(GameManager.gmInstance.SkillGenerator.Stun);
    }
}
