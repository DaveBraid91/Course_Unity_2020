using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController3D : MonoBehaviour
{
    private float hInput, vInput;
    [SerializeField, Range(0, 20), Tooltip("Velocidad del avi�n")]
    private float speed;
    [SerializeField, Range(0, 180), Tooltip("Velocidad m�xima de giro del avi�n")]
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

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        /* Por defecto el giro sale invertido. Se podr�a cambiar el signo a cualquiera de los elementos de la operaci�n.
         * Por l�gica, se podr�a elegir cambiar el vector de rotaci�n a "left" o poner un "-" a la velocidad.*/
        transform.Rotate(Vector3.right * -turnSpeed * vInput * Time.deltaTime);
        transform.Rotate(Vector3.up * turnSpeed * hInput * Time.deltaTime);
    }
}
