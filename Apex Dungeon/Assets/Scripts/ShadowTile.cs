using UnityEngine;
using System.Collections;

public class ShadowTile : MonoBehaviour
{
    private int row;
    private int col;
    public bool visible;

    public SpriteRenderer image;

    // Use this for initialization
    void Start()
    {
        //disableImage();
    }

    public ShadowTile(int r, int c, bool v)
    {
        row = r;
        col = c;
        visible = v;
    }

    //public void enableImage()
    //{
    //    this.GetComponent<SpriteRenderer>().enabled = true;
    //}
    //public static void disableImage()
    //{
    //    this.GetComponent<SpriteRenderer>().enabled = false;

    //}
}
