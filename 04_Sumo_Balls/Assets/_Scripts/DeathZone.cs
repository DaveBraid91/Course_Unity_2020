using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("GAME OVER");
            SceneManager.LoadScene("Prototype 4");
        }
        else
        {
            Destroy(other.gameObject);
        }
        
    }
}
