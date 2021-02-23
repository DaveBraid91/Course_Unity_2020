using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField, Range(0, 180)]
    private float rotSpeed;
    private float hInput;

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotSpeed * hInput * Time.deltaTime);
    }
}
