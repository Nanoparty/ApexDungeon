using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Ghost";

        skills.Add(GameManager.gmInstance.SkillGenerator.Teleport);
        skills.Add(GameManager.gmInstance.SkillGenerator.IceShard);
        skills.Add(GameManager.gmInstance.SkillGenerator.Slash);
        skills.Add(GameManager.gmInstance.SkillGenerator.LifeDrain);
        skills.Add(GameManager.gmInstance.SkillGenerator.IcePalm);
    }
}
