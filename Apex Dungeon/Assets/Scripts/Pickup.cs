using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item;

    bool pickedUp = false;
    void Start()
    {
        
    }

    public Item GetItem(){
        return item;
    }

    public void SetItem(Item i){
        item = i;
    }

}
