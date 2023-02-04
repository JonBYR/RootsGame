using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAdjacent : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        WaterTileManager manager = transform.parent.GetComponent<WaterTileManager>();
        if (collision.transform.name == "WaterCube(Clone)" && !manager.AdjacentWater.Contains(transform))
        {
            manager.AdjacentWater.Add(collision.transform);
        }
    }
}
