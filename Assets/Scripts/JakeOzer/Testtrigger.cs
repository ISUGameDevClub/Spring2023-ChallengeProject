using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testtrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    Debug.Log("Box entered trigger");
    if (other.gameObject.CompareTag("Box"))
    {
        // rest of the code...
    }
    }
}
