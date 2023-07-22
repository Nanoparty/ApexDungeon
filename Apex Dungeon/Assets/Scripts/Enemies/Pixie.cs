using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixie : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Pixie";

        skills.Add(GameManager.gmInstance.SkillGenerator.LightningBolt);
        skills.Add(GameManager.gmInstance.SkillGenerator.Hypnosis);
        skills.Add(GameManager.gmInstance.SkillGenerator.Silence);
        skills.Add(GameManager.gmInstance.SkillGenerator.MagicMissile);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bless);
    }
}
