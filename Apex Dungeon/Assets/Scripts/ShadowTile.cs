using UnityEngine;

public class ShadowTile : MonoBehaviour
{
    private int row;
    private int col;
    public bool visible;

    public SpriteRenderer image;

    public ShadowTile(int r, int c, bool v)
    {
        row = r;
        col = c;
        visible = v;
    }
}
