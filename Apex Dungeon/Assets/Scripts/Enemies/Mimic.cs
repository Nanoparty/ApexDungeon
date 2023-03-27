using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : Enemy
{

    private bool hidden = true;

    protected override void Start()
    {
        base.Start();

        animator.enabled = false;

        entityName = "Mimic";

        skills.Add(GameManager.gmInstance.SkillGenerator.Bite);
        skills.Add(GameManager.gmInstance.SkillGenerator.Berserk);
        skills.Add(GameManager.gmInstance.SkillGenerator.IceShard);
        skills.Add(GameManager.gmInstance.SkillGenerator.Restore);
    }

    protected override void Update()
    {
        base.Update();

        if (hidden)
        {
            animator.enabled = false;
            healthBar.enabled = false;
        }
    }

    public override void TakeDamage(float change, Color color, bool critical = false, bool canDodge = true)
    {
        if (hidden)
        {
            hidden = false;
            agro = true;
            animator.enabled = true;
            animator.Play("Idle");
            enemyTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            return;
        }
        base.TakeDamage(change, color, critical, canDodge);
    }

    public override bool MoveEnemy()
    {
        if (!atTarget) return false;

        StartTurn();

        if (dead || hp <= 0)
        {
            Die();
            return false;
        }

        if (hidden)
        {
            EndTurn();
            return true;
        }

        if (skipTurn || sleeping)
        {
            skipTurn = false;
            EndTurn();
            return true;
        }

        if (agro)
        {
            //ATTACKING
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            CheckPlayerMoved(player);
            if (AttackController(player))
            {
                EndTurn();
                return true;
            }

            if (root)
            {
                EndTurn();
                return true;
            }

            base.AttemptMove<Enemy>(player.GetRow(), player.GetCol());

            if (moving)
            {
                if (atTarget)
                {
                    SetNextTarget();
                }
            }
        }
        else
        {
            //IDLE
            CheckAgro();
            MoveRandom();
        }

        EndTurn();
        return true;
    }
}
