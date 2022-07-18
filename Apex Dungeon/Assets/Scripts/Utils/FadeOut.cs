using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class FadeOut : MonoBehaviour
{
    public float FadeRate;
    private Image Background;
    private Image image1;
    private Image image2;
    private Text text1;
    private Text text2;
    private float targetAlpha;
    bool begin = false;

    void Start()
    {
        this.Background = this.GetComponent<Image>();
        this.image1 = this.transform.GetChild(0).GetComponent<Image>();
        this.image2 = this.transform.GetChild(1).GetComponent<Image>();

        this.text1 = this.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        this.text2 = this.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();

        this.targetAlpha = 0f;
        Invoke("Delay", 1f);
    }

    private void Delay()
    {
        begin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (begin)
        {
            //Debug.Log("update");
            Color curColor = this.image1.color;
            Color textColor = this.text1.color;
            Color backColor = this.Background.color;
            float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
            float alphaDiffText = Mathf.Abs(textColor.a - this.targetAlpha);
            if (curColor.a > 0)
            {
                curColor.a = curColor.a - FadeRate;        //Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
                textColor.a = textColor.a - FadeRate;
                backColor.a = backColor.a - FadeRate;
                this.image1.color = curColor;
                this.image2.color = curColor;
                this.text1.color = textColor;
                this.text2.color = textColor;
                this.Background.color = backColor;
            }
            else
            {
                Destroy(this.gameObject);
                GameManager.gmInstance.doingSetup = false;
                GameManager.gmInstance.playersTurn = true;
            }
        }
    }


}