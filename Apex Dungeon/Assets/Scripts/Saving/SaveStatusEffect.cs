using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveStatusEffect
{
    public string effectId;
    public string effectOrder;
    public int duration;

    public SaveStatusEffect(string effectId, string effectOrder, int duration)
    {
        this.effectId= effectId;
        this.effectOrder= effectOrder;
        this.duration = duration;
    }
}
