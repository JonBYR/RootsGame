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
        var pos = transform.position;
        float chance = Random.Range(0f, 100f);
        if (chance <= 10f && !occupied)
        {        
            var inst = Instantiate(spawnables[0], new Vector3(pos.x+ -.25f, .05f, pos.z+ .25f), Quaternion.identity);
            inst.transform.parent = this.transform;
            occupied = true;
        }
        else if (chance > 10f && chance <= 20f && !occupied)
        {
            var inst = Instantiate(spawnables[1], new Vector3(pos.x, .05f, pos.z), Quaternion.identity);
            inst.transform.parent = this.transform;
            occupied = true;
        }
    }

    
}
