using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public string effectId;
    public int duration;
    public bool spawned;

    public StatusEffect(string id, int duration)
    {
        this.effectId = id;
        this.duration = duration;
    }
}
