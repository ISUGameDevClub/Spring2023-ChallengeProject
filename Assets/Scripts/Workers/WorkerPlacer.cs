using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> worker = new List<GameObject>();

    private GameObject tempWorker;
    private bool isPlacing = false;

    Vector2 mousePos;

    private MoneyManager moneyManager;

    private void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    public void StartPlacing(int value)
    {
        isPlacing = true;
        tempWorker = Instantiate(worker[value], mousePos, Quaternion.identity);
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (tempWorker != null) tempWorker.transform.position = mousePos;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (Input.GetMouseButtonDown(0) && isPlacing && hit.collider == null)
        {
            tempWorker.GetComponent<Worker>().StartPacking();
            moneyManager.subtractMoney(tempWorker.GetComponent<Worker>().cost);
            isPlacing = false;
            tempWorker = null;
        }
    }
}

//RAYCASTING NEEDED TO CHECK IF HITTING SOMETHING NOT INTENDED