using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinShaman : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Goblin Shaman";

        skills.Add(GameManager.gmInstance.SkillGenerator.PoisonSpike);
        skills.Add(GameManager.gmInstance.SkillGenerator.Restore);
        skills.Add(GameManager.gmInstance.SkillGenerator.Silence);
        skills.Add(GameManager.gmInstance.SkillGenerator.Plague);
    }
}
