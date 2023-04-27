using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorRotator : MonoBehaviour
{
    private GameObject boxMover;
    private List<GameObject> conveyorList;
    [SerializeField]
    private int rotation;
    [SerializeField]
    private int index;
    private Vector2 thisPos;
    private Vector2 nextPos;

    private float xDifference;
    private float yDifference;



    // Start is called before the first frame update
    void Start()
    {
        boxMover = GameObject.FindGameObjectWithTag("BoxMover");
        conveyorList = boxMover.GetComponent<BoxMover>().conveyorList;
        rotation = 0;
        index = conveyorList.IndexOf(gameObject);
        thisPos = gameObject.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        if (conveyorList[index + 1] != null)
        {
            index = conveyorList.IndexOf(gameObject);
            nextPos = conveyorList[index + 1].transform.position;

            xDifference = thisPos.x - nextPos.x;
            yDifference = thisPos.y - nextPos.y;

            if (xDifference <= -0.5)
            {
                rotation = 180;
            }

            else if (xDifference >= 0.5)
            {
                rotation = 0;
            }

            else if (yDifference <= -0.5)
            {
                rotation = -90;
            }

            else if (yDifference >= 0.5)
            {
                rotation = 90;
            }
        }
        else if (conveyorList[index - 1] != null)
        {
            rotation = conveyorList[index - 1].gameObject.GetComponent<ConveyorRotator>().rotation;
        }

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, rotation));

    }
}
