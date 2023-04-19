using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public float sightRadius = 4f;
    public float packRate = 1f;

    public float packSpeed = 1f;
    public float moveDistance = 0.1f;
    public float moveSpeed = 10f;
    private bool isMoving = false;

    [SerializeField]
    public float cost { get; private set;}  
    public SpriteRenderer spriteRenderer; 

    Collider2D[] colliders;
    private LayerMask boxLayerMask;

    private void Start()
    {
        boxLayerMask = LayerMask.GetMask("Box");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void CheckForBoxes()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, sightRadius);

        foreach(Collider2D col in colliders)
        {
            if (col.TryGetComponent<Box>(out Box box))
            {
                col.gameObject.GetComponent<Box>().PackBox(packRate);
                MoveWorker();
            }
        }
    }

    private void MoveWorker()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine());
        }
    }

    public void StartPacking()
    {
        InvokeRepeating("CheckForBoxes", 0f, packSpeed); 
        //spriteRenderer.color = new Color(1f, 1f, 1f, 255f);
    }

 private IEnumerator MoveCoroutine()
{
    isMoving = true;

    float moveTimer = 0f;
    Vector3 startPos = transform.position;

    while (moveTimer < packSpeed / 4)
    {
        float newY = startPos.y + Mathf.Sin(moveTimer * moveSpeed) * moveDistance;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        moveTimer += Time.deltaTime;
        yield return null;
    }

    isMoving = false;
}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}
