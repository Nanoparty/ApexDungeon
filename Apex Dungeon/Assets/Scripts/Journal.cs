using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Journal", menuName = "ScriptableObjects/Journal")]
public class Journal : ScriptableObject
{
    public GameObject journalPrefab;
    public GameObject mapBlock;
    public GameObject mapPlayerBlock;
    public GameObject mapPrefab;
    public GameObject popupPrefab;
    public Sprite[] itemFrames;
    public Sprite[] journalTabs;

    public void CreateJournal()
    {

    }
}
