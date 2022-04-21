using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class FadeIn : MonoBehaviour
{
    private float FadeRate = 1f;
    private Image image;
    private GameObject t1;
    private GameObject t2;
    private GameObject t3;
    private float targetAlpha;
    // Use this for initialization
    void Start()
    {
        this.image = this.GetComponent<Image>();
        this.t1 = this.gameObject.transform.GetChild(0).gameObject;
        this.t2 = this.gameObject.transform.GetChild(1).gameObject;
        this.t3 = this.gameObject.transform.GetChild(2).gameObject;
        if (this.image == null)
        {
            Debug.LogError("Error: No image on " + this.name);
        }
        this.targetAlpha = 1f;
        Debug.Log("color:"+image.color.a);
    }

    // Update is called once per frame
    void Update()
    {
        Color curColor = this.image.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);

        Color curColor2 = this.t1.GetComponent<Text>().color;
        Color curColor3 = this.t2.GetComponent<Text>().color;
        
        

        if (alphaDiff > 0.01f)
        {
            Debug.Log("color:"+image.color.a);
            //Debug.Log("Lerp:"+Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime));
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.image.color = curColor;

            curColor2.a = Mathf.Lerp(curColor2.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.t1.GetComponent<Text>().color = curColor2;

            curColor3.a = Mathf.Lerp(curColor3.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.t2.GetComponent<Text>().color = curColor3;
            this.t3.GetComponent<Text>().color = curColor3;
            
        }
        else
        {
            //Debug.Log("DONE LARPING");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ending = true;
        }
    }

    
}