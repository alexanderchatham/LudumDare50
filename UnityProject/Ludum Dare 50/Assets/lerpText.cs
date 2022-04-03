using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class lerpText : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if(lerpAlpha)
            StartCoroutine(lerpTextAlpha());
        if (lerpSize)
            StartCoroutine(lerpTextSize());
    }
    IEnumerator lerpTextAlpha()
    {
        while (true)
        {
            text.alpha = Mathf.PingPong(Time.time, 1);
            yield return new WaitForEndOfFrame();
        }
    }
    public float fontSizeDif = 5f;
    IEnumerator lerpTextSize()
    {
        float fontSize = text.fontSize;
        while (true)
        {
            text.fontSize = fontSize - fontSizeDif/2 + Mathf.PingPong(Time.time, fontSizeDif);
            yield return new WaitForEndOfFrame();
        }
    }
    public bool lerpAlpha;
    public bool lerpSize;
}
