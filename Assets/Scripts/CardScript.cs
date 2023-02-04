using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    TIleInteraction tileInteraction;
    public List<Sprite> rootSprites;
    private Image buttonSprite;
    public int CardContains;
    private void Start()
    {
        buttonSprite = GetComponent<Image>();
        CardContains = Random.Range(0, 4);
        Debug.Log(rootSprites[CardContains]);
        buttonSprite.sprite = rootSprites[CardContains];
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

    public void ChangeCard()
    {
        CardContains = Random.Range(0, 4);
        buttonSprite.sprite = rootSprites[CardContains];
        tileInteraction = GameObject.Find("GameController").GetComponent<TIleInteraction>();
    }
}
