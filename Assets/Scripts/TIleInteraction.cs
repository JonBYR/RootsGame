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
    private CameraMovement cameraMove;
    private Camera cam;
    public Image TreeGrowthChart;
    public Transform Tree;
    public Transform TreeView;
    int numberOfCards = 6;
    GameObject deckObjects;
    float round = 1;
    public float speed = 2f;
    private TMP_Text countdown;
    private float loseTime = 0f;
    [HideInInspector]
    public int RootID;
    bool RefreshDeck;
    //   public MeshCollider outerPLane;

    [HideInInspector]
    public GameObject UsedCard;
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cameraMove = GameObject.Find("Main Camera").GetComponent<CameraMovement>(); //use this to turn off camera movement when game ends
        deckObjects = GameObject.Find("Deck");
        countdown = GameObject.Find("Timer").GetComponent<TMP_Text>();
        TreeView = GameObject.Find("TreeView").GetComponent<Transform>();
        countdown.enabled = false; //this will be enabled as true when we reach the tree.
    }
    void Update()
    {
        if (Holding)
        {
            loseTime += Time.deltaTime;
            Placing();
        }
        if (numberOfCards == 0)
        {
            scrollBack();
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
                    if(hitData.transform.Find("Water(Clone)"))
                    {
                        Debug.Log("Works");
                        Debug.Log(1 / round);
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
                    }
                    rootToPlace.transform.position = hitTransform;
                    hitData.transform.GetComponent<tileState>().Occupied = true;
                    DeckController deck = GameObject.Find("Deck").GetComponent<DeckController>();
                    deck.CardList.Remove(UsedCard);
                    UsedCard.SetActive(false);
                    numberOfCards--;
                    if (RefreshDeck) { newDeck(); }
                    Holding= false;
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
        numberOfCards = 6;
    }
    private void scrollBack()
    {
        cam.transform.position = cameraMove.transform.position;
        cameraMove.enabled = false;
        cam.fieldOfView = 90f;
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, TreeView.position, speed * 0.01f);
        if (cam.transform.position == TreeView.position)
        {
            countdown.enabled = true;
            countdown.text = ("Congradulations! You just wasted " + Mathf.Round(loseTime) + " seconds of your life!");
        }
    }
}
