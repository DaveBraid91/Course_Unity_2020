using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DestroyOutOfBounds : MonoBehaviour
{
    private GameObject _ground;
    private float outOfBoundsPosX;
    // Start is called before the first frame update
    void Start()
    {
        _ground = GameObject.FindGameObjectWithTag("Ground");
        outOfBoundsPosX = _ground.GetComponent<BoxCollider>().bounds.min.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < outOfBoundsPosX)
        {
            Destroy(gameObject);
        }
    }
}
