using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public float sightRadius = 4f;
    Collider2D[] boxesTouched;
    private LayerMask boxLayerMask;

    private void Start()
    {
        boxLayerMask = LayerMask.GetMask("Box");
    }

    private void Update()
    {
        boxesTouched = Physics2D.OverlapCircleAll(transform.position, sightRadius, boxLayerMask);
        


        foreach(Collider2D boxCol in boxesTouched)
        {
            Debug.Log("Collider of " + boxCol.name+" overlapping with circle") ;

            //boxCol.gameObject.GetComponent<Box>().packBox();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}
//do this with triggers i guess