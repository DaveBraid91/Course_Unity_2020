using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField, Range(0, 180)]
    private float moveForce;

    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 lookDirection = _player.transform.position - transform.position;
        lookDirection.Normalize();
        _rigidbody.AddForce(lookDirection * moveForce);
    }
}
