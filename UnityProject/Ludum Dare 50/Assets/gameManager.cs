using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class gameManager : MonoBehaviour
{
   
    public GameObject player;
    public GameObject startTile;
    public GameObject endTile;
    public GameObject finalTile;
    public GameObject startPanel;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI highscoreText;
    public GameObject newHighScore;
    public Animator reaper;
    int round;
    // Start is called before the first frame update
    void Start()
    {
        round = PlayerPrefs.GetInt("level", 0);
        print("round: " + round);
        if(round>0)
        {
            player.SetActive(true);
            startPanel.SetActive(false);
        }
        int highscore = PlayerPrefs.GetInt("highscore", 0);
        if (round > highscore)
        {
            //new highscore
            PlayerPrefs.SetInt("highscore", round);
            highscore = round;
            newHighScore.SetActive(true);
        }
        highscoreText.text = highscore.ToString();
        roundTime = roundTime + round * 5;
        roundText.text = ( round+ 1).ToString();
        setTiles();
        timerText.text = roundTime.ToString();
        int rand = Random.Range(4, 9);
        print(rand);
        print(transform.GetChild(rand * 8).position.y);
        transform.GetChild(rand * 8).GetComponent<movementTile>().startTile = true;
        startTile.transform.position = new Vector3(startTile.transform.position.x, transform.GetChild(rand * 8).position.y,startTile.transform.position.z);
        startTile.GetComponent<movementTile>().rightTile = transform.GetChild(rand * 8).gameObject;
        player.transform.position = startTile.transform.GetChild(startTile.transform.childCount-1).transform.position;
        int rand2 = Random.Range(0, 4);
        endTile.transform.position = new Vector3(endTile.transform.position.x, transform.GetChild(rand2 * 8).position.y, endTile.transform.position.z);
        finalTile = transform.GetChild(rand2 * 8 + 7).gameObject;



        //makes game play
    }
    bool gameStarted = false;
    public TextMeshProUGUI reapertext;
    public GameObject[] sidePanels;
    public GameObject reaperTextbox;
    public string[] reaperSayings;
    public string[] reaperEndSayings;
    public string[] reaperDeathSayings;
    public void startGame()
    {
        if (gameStarted)
            return;
        gameStarted = true;
        reaperTextbox.SetActive(true);
        StartCoroutine(disableAfterTime(reaperTextbox, 3f));
        foreach (var panel in sidePanels)
        {
            panel.GetComponent<Animator>().SetTrigger("start");
            panel.GetComponent<ParticleSystem>().Play();
        }
        reapertext.text = reaperSayings[Random.Range(0, reaperSayings.Length)];
        int round = PlayerPrefs.GetInt("level", 0);
        startTile.GetComponent<Image>().color = Color.clear;
        endTile.GetComponent<Image>().color = Color.clear;
        StartCoroutine(GameTimer());
        var spawners = FindObjectsOfType<carSpawner>();
        foreach (var spawn in spawners)
        {
            spawn.setSpawn(round);
        }
        reaper.SetTrigger("start");
    }

    IEnumerator disableAfterTime(GameObject a,float t)
    {
        yield return new WaitForSeconds(t);
        a.SetActive(false);
    }
    public void playIntro()
    {
        if (round == 0)
            print("first round dialog");
        else
        {

            //so its not the first round
        }
        startGame();
    }

    void setTiles()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var tile = transform.GetChild(i).GetComponent<movementTile>();
            if (i > 7)
                tile.upTile = transform.GetChild(i - 8).gameObject;
            if (i < 80)
                tile.downTile = transform.GetChild(i + 8).gameObject;
            if (i % 8 != 0)
                tile.leftTile = transform.GetChild(i - 1).gameObject;
            if (i % 8 != 7)
                tile.rightTile = transform.GetChild(i + 1).gameObject;
        }
    }
    public bool gameOver = false;
    public float roundTime = 30f;
    public TextMeshProUGUI timerText;
    public AudioSource song;
    IEnumerator GameTimer()
    {
        song.Play();
        startTile.GetComponent<ParticleSystem>().Play();
        endTile.GetComponent<ParticleSystem>().Play();
        FindObjectOfType<audioManager>().playLaugh();
        float timer = 0;
        while (timer<roundTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            timerText.text = ((int)(roundTime - timer)).ToString();
        }
        finalTile.GetComponent<movementTile>().rightTile = endTile;
        //trigger ending animation
        endTile.GetComponent<Image>().color = Color.white;
        startTile.GetComponent<Image>().color = Color.white;
        var spawners = FindObjectsOfType<carSpawner>();

        FindObjectOfType<audioManager>().playNextAreaSound();
        foreach (var spawn in spawners)
        {
            spawn.spawning = false;
        }
        reaper.SetTrigger("end");

        reaperTextbox.SetActive(true);
        StartCoroutine(disableAfterTime(reaperTextbox, 2f));

        reapertext.text = reaperEndSayings[Random.Range(0, reaperEndSayings.Length)];
        foreach (var panel in sidePanels)
        {
            panel.GetComponent<Animator>().SetTrigger("end");
            panel.GetComponent<ParticleSystem>().Stop();
        }
        StartCoroutine(fadeAudio(song));
        startTile.GetComponent<ParticleSystem>().Stop();
        endTile.GetComponent<ParticleSystem>().Stop();
    }
    IEnumerator fadeAudio(AudioSource a)
    {
        float timer = 0f;
        while (timer < 1f)
        {

        yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            song.volume = 1 - timer;
        }
        song.Stop();
    }
    public Color grass;
    public void loseGame()
    {
        reaperTextbox.SetActive(true);
        StartCoroutine(disableAfterTime(reaperTextbox, 3f));

        reapertext.text = reaperDeathSayings[Random.Range(0, reaperDeathSayings.Length)];

        player.GetComponent<player>().kill();
        var round = PlayerPrefs.GetInt("level", 0);
        nextLevelTimerLabel.text ="You Died \nTry Again";
        StopAllCoroutines();
        StartCoroutine(NextLevel());
        PlayerPrefs.SetInt("level", 0);
    }
    public bool gameIsOver;
    public void endGame()
    {
        if (gameIsOver)
            return;
        gameIsOver = true;
        int round = PlayerPrefs.GetInt("level", 0);
        round++;
        PlayerPrefs.SetInt("level", round);
        StartCoroutine(NextLevel());
        //you win!
        print("you win!");
    }
    float NextLevelCountdown = 5f;
    public TextMeshProUGUI nextLevelTimerText;
    public TextMeshProUGUI nextLevelTimerLabel;
    IEnumerator NextLevel()
    {
        nextLevelTimerLabel.gameObject.SetActive(true);
        nextLevelTimerText.gameObject.SetActive(true);
        nextLevelTimerText.text = NextLevelCountdown.ToString();
        float timer = 0;
        while (timer < NextLevelCountdown)
        {
            nextLevelTimerText.text = ((int)(NextLevelCountdown - timer)).ToString();
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            
        }
        Invoke("loadNext",1f);
        FindObjectOfType<fader>().fade(1);
    }
    public void loadNext()
    {
        SceneManager.LoadScene(0);

    }
}
