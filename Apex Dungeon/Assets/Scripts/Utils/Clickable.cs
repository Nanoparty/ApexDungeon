using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerDownHandler
{
    bool clicked = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked " + this.name);
        clicked = true;
    }

    public bool getClicked()
    {
        return clicked;
    }
    public void setClicked(bool b)
    {
        clicked = b;
    }
}
