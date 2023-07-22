using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Beetle";

        skills.Add(GameManager.gmInstance.SkillGenerator.Headbutt);
    }
}
