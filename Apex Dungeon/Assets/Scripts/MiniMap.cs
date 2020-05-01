using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap 
{
    public GameObject block;
    public GameObject pBlock;
    public GameObject minimap;
    private int[,] map;
    private int width = 100;
    private int height = 100;
    Button close;
    GameObject mapRoot;

    public MiniMap(GameObject mm, GameObject block, GameObject pb)
    {
        this.block = block;
        this.minimap = mm;
        this.pBlock = pb;

        map = new int[100, 100];
        buildMap();
        //buildMiniMap();

        width = MapGenerator.width;
        height = MapGenerator.height;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void buildMap()
    {
        for(int i = 0; i < 100; i++)
        {
            for(int j = 0;j < 100; j++)
            {
                map[i, j] = 0;
            }
            
        }
        for(int i = 0; i < 100; i++)
        {
            map[0, i] = 1;
            map[i, 0] = 1;
            map[99, i] = 1;
            map[i, 99] = 1;
        }
        for(int i = 0; i < 300; i++)
        {
            int x = Random.Range(0, 99);
            int y = Random.Range(0, 99);
            map[x, y] = 1;
        }
    }

    void closeListener()
    {
        Debug.Log("CLOSE MAP");
        closeMiniMap();
    }

    public void closeMiniMap()
    {
        GameObject.Destroy(mapRoot);
    }

    public void buildMiniMap()
    {
        mapRoot = GameObject.Instantiate(minimap, new Vector3(0, 0, 0), Quaternion.identity);
        mapRoot.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        int size = 20;
        GameObject mapHolder = GameObject.FindGameObjectWithTag("Map").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        mapHolder.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width * size + 100);
        mapHolder.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height * size + 100);

        close = GameObject.FindGameObjectWithTag("Map").transform.GetChild(1).gameObject.GetComponent<Button>();
        close.onClick.AddListener(closeListener);

        int xOff = -1*((width*size + 100)/2) + 50;
        int yOff = -1 * ((height * size +100)/ 2) + 50;
        
        for (int i = 0; i < height; i++)
        {
            for(int j = 0;j < width; j++)
            {
                if(MapGenerator.tileMap[i,j].type == 2)
                {
                    Vector3 pos = new Vector3(xOff + j * size,yOff + i * size, 0f);
                    GameObject b = GameObject.Instantiate(block, pos, Quaternion.identity);
                    b.transform.SetParent(mapHolder.transform, false);
                }
                else if (MapGenerator.isPlayer(i, j))
                {
                    Vector3 pos = new Vector3(xOff + j * size, yOff + i * size, 0f);
                    GameObject b = GameObject.Instantiate(pBlock, pos, Quaternion.identity);
                    b.transform.SetParent(mapHolder.transform, false);
                }
            }
        }
    }

    
}
