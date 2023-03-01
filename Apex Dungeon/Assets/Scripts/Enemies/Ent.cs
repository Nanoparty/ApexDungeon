using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Ent";

        skills.Add(GameManager.gmInstance.SkillGenerator.PoisonSpike);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bind);
        skills.Add(GameManager.gmInstance.SkillGenerator.Slash);
    }
}
