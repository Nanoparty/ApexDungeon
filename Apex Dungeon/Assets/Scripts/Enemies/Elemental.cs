using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Elemental";

        skills.Add(GameManager.gmInstance.SkillGenerator.Fireball);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
        skills.Add(GameManager.gmInstance.SkillGenerator.Silence);
        skills.Add(GameManager.gmInstance.SkillGenerator.ManaDrain);
    }
}
