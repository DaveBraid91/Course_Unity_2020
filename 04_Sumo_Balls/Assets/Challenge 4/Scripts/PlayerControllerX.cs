using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody _playerRb;
    [SerializeField]
    private float speed, boostForce;
    private GameObject _focalPoint;
    private ParticleSystem _smokeParticleSystem;
    [SerializeField]
    private GameObject[] _powerUpIndicators;

    private bool hasPowerUp, hasBoosted;
    [SerializeField, Range(1, 10)]
    private int powerUpDuration = 5, boostCooldown;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
        _powerUpIndicators = GameObject.FindGameObjectsWithTag("PowerUpIndicator");
        _smokeParticleSystem = GameObject.Find("Smoke_Particle").GetComponent<ParticleSystem>();
        HidePowerUpIndicators();
    }

    void FixedUpdate()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        _playerRb.AddForce(_focalPoint.transform.forward * verticalInput * speed); 

        if(Input.GetKeyDown(KeyCode.Space) && !hasBoosted)
        {
            _playerRb.AddForce(_focalPoint.transform.forward * boostForce, ForceMode.Impulse);
            _smokeParticleSystem.Play();
            hasBoosted = true;
            StartCoroutine(BoostCooldown());
        }
        // Set powerup indicator position to beneath player
        //powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            StartCoroutine(PowerUpCooldown());
        }
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position; 
           
            if (hasPowerUp) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerUpCooldown()
    {
        foreach (GameObject indicator in _powerUpIndicators)
        {
            indicator.SetActive(true);
            yield return new WaitForSeconds(powerUpDuration / _powerUpIndicators.Length);
            indicator.SetActive(false);
        }
        hasPowerUp = false;
    }

    IEnumerator BoostCooldown()
    {
        yield return new WaitForSeconds(boostCooldown);
        hasBoosted = false;
    }

    void HidePowerUpIndicators()
    {
        foreach (GameObject indicator in _powerUpIndicators)
        {
            indicator.SetActive(false);
        }
    }

}
