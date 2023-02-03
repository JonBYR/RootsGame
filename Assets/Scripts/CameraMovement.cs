using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(horizontal * 2*Time.deltaTime, vertical * 2*Time.deltaTime, 0);

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, -5, 5);
        clampedPos.z = Mathf.Clamp(clampedPos.z, -5, 5);
        transform.position = clampedPos;
    }
}
