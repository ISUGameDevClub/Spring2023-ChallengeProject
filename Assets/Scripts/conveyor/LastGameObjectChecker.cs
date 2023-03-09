using UnityEngine;
using System.Collections.Generic;

public class LastGameObjectChecker : MonoBehaviour
{
    public bool canBeDestroyed;

    public List<GameObject> gameObjectsList; // The list of game objects to check
    public bool isFristGameObject; // A flag to indicate whether this game object is the last one in the list
    public bool isLastGameObject; // A flag to indicate whether this game object is the last one in the list
    public GameObject boxMover;

    public bool endPoint;

    public List<GameObject> minorList;
    public int minorListNumber = -1;

    void Start()
    {
        boxMover = GameObject.FindGameObjectWithTag("BoxMover");
        gameObjectsList = boxMover.GetComponent<BoxMover>().conveyorList;
    }

    private void Update()
    {


        // Get a reference to the game object's index in the list

        // Check if the game object is the last one in the list

        if (minorListNumber != -1)
        {
            int index = minorList.IndexOf(gameObject);
            if (index == -1)
            {

            }
            else if (index == 0)
            {
                isLastGameObject = true;
                GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1f);
            }
            else if (index == minorList.Count - 1)
            {
                isLastGameObject = true;
                GetComponent<SpriteRenderer>().color = new Color(.9f, 0.4f, 1f, 1f);
            }
            else
            {
                isLastGameObject = false;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }

        }
        else
        {
            int index = gameObjectsList.IndexOf(gameObject);

            if (index == -1)
            {

            }
            else if (index == gameObjectsList.Count - 1)
            {
                isLastGameObject = true;
                GetComponent<SpriteRenderer>().color = new Color(.9f, 0.4f, 1f, 1f);
            }
            else
            {
                isLastGameObject = false;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }


    }
}
