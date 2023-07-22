using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Spirit";

        skills.Add(GameManager.gmInstance.SkillGenerator.IceShard);
        skills.Add(GameManager.gmInstance.SkillGenerator.LifeDrain);
        skills.Add(GameManager.gmInstance.SkillGenerator.Hypnosis);

    }
}
