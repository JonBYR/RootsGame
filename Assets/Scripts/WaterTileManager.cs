using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTileManager : MonoBehaviour
{
    public List<Transform> AdjacentWater;
    public GameObject sand;
    public void Dry()
    {
        foreach (Transform AdjacentTile in AdjacentWater)
        {
            Instantiate(sand, AdjacentTile.position, Quaternion.identity);
            AdjacentTile.gameObject.SetActive(false);
        }
        Instantiate(sand ,transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
