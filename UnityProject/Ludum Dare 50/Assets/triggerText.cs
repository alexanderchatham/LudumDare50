using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerText : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<player>())
        {
            collision.gameObject.GetComponent<player>().triggerSpeech();
            FindObjectOfType<audioManager>().playClose();
        }
    }
}
