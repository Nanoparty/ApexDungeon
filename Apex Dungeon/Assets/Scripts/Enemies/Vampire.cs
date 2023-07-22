using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Vampire";

        skills.Add(GameManager.gmInstance.SkillGenerator.Fireball);
        skills.Add(GameManager.gmInstance.SkillGenerator.Hypnosis);
        skills.Add(GameManager.gmInstance.SkillGenerator.LifeDrain);
        skills.Add(GameManager.gmInstance.SkillGenerator.BloodCurse);

    }
}
