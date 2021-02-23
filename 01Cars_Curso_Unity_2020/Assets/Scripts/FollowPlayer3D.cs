using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer3D : MonoBehaviour
{
    public GameObject playerGO;
    [SerializeField]
    private Vector3 offset;
    private Vector3 planePreviousPos = Vector3.zero;
    private float movement;

    private void Start()
    {
        movement = (transform.position - playerGO.transform.position).magnitude;
    }

    private void LateUpdate()
    {
        offset = playerGO.transform.position - planePreviousPos;
        offset.Normalize();
        if( offset.magnitude > 0.5f)
        {
            transform.position = playerGO.transform.position - offset * movement;
        }
        
        transform.LookAt(playerGO.transform.position);
        planePreviousPos = playerGO.transform.position;
    }
}
