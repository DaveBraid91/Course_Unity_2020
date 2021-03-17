using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 cameraOffset;

    private void Start()
    {
        cameraOffset = new Vector3(0, 0, -Camera.main.transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = mousePosition + cameraOffset;
    }
}
