using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    private void Start()
    {
        base.Start();

        // Skills
        skills.Add(GameManager.gmInstance.SkillGenerator.Pound);
    }
}
