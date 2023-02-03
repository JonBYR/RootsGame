using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TIleInteraction : MonoBehaviour
{
    public GameObject[] roots;
    public MeshCollider outerPLane;
    void Update()
    {
        if (Holding)
        {
            Placing();
        }
    }

    private bool Holding;
    GameObject rootToPlace;
    public void PickRoot(int RootID)
    {
        rootToPlace = Instantiate(roots[RootID]);
        Holding = true;
    }

    private void Placing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData) && hitData.transform.tag == "tile")
        {
            if (rootToPlace) { rootToPlace.transform.position = hitData.transform.position; }
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
    }
}
