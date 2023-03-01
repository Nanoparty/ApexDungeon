using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Enemy
{
    protected override void Start()
    {
        base.Start();

        entityName = "Minotaur";

        skills.Add(GameManager.gmInstance.SkillGenerator.FlamePalm);
        skills.Add(GameManager.gmInstance.SkillGenerator.Bash);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
        skills.Add(GameManager.gmInstance.SkillGenerator.Fireball);
    }
}
