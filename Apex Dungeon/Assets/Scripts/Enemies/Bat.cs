using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Bat";

        skills.Add(GameManager.gmInstance.SkillGenerator.Bite);
    }
}
