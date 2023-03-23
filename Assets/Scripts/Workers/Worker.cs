using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public float sightRadius = 4f;
    public float packRate = 1f;
    Collider2D[] colliders;
    private LayerMask boxLayerMask;

    private void Start()
    {
        boxLayerMask = LayerMask.GetMask("Box");
    }

    private void Update()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, sightRadius);
        


        foreach(Collider2D col in colliders)
        {
            if (col.TryGetComponent<Box>(out Box box))
            {
                Debug.Log("Collider of " + col.name + " overlapping with circle");
                col.gameObject.GetComponent<Box>().PackBox(Time.deltaTime * packRate);
            }


            
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}