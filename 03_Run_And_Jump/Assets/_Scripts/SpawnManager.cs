using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstaclePrefabs;
    private Vector3 spawnPos;
    private int obstacleIndex;
    private float startDelay = 2;
    private float repeatDelay = 2;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        InvokeRepeating("SpawnObstacle", startDelay, repeatDelay);
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        if(!_playerController.GameOver)
        {
            obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);
        }
        
    }
}
