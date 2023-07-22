using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeti : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Yeti";

        skills.Add(GameManager.gmInstance.SkillGenerator.IcePalm);
        skills.Add(GameManager.gmInstance.SkillGenerator.IceShard);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bash);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);

    }
}
