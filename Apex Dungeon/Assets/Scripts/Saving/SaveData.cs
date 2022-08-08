using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float musicVolume;
    public float soundVolume;

    public bool musicOn;
    public bool soundOn;

    public List<string> usedNames;

    public List<SavePlayer> players;

    public List<(string, int)> scores;

    public SaveData()
    {
        musicOn = Data.music;
        soundOn = Data.sound;

        musicVolume = Data.musicVolume;
        soundVolume = Data.soundVolume;

        usedNames = Data.names;
        scores = Data.scores;

        players = new List<SavePlayer>();

        foreach (CharacterData cd in Data.charData ?? new List<CharacterData>())
        {
            players.Add(new SavePlayer(cd));
        }
    }
}
