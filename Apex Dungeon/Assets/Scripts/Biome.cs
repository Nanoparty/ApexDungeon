using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "ScriptableObjects/Biome")]
public class Biome : ScriptableObject
{
    public string biomeName;

    public bool top_bottom;

    public GameObject[] floorTiles;
    public GameObject[] verticalWallTiles;
    public GameObject[] horizontalWallTiles;
    public GameObject[] voidTiles;
    public GameObject[] enemies;

    public float decorationPercentage;
    public GameObject[] decorations;
    public GameObject[] obstacles;

    public GameObject stairs;

    public GameObject[] topWalls;
    public GameObject[] bottomWalls;
}
