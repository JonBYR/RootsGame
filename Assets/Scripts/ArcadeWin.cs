using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeWin : MonoBehaviour
{
    public TIleInteraction tile;
    public static bool collisionOn = true;
    private void Start()
    {
        if (!collisionOn) this.gameObject.GetComponent<BoxCollider>().enabled = false;
        else { this.gameObject.GetComponent<BoxCollider>().enabled = true; }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "tree")
        {
            Debug.Log("Collided");
            tile.scrollBack();
        }
    }
}
