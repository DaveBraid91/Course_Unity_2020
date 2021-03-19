using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem explosionParticles;
    //[SerializeField, Range(10, 20)]
    private float minForce = 12, maxForce = 17;
    //[SerializeField, Range(-10, 10)]
    private float minTorque = -10, maxTorque = 10;
    //[SerializeField, Range(-5, 4)]
    private float minPosition = -4, maxPosition = 4, ySpawnPos = -5;
    [SerializeField, Range(-100, 100)]
    private int scoreValue;
    private Rigidbody _rb;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(RandomForce(), ForceMode.Impulse);
        _rb.AddTorque(RandomTorque());
        _gameManager = FindObjectOfType<GameManager>();
        transform.position = RandomSpawnPosition();
    }

    
    private void OnMouseEnter()
    {
        if(_gameManager.gameState == GameManager.GameState.inGame)
        {
            _gameManager.UpdateScore(scoreValue);
            Destroy(gameObject);
            Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation);
        }
        
        /*if (gameObject.CompareTag("Bad"))
        {
            _gameManager.GameOver();
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("KillZone"))
        {
            if(gameObject.CompareTag("Good"))
            {
                //_gameManager.UpdateScore(-scoreValue);
                _gameManager.GameOver();
            }            
            Destroy(gameObject);
        }
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
