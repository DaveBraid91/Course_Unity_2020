using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
    private Rigidbody _enemyRb;
    private GameObject _playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _playerGoal = GameObject.Find("Player Goal");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (_playerGoal.transform.position - transform.position).normalized;
        _enemyRb.AddForce(lookDirection * speed);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal" || other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        } 
    }

}
