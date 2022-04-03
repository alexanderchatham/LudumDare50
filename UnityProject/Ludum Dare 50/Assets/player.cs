using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
public class player : MonoBehaviour
{
    public GameObject selectedTile;
    // Start is called before the first frame update

    bool alive = true;
    public void kill()
    {
        alive = false;
        GetComponent<BoxCollider2D>().enabled = false;
        disableText();
        GetComponent<Image>().color = Color.red;
        //trigger animation? or effect
    }
    public void movePlayer(InputAction.CallbackContext context)
    {
        if (!alive||!gameObject.activeSelf)
            return;
        if (context.started)
        {
            if (moving)
                return;
            Debug.Log("Move" + context.valueType +context.ReadValue<Vector2>().x+" "+ context.ReadValue<Vector2>().y);
            Vector2 movement = context.ReadValue<Vector2>();
            if(movement.x == -1)
            {
                if(selectedTile.GetComponent<movementTile>().leftTile)
                {
                    
                    selectedTile = selectedTile.GetComponent<movementTile>().leftTile;
                    StartCoroutine(move(selectedTile.transform.position));
                }    
            }
            if(movement.x == 1)
            {
                
                if (selectedTile.GetComponent<movementTile>().rightTile)
                {
                    selectedTile = selectedTile.GetComponent<movementTile>().rightTile;
                    StartCoroutine(move(selectedTile.transform.position));
                }

            }
            if(movement.y == 1)
            {
                if (selectedTile.GetComponent<movementTile>().upTile)
                {
                    selectedTile = selectedTile.GetComponent<movementTile>().upTile;
                    StartCoroutine(move(selectedTile.transform.position));
                }
            }
            if(movement.y == -1)
            {
                if (selectedTile.GetComponent<movementTile>().downTile)
                {
                    selectedTile = selectedTile.GetComponent<movementTile>().downTile;
                    StartCoroutine(move(selectedTile.transform.position));
                }
            }
            selectedTile.GetComponent<movementTile>().Select();
        }
    }
    public float moveTime = 1f;
    public bool moving = false;
    public string[] sayings;
    public GameObject textbox;
    public TextMeshProUGUI textboxtext;
    public void triggerSpeech()
    {
        if (textbox.activeSelf)
            return;
        textbox.SetActive(true);
        textboxtext.text = sayings[Random.Range(0, sayings.Length)];
        Invoke("disableText", 1f);
    }
    void disableText()
    {
        textbox.SetActive(false);
    }
    IEnumerator move(Vector3 target)
    {
        FindObjectOfType<audioManager>().playMove();
        float timer = 0f;
        moving = true;
        while (timer<moveTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target, timer/moveTime);
        }
        transform.position = target;
        moving = false;
    }
}
