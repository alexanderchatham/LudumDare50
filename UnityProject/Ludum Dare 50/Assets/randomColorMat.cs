using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomColorMat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var material = GetComponent<MeshRenderer>().material;
        material.color = Color.HSVToRGB(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

}
