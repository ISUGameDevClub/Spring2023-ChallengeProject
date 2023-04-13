using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    private float conveyorTime;
    private int conveyorIndex = 0;
    private float boxFill = 0;
    private float boxFillMax = 100;
    [SerializeField] private FillBar fillBar;


    private void Start()
    {
        conveyorTime = Time.time;
    }

    public float GetConveyorTime() => conveyorTime;
    public int GetConveyorIndex() => conveyorIndex;
    public float GetBoxFill() => boxFill;
    public float GetBoxFillMax() => boxFillMax;

    public void NextConveyor()
    {
        conveyorIndex++;
        conveyorTime = Time.time;
    }

    public void PackBox(float amount)
    {
        fillBar.transform.GetChild(0).gameObject.SetActive(true);
        fillBar.transform.GetChild(1).gameObject.SetActive(true);

        boxFill += amount;
        fillBar.FillBoxBar(boxFill);
        if (boxFill >= boxFillMax)
        {
            Debug.Log("Box is filled.");
        }

    }

}
