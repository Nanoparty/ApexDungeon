using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "ScriptableObjects/Biome")]
public class Biome : ScriptableObject
{
    public GameObject[] floorTiles;
    public GameObject[] verticalWallTiles;
    public GameObject[] horizontalWallTiles;
    public GameObject[] voidTiles;
    public GameObject[] enemies;
    public GameObject stairs;
}
