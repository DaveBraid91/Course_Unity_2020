using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField, Range(10, 20)]
    private float minForce, maxForce;
    [SerializeField, Range(-10, 10)]
    private float minTorque, maxTorque;
    [SerializeField, Range(-5, 4)]
    private float minPosition, maxPosition, ySpawnPos;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(RandomForce(), ForceMode.Impulse);
        _rb.AddTorque(RandomTorque());
        transform.position = RandomSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Generates a random Vector3 with up direction
    /// </summary>
    /// <returns>Random upwards force</returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }
    /// <summary>
    /// Generates a Random Vector3 between minTorque and maxTorque
    /// </summary>
    /// <returns>Random Vector3 between minTorque and maxTorque</returns>
    private Vector3 RandomTorque()
    {
        return new Vector3(
            Random.Range(minTorque, maxTorque), 
            Random.Range(minTorque, maxTorque), 
            Random.Range(minTorque, maxTorque));
    }
    /// <summary>
    /// Generates a Random spawn position
    /// </summary>
    /// <returns>Random spawnposition with z = 0</returns>
    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(minPosition, maxPosition), ySpawnPos, 0);
    }
}
