using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bank
{
    int maxItems = 25;

    public List<Item> items;
    public int gold = 0;

    public Bank()
    {
        items = new List<Item>();
    }
}
