using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Spider";

        skills.Add(GameManager.gmInstance.SkillGenerator.Scratch);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bind);
        skills.Add(GameManager.gmInstance.SkillGenerator.Lacerate);
    }
}
