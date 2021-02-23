using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float hInput, vInput;
    [SerializeField, Range(0,20), Tooltip("Velocidad m�xima del coche")]
    private float speed;
    [SerializeField, Range(0, 180), Tooltip("Velocidad m�xima de giro del coche")]
    private float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        //Se mueve el veh�culo hacia adelante
        transform.Translate(Vector3.forward * speed * vInput * Time.deltaTime);
        transform.Rotate(Vector3.up * turnSpeed * hInput * Time.deltaTime);

    }
}