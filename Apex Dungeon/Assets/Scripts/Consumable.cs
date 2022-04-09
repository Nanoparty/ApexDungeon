using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public Consumable(string name, string flavor, string desc, Sprite img){
        itemName = name;
        flavorText = flavor;
        description = desc;
        image = img;
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
        if(itemName == "Health Potion"){
            p.addHP(20);
        }
        if(itemName == "Mana Potion"){
            p.addMP(20);
        }
        if(itemName == "Skip Orb"){
            p.nextFloor();
        }
        if(itemName == "Teleport Orb"){
            Vector2 pos = GameManager.gmInstance.Dungeon.getRandomUnoccupiedTile();
            p.setPosition((int)pos.x, (int)pos.y);
        }
        if(itemName == "Death Orb"){
            List<Vector2> activeShadows = GameManager.gmInstance.Dungeon.getActiveShadowCoords();
            foreach(Vector2 v in activeShadows){
                Enemy e = GameManager.gmInstance.getEnemyAtLoc((int)v.x,(int)v.y);
                if(e != null){
                    e.die();
                    Debug.Log("KILL ENEMY");
                }
            }
        }
        if(itemName == "Map Fragment"){
            GameManager.gmInstance.Dungeon.setFullExplored(true);
        }
        if(itemName == "Light Orb"){
            GameManager.gmInstance.Dungeon.setFullBright(true);
        }
    }
}
