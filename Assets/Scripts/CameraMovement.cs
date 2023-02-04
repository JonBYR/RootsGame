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
        transform.Translate(horizontal * 5*Time.deltaTime, vertical * 5*Time.deltaTime, 0);

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, 5, 24);
        clampedPos.z = Mathf.Clamp(clampedPos.z, 2.5f, 70);
        transform.position = clampedPos;
    }
}
