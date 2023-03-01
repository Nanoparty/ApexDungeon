using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Demon Eye";

        skills.Add(GameManager.gmInstance.SkillGenerator.LightningBolt);
        skills.Add(GameManager.gmInstance.SkillGenerator.LifeDrain);
        skills.Add(GameManager.gmInstance.SkillGenerator.MagicMissile);
        skills.Add(GameManager.gmInstance.SkillGenerator.Hypnosis);
    }
}
