using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerPlacer : MonoBehaviour
{
    [SerializeField] private GameObject worker;
    [SerializeField] private GameObject ghostWorker;
    private UIBottomPanelController bottomPanelController;
    private GameObject tempWorker;
    private bool isPlacing = false;

    Vector2 mousePos;

    private void Start()
    {
        bottomPanelController = FindObjectOfType<UIBottomPanelController>();
    }

    public void StartPlacing()
    {
        isPlacing = true;
        tempWorker = Instantiate(ghostWorker, mousePos, Quaternion.identity);
        bottomPanelController.Toggle();
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
            bottomPanelController.Toggle();
        }
    }
}

//maybe need to make it so when placing, the ui slider goes down