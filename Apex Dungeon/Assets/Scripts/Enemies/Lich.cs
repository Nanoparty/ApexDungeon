using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Lich";

        skills.Add(GameManager.gmInstance.SkillGenerator.IceShard);
        skills.Add(GameManager.gmInstance.SkillGenerator.Fireball);
        skills.Add(GameManager.gmInstance.SkillGenerator.LightningBolt);
        skills.Add(GameManager.gmInstance.SkillGenerator.ManaDrain);
        skills.Add(GameManager.gmInstance.SkillGenerator.LifeDrain);
        skills.Add(GameManager.gmInstance.SkillGenerator.Restore);
        skills.Add(GameManager.gmInstance.SkillGenerator.MagicMissile);
        skills.Add(GameManager.gmInstance.SkillGenerator.Silence);
    }
}
