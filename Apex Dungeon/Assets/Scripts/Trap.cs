using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Trap : MonoBehaviour
{
    private int row;
    private int col;
    private bool disarmed;
    private int damage;

    private SpriteRenderer sr;

    [SerializeField] private int baseDamage = 15;
    [SerializeField] private ParticleSystem destruction;
    [SerializeField] GameObject disarmText;

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

    public void TriggerTrap(MovingEntity me)
    {
        if (disarmed) return;

        if (typeof(Player).IsInstanceOfType(me))
        {
            Debug.Log("Player Trap");
            Player player = (Player)me;
            player.takeAttack(-damage);
        }
        if (typeof(Enemy).IsInstanceOfType(me)){
            Debug.Log("Enemy Trap");
            Enemy enemy = (Enemy)me;
            enemy.takeDamage(-damage);
        }

        DestroyTrap();
    }

    public void DisarmTrap()
    {
        GameObject text = Instantiate(disarmText, new Vector2(col, row), Quaternion.identity);
        text.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"DISARM";
        text.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
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
