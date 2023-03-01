using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Reaper";

        skills.Add(GameManager.gmInstance.SkillGenerator.BloodCurse);
        skills.Add(GameManager.gmInstance.SkillGenerator.Slash);
        skills.Add(GameManager.gmInstance.SkillGenerator.LifeDrain);
        skills.Add(GameManager.gmInstance.SkillGenerator.IceShard);

    }
}
