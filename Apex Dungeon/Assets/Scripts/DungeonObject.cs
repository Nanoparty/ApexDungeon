using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonObject
{
    public Tile[,] tileMap;
    public GameObject[,] shadowMap;
    public List<GameObject> activeShadows;
    public List<Vector2> visibleTiles;
    
}
