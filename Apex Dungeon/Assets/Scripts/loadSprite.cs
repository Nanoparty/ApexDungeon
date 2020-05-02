using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sprite[] s = Resources.LoadAll<Sprite>("mapIcons");
        GetComponent<Image>().sprite = s[0];
    }

    
}
