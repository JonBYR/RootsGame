using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public List<GameObject> CardList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform card in transform)
        {
            if(card.gameObject.activeSelf == true && !CardList.Contains(card.gameObject))
            {
                CardList.Add(card.gameObject);
            }
        }
    }
}
