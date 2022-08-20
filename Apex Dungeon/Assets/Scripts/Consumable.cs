using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public Consumable(string id, string name, string flavor, string desc, Sprite img, int level = 1){
        itemName = name;
        flavorText = flavor;
        description = desc;
        image = img;
        this.level = level;
        this.id = id;
    }

    public Consumable(SaveConsumable sc)
    {
        itemName = sc.itemName;
        flavorText = sc.rank;
        description = sc.description;
        level = sc.itemLevel;
        id = sc.id;
    }

    public void SetStats(string n, string f, string d, Sprite s)
    {
        itemName = n;
        flavorText = f;
        description = d;
        image = s;
    }

    public override void Create()
    {
    }

    public override void UseItem()
    {
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(id == "HealthPotion"){
            int baseHP = 50;
            float appliedHP = baseHP * Mathf.Pow(1.5f,level-1);
            p.addHP((int)appliedHP);
            SoundManager.sm.PlayPotionSound();
        }
        if(id == "ManaPotion"){
            p.addMP(20);
        }
        if(id == "SkipOrb"){
            p.nextFloor();
            SoundManager.sm.PlayMagicSound();
            p.CloseJournal();
        }
        if(id == "TeleportOrb"){
            Vector2 pos = GameManager.gmInstance.Dungeon.getRandomUnoccupiedTile();
            p.setPosition((int)pos.x, (int)pos.y);
            SoundManager.sm.PlayMagicSound();
            p.CloseJournal();
        }
        if(id == "DeathOrb"){
            List<Vector2> activeShadows = GameManager.gmInstance.Dungeon.getActiveShadowCoords();
            foreach(Vector2 v in activeShadows){
                Enemy e = GameManager.gmInstance.getEnemyAtLoc((int)v.x,(int)v.y);
                if(e != null){
                    e.die();
                }
            }
            SoundManager.sm.PlayMagicSound();
            p.CloseJournal();
        }
        if(id == "MapFragment"){
            GameManager.gmInstance.Dungeon.setFullExplored(true);
            SoundManager.sm.PlayPageTurn();
        }
        if(id == "LightOrb"){
            GameManager.gmInstance.Dungeon.setFullBright(true);
            SoundManager.sm.PlayMagicSound();
            p.CloseJournal();
        }
    }
}
