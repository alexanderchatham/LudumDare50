using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    // Start is called before the first frame update
    public int direction;
    public float speed;
    float timer = 0;
    float carDeathTime =5f;
    private void Start()
    {
        speed = Random.Range(3, 7);
    }
    public void setRound(int round)
    {
        speed = speed + round * .25f;
        if (speed > 8)
            speed = 8;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,Time.deltaTime*direction*speed,0);
        timer += Time.deltaTime;
        if (timer > carDeathTime)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        FindObjectOfType<audioManager>().playImpact();
        print("Hit: " + collision.gameObject.name);
        FindObjectOfType<gameManager>().loseGame();
    }
}
