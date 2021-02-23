using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    private int animalIndex;

    private float spawnRangeX = 14;
    private float spawnPosZ;
    [SerializeField, Range(2,5)]
    private float startDelay = 2;
    [SerializeField, Range(0.5f, 3)]
    private float spawnInterval;
    private void Start()
    {
        animalIndex = Random.Range(0, enemies.Length);
        spawnPosZ = transform.position.z;
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    private void SpawnRandomAnimal()
    {
        Instantiate(enemies[animalIndex], new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ), enemies[animalIndex].transform.rotation);
        animalIndex = Random.Range(0, enemies.Length);
    }
}
