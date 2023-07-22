using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWizard : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Flame Wizard";

        skills.Add(GameManager.gmInstance.SkillGenerator.Fireball);
        skills.Add(GameManager.gmInstance.SkillGenerator.FlamePalm);
        skills.Add(GameManager.gmInstance.SkillGenerator.ManaDrain);
        skills.Add(GameManager.gmInstance.SkillGenerator.MagicMissile);
    }
}
