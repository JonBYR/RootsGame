using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootScript : MonoBehaviour
{
    [HideInInspector]
    public bool TouchesRoot;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "root")
        {
            TouchesRoot = true;
        }
        else { TouchesRoot= false; }
    }

    private void OnCollisionExit(Collision collision)
    {
        TouchesRoot= false;
    }
}
