using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    public void triggerInventory()
    {
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        bool open = p.getInventory();
        Debug.Log("Toggle Inventory:"+ !open);
        if (open)
        {
            p.setInventory(false);
        }
        else
        {
            p.setInventory(true);
        }
    }

    
}
