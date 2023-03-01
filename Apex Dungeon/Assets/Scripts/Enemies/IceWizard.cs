using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWizard : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Ice Wizard";

        skills.Add(GameManager.gmInstance.SkillGenerator.IceShard);
        skills.Add(GameManager.gmInstance.SkillGenerator.ManaDrain);
        skills.Add(GameManager.gmInstance.SkillGenerator.Teleport);
        skills.Add(GameManager.gmInstance.SkillGenerator.Silence);
    }
}
