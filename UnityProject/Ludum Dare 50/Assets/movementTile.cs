using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementTile : MonoBehaviour
{
    public GameObject leftTile;
    public GameObject rightTile;
    public GameObject upTile;
    public GameObject downTile;
    public bool finalTile;
    public bool startTile;
    // Start is called before the first frame update
    public void Select()
    {
        if (finalTile)
        {
            FindObjectOfType<gameManager>().endGame();
        }
        if (startTile)
            FindObjectOfType<gameManager>().playIntro();
    }
}
