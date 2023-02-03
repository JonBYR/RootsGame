using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    TIleInteraction tileInteraction;
    private void Start()
    {
        tileInteraction = GameObject.Find("GameController").GetComponent<TIleInteraction>();
    }
    public void Clicked()
    {
        if (!tileInteraction.Holding)
        {
            tileInteraction.UsedCard = gameObject;
        }
    }
}
