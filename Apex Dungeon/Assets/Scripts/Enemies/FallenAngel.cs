using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenAngel : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Fallen Angel";

        skills.Add(GameManager.gmInstance.SkillGenerator.LightningBolt);
        skills.Add(GameManager.gmInstance.SkillGenerator.StaticPalm);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bless);
        skills.Add(GameManager.gmInstance.SkillGenerator.Teleport);
    }
}
