using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerGO;
    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - playerGO.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = playerGO.transform.position + offset;
    }
}
