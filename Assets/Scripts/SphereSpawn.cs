using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawn : MonoBehaviour
{
    public List<GameObject> spawnables = new List<GameObject>();
    private bool occupied = false;
    tileState state;
    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<tileState>();
        var pos = transform.position;
        float chance = Random.Range(0f, 100f);
        if (chance <= 20f && !occupied)
        {        
            var inst = Instantiate(spawnables[0], new Vector3(pos.x+ -.25f, .05f, pos.z+ .25f), Quaternion.identity);
            inst.transform.parent = this.transform;
            occupied = true;
            state.Occupied= true;
        }
        else if (chance > 20f && chance <= 45f && !occupied)
        {
            var inst = Instantiate(spawnables[1], new Vector3(pos.x, .05f, pos.z), Quaternion.identity);
            inst.transform.parent = this.transform;
            occupied = true;
        }
    }

    
}
