using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Sprite GreenCursor;
    public Sprite RedCursor;

    public Sprite GreenFull;
    public Sprite RedFull;

    public SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetGreen()
    {
        sr.sprite = GreenCursor;
    }

    public void SetRed()
    {
        sr.sprite = RedCursor;
    }

    public void SetRedFull()
    {
        sr.sprite = RedFull;
    }

    public void SetGreenFull()
    {
        sr.sprite = GreenFull;
    }
}
