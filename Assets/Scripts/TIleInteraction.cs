using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TIleInteraction : MonoBehaviour
{
    public GameObject[] roots;
    public Image TreeGrowthChart;
    public Transform Tree;
    GameObject deckObjects;
    float round = 1;
    private TMP_Text countdown;
    private float loseTime = 5f;
    [HideInInspector]
    public int RootID;
    bool RefreshDeck;
    //   public MeshCollider outerPLane;

    [HideInInspector]
    public GameObject UsedCard;
    private void Start()
    {
        deckObjects = GameObject.Find("Deck");
        countdown = GameObject.Find("Timer").GetComponent<TMP_Text>();
        countdown.enabled = false;
    }
    void Update()
    {
        if (Holding)
        {
            countdown.enabled = true;
            loseTime -= Time.deltaTime;
            countdown.text = ("Place! " + loseTime);
            Placing();
        }
    }
    [HideInInspector]
    public bool Holding;
    GameObject rootToPlace;
    public void PickRoot()
    {
        if (!Holding)
        {
            rootToPlace = Instantiate(roots[RootID]);
            float CameraZDistance = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 ScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance);
            Vector3 NewPos = Camera.main.ScreenToWorldPoint(ScreenPos);
            rootToPlace.transform.position = NewPos;
            Holding = true;
        }
    }

    private void Placing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData) && hitData.transform.tag == "tile")
        {
            if (rootToPlace) {
                
                var rootTransform = rootToPlace.transform.position;
                var hitTransform = hitData.transform.position;
                rootToPlace.transform.position = Vector3.MoveTowards(rootTransform, hitTransform, 10f * Time.deltaTime);
                if (Input.GetKeyDown(KeyCode.Mouse0) && hitData.transform.GetComponent<tileState>().Occupied == false &&
                    rootToPlace.GetComponent<rootScript>().TouchesRoot)
                {
                    if(hitData.transform.Find("Water(Clone)") || hitData.transform.name == "WaterCube(Clone)")
                    {
                        float ToNextGrowth = 1f/round;
                        TreeGrowthChart.fillAmount+=ToNextGrowth;
                        if(TreeGrowthChart.fillAmount == 1)
                        {
                            RefreshDeck = true;
                            Tree.localScale = new Vector3(Tree.localScale.x + 1, Tree.localScale.y + 1, Tree.localScale.z + 1);
                            TreeGrowthChart.fillAmount = 0;
                            if (round < 4) { round++; }
                            TreeGrowthChart.GetComponentInChildren<TMP_Text>().text = round.ToString();
                        }
                        if(hitData.transform.name == "WaterCube(Clone)")
                        {
                            hitData.transform.GetComponent<WaterTileManager>().Dry();
                        }
                    }
                    rootToPlace.transform.position = hitTransform;
                    hitData.transform.GetComponent<tileState>().Occupied = true;
                    DeckController deck = GameObject.Find("Deck").GetComponent<DeckController>();
                    deck.CardList.Remove(UsedCard);
                    UsedCard.SetActive(false);
                    if (RefreshDeck) { newDeck(); }
                    Holding= false;
                    countdown.enabled = false;
                    loseTime = 5f;
                }
            }
        }
        else
        {
            if (rootToPlace)
            {
                float CameraZDistance = Camera.main.WorldToScreenPoint(transform.position).z;
                Vector3 ScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance);
                Vector3 NewPos = Camera.main.ScreenToWorldPoint(ScreenPos);
                rootToPlace.transform.position = NewPos;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rootToPlace.transform.Rotate(0, 90, 0); 
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Destroy(rootToPlace);
            Holding = false;
            countdown.enabled = false;
            loseTime = 5f;
        }
        
    }

    public void newDeck()
    {
        foreach (Transform Card in deckObjects.transform)
        {
            if (Card.gameObject.activeSelf == false)
            {
                Card.gameObject.SetActive(true);
                Card.GetComponent<CardScript>().ChangeCard();
            }
        }
        RefreshDeck = false;
    }
    /*
    IEnumerator CountSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            loseTime -= 1f;
        }
    }
    */
}
