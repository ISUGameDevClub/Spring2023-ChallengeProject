using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private float conveyorTime;
    private int conveyorIndex = 0;
    public float boxFill { get; private set; } = 0;
    public float boxFillMax { get; private set; } = 100;
    public float speed;

    public float value;


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
}
