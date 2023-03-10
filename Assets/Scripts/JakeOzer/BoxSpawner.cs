using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject boxPrefab;
    private float spawnTimer;
    private BoxMover boxMover;

    private void Start()
    {
        boxMover = FindObjectOfType<BoxMover>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnRate)
        {
            SpawnBox();
            spawnTimer = 0;
        }
    }

    private void SpawnBox()
    {
        //NOTE: the box spawn pos will default to first conveyor location upon instantiation
        Box newBox = Instantiate(boxPrefab, transform.position, Quaternion.identity).GetComponent<Box>();
        boxMover.AddBoxToGameList(newBox);
    }
}
