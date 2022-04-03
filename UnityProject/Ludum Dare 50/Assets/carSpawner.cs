using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        
    }
    public bool spawning = false;
    float spawnRate = 8f;
    public float minSpawnRate = 2f;
    public float maxSpawnRate = 8f;
    float timer = 0f;
    public int currentRound;
    public void setSpawn(int round)
    {
        currentRound = round;
        spawning = true;
        maxSpawnRate -= round * .5f;
        if (maxSpawnRate < 4f)
            maxSpawnRate = 4f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!spawning)
            return;
        if (timer > spawnRate)
        {
            spawnCar();
            timer = 0;
        }
        timer += Time.deltaTime;
    }
    public GameObject carPrefab;
    void spawnCar()
    {
        int coin = Random.Range(0, 2);
        if(coin == 0)
        {
            GameObject newCar = Instantiate(carPrefab, transform.GetChild(0).position, Quaternion.identity, transform.GetChild(0));
            newCar.GetComponent<car>().direction = -newCar.GetComponent<car>().direction;
            newCar.GetComponent<car>().setRound(currentRound);

        }
        else
        {
            GameObject newCar = Instantiate(carPrefab, transform.position, Quaternion.identity, transform);
            newCar.GetComponent<car>().setRound(currentRound);
        }
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }
}
