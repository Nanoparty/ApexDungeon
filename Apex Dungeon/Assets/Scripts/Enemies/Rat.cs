using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Rat";

        skills.Add(GameManager.gmInstance.SkillGenerator.Bite);
        skills.Add(GameManager.gmInstance.SkillGenerator.Lacerate);
        skills.Add(GameManager.gmInstance.SkillGenerator.IcePalm);

    }
}
