using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private float conveyorTime;
    private int conveyorIndex = 0;

    public float GetConveyorTime() => conveyorTime;
    public int GetConveyorIndex() => conveyorIndex;

    public void NextConveyor()
    {
        conveyorIndex++;
        conveyorTime = Time.time;
    }
}
