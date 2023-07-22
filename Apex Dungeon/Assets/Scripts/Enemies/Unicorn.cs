using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unicorn : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Unicorn";

        skills.Add(GameManager.gmInstance.SkillGenerator.Bless);
        skills.Add(GameManager.gmInstance.SkillGenerator.Stomp);
        skills.Add(GameManager.gmInstance.SkillGenerator.LightningBolt);
        skills.Add(GameManager.gmInstance.SkillGenerator.Stun);

    }
}
