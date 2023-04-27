using UnityEngine;

public class BoxChecker : MonoBehaviour
{
    public MoneyManager moneyManager;

    BoxMover boxMover;

    void Start()
    {
        boxMover = FindObjectOfType<BoxMover>();
    }

    // Detect when a "box" enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
            Debug.Log("Box entered trigger");

        if (other.gameObject.CompareTag("Box"))
        {
            // Get the Box script attached to the box object
            Box box = other.gameObject.GetComponent<Box>();
            boxMover.boxes.Remove(box);
            // Check the fill status of the box
            float fill = box.boxFill;
            if (fill >= box.boxFillMax)
            {
                // Add the box's value to the money manager
                float value = box.value;
                moneyManager.addMoney(value);
            } else {
            Debug.Log("box too low ");
            }
            Destroy(other.gameObject);
        }
    }
}
