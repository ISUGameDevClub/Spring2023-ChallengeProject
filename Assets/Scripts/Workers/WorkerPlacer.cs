using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerPlacer : MonoBehaviour
{
    [SerializeField] private GameObject worker;
    [SerializeField] private GameObject ghostWorker;
    private GameObject tempWorker;
    private bool isPlacing = false;

    Vector2 mousePos;

    public void StartPlacing()
    {
        isPlacing = true;
        tempWorker = Instantiate(ghostWorker, mousePos, Quaternion.identity);
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (Input.GetMouseButtonDown(0) && isPlacing && hit.collider == null)
        {
            Destroy(tempWorker);
            Instantiate(worker, mousePos, Quaternion.identity);
            isPlacing = false;
        }
    }
}

//RAYCASTING NEEDED TO CHECK IF HITTING SOMETHING NOT INTENDED