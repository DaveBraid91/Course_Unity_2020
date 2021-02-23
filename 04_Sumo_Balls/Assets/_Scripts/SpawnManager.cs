using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab, powerUpPrefab;
    private float spawnRange = 9;
    [SerializeField]
    private int enemyCount, enemyWave;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enemyWave);
    }

    private void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount == 0)
        {
            enemyWave++;
            SpawnEnemyWave(enemyWave);
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        }
    }

    /// <summary>
    /// Generates a random spawn position in the game zone
    /// </summary>
    /// <returns>The generated position</returns>
    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange + 1);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange + 1);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    /// <summary>
    /// Spawns a certain number of enemies
    /// </summary>
    /// <param name="numberOfEnemies">Number of enemies to spawn</param>
    void SpawnEnemyWave(int numberOfEnemies)
    {
        for(int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
