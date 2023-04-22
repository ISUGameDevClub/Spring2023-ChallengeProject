using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTrack : MonoBehaviour
{
    public List<GameObject> boxObjects = new List<GameObject>();


    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Box") && !boxObjects.Contains(col.gameObject) && col.gameObject.GetComponent<Box>().boxFill <= col.gameObject.GetComponent<Box>().boxFillMax) {
            boxObjects.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (boxObjects.Contains(col.gameObject)) {
            boxObjects.Remove(col.gameObject);
        }
    }
}