using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Worker : MonoBehaviour
{
    //events
    public event Action<Box> boxPacked;


    public float sightRadius = 4f;
    public float packRate = 1f;

    public float packSpeed = 1f;
    public float moveDistance = 0.1f;
    public float moveSpeed = 10f;
    private bool isMoving = false;
    public bool isMouseOver = false;


    [SerializeField]
    public float cost;
    public SpriteRenderer spriteRenderer; 

    public SpriteRenderer keepTrackSpriteRenderer; 

    Collider2D[] colliders;
    private LayerMask boxLayerMask;
    [SerializeField]
    public KeepTrack keepTrack;

    public GameObject selfPrefab;


    private void Start()
    {
        boxLayerMask = LayerMask.GetMask("Box");
        keepTrackSpriteRenderer = keepTrack.GetComponent<SpriteRenderer>();
    }

        private void Update()
    {
        // Adjust the transparency level of the sprite renderer based on whether the mouse is over the object
        if (isMouseOver)
        {
            // Make the object slightly visible (alpha value of 0.5)
            keepTrackSpriteRenderer.color = new Color(keepTrackSpriteRenderer.color.r, keepTrackSpriteRenderer.color.g, keepTrackSpriteRenderer.color.b, 0.3f);
        }
        else
        {
            // Make the object fully transparent (alpha value of 0)
            keepTrackSpriteRenderer.color = new Color(keepTrackSpriteRenderer.color.r, keepTrackSpriteRenderer.color.g, keepTrackSpriteRenderer.color.b, 0.0f);
        }
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }

    private void CheckForBoxes()
    {
            if (keepTrack.boxObjects.Count > 0)
            {
                Box box = keepTrack.boxObjects[0].GetComponent<Box>();
                box.PackBox(packRate);
                if (box.boxFill >= box.boxFillMax)
                {
                    //call event
                    boxPacked?.Invoke(box);
                    
                    keepTrack.boxObjects.Remove(keepTrack.boxObjects[0]);
                }
                MoveWorker();
                GetComponent<PeeMeter>().enabled = true;
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
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        GetComponent<BoxCollider2D>().enabled = true;
        keepTrack.gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

 private IEnumerator MoveCoroutine()
{
    isMoving = true;

    float moveTimer = 0f;
    Vector3 startPos = transform.position;

    while (moveTimer < moveSpeed / 4)
    {
        float newY = startPos.y + Mathf.Sin(moveTimer * moveSpeed) * moveDistance;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        moveTimer += Time.deltaTime;
        yield return null;
    }

    isMoving = false;
    transform.position = startPos;
}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
    
}
