using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = _target.position;
    }

    private void OnEnable()
    {
        transform.position = _target.position;
    }
}
