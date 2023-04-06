using UnityEngine;

public class BoxChecker : MonoBehaviour
{
    public MoneyManager moneyManager;



    // Detect when a "box" enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
            Debug.Log("Box entered trigger");

        if (other.gameObject.CompareTag("Box"))
        {
            // Get the Box script attached to the box object
            Box box = other.gameObject.GetComponent<Box>();

            // Check the fill status of the box
            float fill = box.boxFill;
            if (fill >= box.boxFillMax)
            {
                // Add the box's value to the money manager
                float value = box.value;
                moneyManager.addMoney(value);

                // Reset the box's fill status
                Destroy(other.gameObject);
            } else {
            Destroy(other.gameObject);
            Debug.Log("box too low ");
            }

        }
    }
}
