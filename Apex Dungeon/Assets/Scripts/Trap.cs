using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Trap : MonoBehaviour
{
    private int row;
    private int col;
    private bool disarmed;
    private int damage;

    private SpriteRenderer sr;

    [SerializeField] public int baseDamage = 15;
    [SerializeField] public ParticleSystem destruction;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameManager.gmInstance.Dungeon == null) return;

        if (!GameManager.gmInstance.Dungeon.tileMap[row, col].visible)
        {
            sr.enabled = false;
        }
        else
        {
            sr.enabled = true;
        }
    }

    public void Setup(int row, int col, int floor)
    {
        this.row = row;
        this.col = col;
        damage = (int)(baseDamage + baseDamage * 0.1 * (floor - 1));
        GameManager.gmInstance.AddTrapToList(this);
    }

    public void TriggerTrap()
    {
        if (disarmed) return;

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.takeAttack(-damage);
        DestroyTrap();
    }

    public void DisarmTrap()
    {
        DestroyTrap();   
    }

    public void DestroyTrap()
    {
        SoundManager.sm.PlayTrapSound();
        disarmed = true;
        Instantiate(destruction, new Vector2(col, row), Quaternion.identity);
        GameManager.gmInstance.removeTrap(this);
        Destroy(gameObject);
    }

    public int GetRow() { return row; }
    public int GetCol() { return col; }
}
