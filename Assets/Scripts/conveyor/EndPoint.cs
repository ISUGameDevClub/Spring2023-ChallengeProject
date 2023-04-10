using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{
    public float sightRadius = 4f;
    Collider2D[] colliders;
    private LayerMask boxLayerMask;

    [SerializeField] private float shipReward;
    [SerializeField] private float shipFail;
    [SerializeField] MoneyManager moneyManager;

    private void Start()
    {
        boxLayerMask = LayerMask.GetMask("Box");
    }

    private void Update()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, sightRadius);

        foreach (Collider2D col in colliders)
        {
            if (col.TryGetComponent<Box>(out Box box))
            {
                //Debug.Log("Collider of " + col.name + " touching the end");
                
                if (col.gameObject.GetComponent<Box>().IsPacked())
                {
                    moneyManager.addMoney(shipReward);
                    Destroy(col.gameObject);
                }
                else
                {
                    Debug.Log($"<color=purple>YOU LET AN UNPACKED BOX GET TO THE TRUCK. YOU'RE FIRED</color>");
                    moneyManager.subtractMoney(shipFail);
                    Destroy(col.gameObject);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }


}
