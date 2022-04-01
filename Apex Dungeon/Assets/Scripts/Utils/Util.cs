using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class Util 
{
    //Strength.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = damage.ToString();

    public static GameObject getChild(GameObject o, int child){
        return o.transform.GetChild(child).gameObject;
    }
    public static void setText(GameObject o, string s){
        o.GetComponent<TMP_Text>().text = s;
    }
    public static void setText(GameObject o, string s, int child){
        o.transform.GetChild(child).gameObject.GetComponent<TMP_Text>().text = s;
    }
    public static void setText(GameObject o, string s, int child1, int child2){
        o.transform.GetChild(child1).gameObject.transform.GetChild(child2).gameObject.GetComponent<TMP_Text>().text = s;
    }

    public static void setColor(GameObject o, Color c, int child1, int child2){
        o.transform.GetChild(child1).gameObject.transform.GetChild(child2).gameObject.GetComponent<TMP_Text>().color = c;
    }

    public static void setListener(GameObject o, UnityEngine.Events.UnityAction action){
        o.GetComponent<Button>().onClick.AddListener(action);
    }
    public static void setListener(GameObject o, UnityEngine.Events.UnityAction action, int child){
        o.transform.GetChild(child).gameObject.GetComponent<Button>().onClick.AddListener(action);
    }

}
