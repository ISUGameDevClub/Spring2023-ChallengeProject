using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private float conveyorTime;
    private int conveyorIndex = 0;
    private float boxFill = 0;
    private float boxFillMax = 100;


    private void Start()
    {
        conveyorTime = Time.time;
    }

    public float GetConveyorTime() => conveyorTime;
    public int GetConveyorIndex() => conveyorIndex;
    public float GetBoxFill() => boxFill;

    public void NextConveyor()
    {
        conveyorIndex++;
        conveyorTime = Time.time;
    }

    public void packBox()
    {
       while (boxFill < boxFillMax)
        {
            Debug.Log("Box getting packed");
            boxFill++;
        }

        Destroy(gameObject);
    }
}
