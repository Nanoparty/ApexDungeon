using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CharacterSelect : MonoBehaviour, IPointerDownHandler
{
    bool clicked = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked " + this.name);
        clicked = true;
        this.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
        Debug.Log("CLICKED CHARACTER");
    }

    public bool getClicked()
    {
        return clicked;
    }
    public void setClicked(bool b)
    {
        clicked = b;
        if(b){
            this.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
        }else{
            this.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
