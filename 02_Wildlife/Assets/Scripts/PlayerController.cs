using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 20)]
    private float speed;
    private float hInput;
    [SerializeField]
    private GameObject projectilePrefab;

    public float xRange = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento del personaje
        hInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * hInput * speed * Time.deltaTime);
        //Se limitan los movimientos con números fijos, porque es un Top-Down y no se va a mover la cámara.
        CheckInBounds();
        
        //Acciones del personaje
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Si se entra aquí hay que lanzar un proyectil
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
    /// <summary>
    /// Checks if the player is trying to move out of bounds and keeps him from doing it.
    /// </summary>
    public void CheckInBounds(/*int sign*/)
    {
        /*if (transform.position.x > sign * xRange)
            transform.position = new Vector3(sign * xRange, transform.position.y, transform.position.z);*/
        if (transform.position.x > xRange || transform.position.x < -xRange)
        {
            transform.position = transform.position.x < 0 ? new Vector3(-xRange, transform.position.y, transform.position.z) :
                                               new Vector3(xRange, transform.position.y, transform.position.z);
        }
            
    }
}
