using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    [SerializeField] private List<Box> boxes;
    [SerializeField] private List<GameObject> conveyorList;
    private float conveyorTime;

    private int conveyorIndex = 0;
    [SerializeField] private float conveyorSpeed = 1f;

    void Awake()
    {
        boxes[0].transform.position = conveyorList[0].transform.position; //temporay use of first box until spawning is implemented 
        conveyorTime = Time.time;
    }

    private void FixedUpdate()
    {
        foreach (Box box in boxes)
        {
            float t = (Time.time - box.GetConveyorTime()) / conveyorSpeed;

            box.transform.position = Vector3.Lerp(conveyorList[box.GetConveyorIndex()].transform.position, conveyorList[box.GetConveyorIndex() + 1].transform.position, t);

            if (t >= 1)
            {
                box.NextConveyor();
            }
        }
    }
}
