using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Demon";

        skills.Add(GameManager.gmInstance.SkillGenerator.FlamePalm);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
        skills.Add(GameManager.gmInstance.SkillGenerator.Scratch);
    }
}
