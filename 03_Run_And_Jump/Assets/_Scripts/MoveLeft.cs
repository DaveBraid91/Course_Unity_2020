using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField, Range(0, 30)]
    private float speed = 0;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if(speed < 5)
        {
            speed += Time.deltaTime;
        }
        else if(speed < 30)
        {
            speed += Time.deltaTime / 10;
        }

        if(!_playerController.GameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        
    }
}
