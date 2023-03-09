using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWorker : MonoBehaviour
{
    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.gameObject.transform.position = mousePosition;
    }
}
