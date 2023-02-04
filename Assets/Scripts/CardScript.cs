using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    TIleInteraction tileInteraction;
    public int CardContains;
    private void Start()
    {
        CardContains = Random.Range(0, 4);
        tileInteraction = GameObject.Find("GameController").GetComponent<TIleInteraction>();
    }
    public void Clicked()
    {
        if (!tileInteraction.Holding)
        {
            tileInteraction.RootID = CardContains;
            tileInteraction.UsedCard = gameObject;
        }
    }
}
