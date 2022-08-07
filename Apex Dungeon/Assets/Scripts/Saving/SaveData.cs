using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    float musicVolume;
    float soundVolume;

    bool musicOn;
    bool soundOn;

    List<string> usedNames;

    List<SavePlayer> players;

    List<string> scores;
}
