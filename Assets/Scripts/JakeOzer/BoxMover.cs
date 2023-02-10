using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    [SerializeField] private List<Box> boxes;
    [SerializeField] private List<GameObject> conveyorList;

    [SerializeField] private float conveyorSpeed = 1f;

    public void AddBoxToGameList(Box box)
    {
        this.boxes.Add(box);
        box.transform.position = conveyorList[0].transform.position;
    }

    private void FixedUpdate()
    {
        foreach (Box box in boxes)
        {
            float t = (Time.time - box.GetConveyorTime()) / conveyorSpeed;

            //Only lerp box if there is another conveyor to move to, else dont move box
            if (box.GetConveyorIndex() < (conveyorList.Count - 1))
            {
                box.transform.position = Vector3.Lerp(conveyorList[box.GetConveyorIndex()].transform.position, conveyorList[box.GetConveyorIndex() + 1].transform.position, t);
            }

            if (t >= 1)
            {
                box.NextConveyor();
            }
        }
    }
}
