using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centaur : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Centaur";

        skills.Add(GameManager.gmInstance.SkillGenerator.Thrust);
        skills.Add(GameManager.gmInstance.SkillGenerator.Stomp);
    }
}
