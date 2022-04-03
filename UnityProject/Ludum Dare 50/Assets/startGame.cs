using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class startGame : MonoBehaviour
{
    bool gameStarted;
    public GameObject player;
    // Update is called once per frame
    
    public void startGameCall()
    {
        if (gameStarted)
            return;
        gameStarted = true;
        if (!gameObject.activeSelf)
            return;

        FindObjectOfType<audioManager>().playStartSound();
        StartCoroutine(fadeOut());
    }
    public float fadeTime = 2f;
    IEnumerator fadeOut()
    {
        float timer = 0;
        var group = GetComponent<CanvasGroup>();
        while (timer < fadeTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            group.alpha = Mathf.Lerp(1, 0, timer / fadeTime);
        }
        group.alpha = 0;
        player.SetActive(true);
    }
}
