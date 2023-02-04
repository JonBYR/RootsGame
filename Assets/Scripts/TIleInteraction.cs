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
    private TMP_Text countdown;
    private float loseTime = 5f;
    //   public MeshCollider outerPLane;

    [HideInInspector]
    public GameObject UsedCard;
    private void Start()
    {
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
    public void PickRoot(int RootID)
    {
        if (!Holding)
        {
            rootToPlace = Instantiate(roots[RootID]);
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
                GameObject InGround = null;
                if (hitData.transform.childCount == 1) {InGround = hitData.transform.GetChild(0).gameObject; }
                rootToPlace.transform.position = Vector3.MoveTowards(rootTransform, hitTransform, 10f * Time.deltaTime);
                if (Input.GetKeyDown(KeyCode.Mouse0) && hitData.transform.GetComponent<tileState>().Occupied == false &&
                    rootToPlace.GetComponent<rootScript>().TouchesRoot && !InGround)
                {
                    rootToPlace.transform.position = hitTransform;
                    hitData.transform.GetComponent<tileState>().Occupied = true;
                    DeckController deck = GameObject.Find("Deck").GetComponent<DeckController>();
                    deck.CardList.Remove(UsedCard);
                    UsedCard.SetActive(false);                   
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
