using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField, Range(0, 180)]
    private float moveForce, powerUpForce, powerUpTime;
    private float vInput;
    private bool hasPowerUp = false;

    private Rigidbody _rb;
    private Transform _focalPointTF;
    private ParticleSystem _smokeParticleSystem;
    [SerializeField]
    private GameObject[] _powerUpIndicators;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _focalPointTF = GameObject.Find("FocalPoint").transform;
        _powerUpIndicators = GameObject.FindGameObjectsWithTag("PowerUpIndicator");
        _smokeParticleSystem = GameObject.Find("Smoke_Particle").GetComponent<ParticleSystem>();
        HidePowerUpIndicators();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vInput = Input.GetAxis("Vertical");
        _rb.AddForce(_focalPointTF.forward * vInput * moveForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);
        }
    }

    void HidePowerUpIndicators()
    {
        foreach(GameObject indicator in _powerUpIndicators)
        {
            indicator.SetActive(false);
        }
    }

    IEnumerator PowerUpCountdown()
    {
        foreach(GameObject indicator in _powerUpIndicators)
        {
            indicator.SetActive(true);
            yield return new WaitForSeconds(powerUpTime / _powerUpIndicators.Length);
            indicator.SetActive(false);
        }
        hasPowerUp = false;
    }
}
