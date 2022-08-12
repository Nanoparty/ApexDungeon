using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item;

    public Item GetItem(){
        return item;
    }

    public void SetItem(Item i){
        item = i;
    }

}
