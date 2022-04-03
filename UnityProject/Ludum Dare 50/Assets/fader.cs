using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        fade(0);
    }
    public void fade(float f)
    {
        StartCoroutine(alphaTo(f));
    }
    IEnumerator alphaTo(float f)
    {
        var image = GetComponent<Image>();
        float timer =0;
        while (timer < .5f)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            image.color = new Color(0, 0, 0, Mathf.Lerp(image.color.a, f, timer / .5f));
        }
    }
}
