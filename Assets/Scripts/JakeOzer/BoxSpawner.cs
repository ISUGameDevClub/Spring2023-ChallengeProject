using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Timeline
    {
        public List<GameObject> boxPrefabs;
        public List<float> spawnTimes;
    }

    [SerializeField] private List<Timeline> timelines;
    private int currentTimelineIndex;
    public float spawnTimer;
    private BoxMover boxMover;

    public bool noBoxesLeft = false;

    private void Start()
    {
        boxMover = FindObjectOfType<BoxMover>();
        currentTimelineIndex = 0;
    }

   private void Update()
{
    Timeline currentTimeline = timelines[currentTimelineIndex];

    spawnTimer += Time.deltaTime;

    // Check if there's a box to spawn
    int nextBoxIndex = GetNextBoxIndex(currentTimeline.spawnTimes);
    if (nextBoxIndex != -1 && spawnTimer >= currentTimeline.spawnTimes[nextBoxIndex])
    {
        // Spawn the next box
        SpawnBox(currentTimeline.boxPrefabs[nextBoxIndex]);

        // Remove the box from the timeline
        currentTimeline.spawnTimes.RemoveAt(nextBoxIndex);
        currentTimeline.boxPrefabs.RemoveAt(nextBoxIndex);
    }

    noBoxesLeft =  boxMover.boxes.Count != 0;

    //Debug.Log("Current Time: " + spawnTimer);
}

private int GetNextBoxIndex(List<float> spawnTimes)
{
    // Find the index of the next box to spawn
    for (int i = 0; i < spawnTimes.Count; i++)
    {
        if (spawnTimer >= spawnTimes[i])
        {
            return i;
        }
    }

    // No box to spawn
    return -1;
}


    private void SpawnBox(GameObject boxPrefab)
    {
        Box newBox = Instantiate(boxPrefab, transform.position, Quaternion.identity).GetComponent<Box>();
        boxMover.AddBoxToGameList(newBox);
    }

    public void SetTimeline(int timelineIndex)
    {
        if (timelineIndex < timelines.Count)
        {
            currentTimelineIndex = timelineIndex;
        }
    }
}
