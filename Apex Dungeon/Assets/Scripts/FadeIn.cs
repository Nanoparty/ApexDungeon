using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float FadeRate;
    private Image image;
    private float targetAlpha;
    // Use this for initialization
    void Start()
    {
        this.image = this.GetComponent<Image>();
        if (this.image == null)
        {
            Debug.LogError("Error: No image on " + this.name);
        }
        this.targetAlpha = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Color curColor = this.image.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (alphaDiff < 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.image.color = curColor;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    
}