using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawn : MonoBehaviour
{
    public List<GameObject> spawnables = new List<GameObject>();
    private bool occupied = false;
    // Start is called before the first frame update
    void Start()
    {
        float chance = Random.Range(0f, 1f);
        if (chance >= 0.9f && !occupied)
        {
            Instantiate(spawnables[0], this.transform.localPosition + new Vector3(Random.Range(-0.5f, 0.5f), 0.05f, Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            occupied = true;
        }
        else if (chance <= 0.3f && !occupied)
        {
            Instantiate(spawnables[1], this.transform.localPosition + new Vector3(Random.Range(-0.5f, 0.5f), 0.05f, Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            occupied = true;
        }
    }

    
}
