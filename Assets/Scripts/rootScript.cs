using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootScript : MonoBehaviour
{
    public bool TouchesRoot;
    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag == "root")
        {
            TouchesRoot = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        TouchesRoot= false;
    }
}
