using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TIleInteraction : MonoBehaviour
{
    public GameObject[] roots;
    //   public MeshCollider outerPLane;

    [HideInInspector]
    public GameObject UsedCard;
    void Update()
    {
        if (Holding)
        {
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
                rootToPlace.transform.position = Vector3.MoveTowards(rootTransform, hitTransform, 10f * Time.deltaTime);
                if (Input.GetKeyDown(KeyCode.Mouse0) && hitData.transform.GetComponent<tileState>().Occupied == false &&
                    rootToPlace.GetComponent<rootScript>().TouchesRoot)
                {
                    rootToPlace.transform.position = hitTransform;
                    hitData.transform.GetComponent<tileState>().Occupied = true;
                    DeckController deck = GameObject.Find("Deck").GetComponent<DeckController>();
                    deck.CardList.Remove(UsedCard);
                    UsedCard.SetActive(false);                   
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
}
