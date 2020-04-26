using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    public void triggerInventory()
    {
        Debug.Log("CLICK");
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        bool open = p.getInventory();
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
