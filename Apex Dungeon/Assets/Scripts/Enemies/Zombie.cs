using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    protected override void Start()
    {
        base.Start();

        // Name
        entityName = "Zombie";

        // Skills
        skills.Add(GameManager.gmInstance.SkillGenerator.Pound);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
    }
}
