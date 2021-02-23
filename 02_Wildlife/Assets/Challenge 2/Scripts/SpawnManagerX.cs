using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;

    //private float startDelay = 1.0f;
    private float spawnInterval = 2.0f;
    private float minSpawnInterval = 2.0f;
    private float maxSpawnInterval = 4.0f;
    private float timer = 0;

    private int ballIndex;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnRandomBall", startDelay, spawnInterval);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnInterval)
        {
            SpawnRandomBall();
            Debug.Log("Han pasado " + timer + " segundos desde el último spawn.");
            timer = 0;
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }
    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall()
    {
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        // instantiate ball at random spawn location
        ballIndex = Random.Range(0, ballPrefabs.Length);
        Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);
    }

}
