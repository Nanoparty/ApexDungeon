using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float FadeRate;
    private Image image;
    private float targetAlpha;
    bool begin = false;
    // Use this for initialization
    void Start()
    {
        this.image = this.GetComponent<Image>();
        
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
            Debug.Log("update");
            Color curColor = this.image.color;
            float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
            if (curColor.a > 0)
            {
                curColor.a = curColor.a - FadeRate;        //Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
                this.image.color = curColor;
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