using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoxSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Timeline
    {
        public List<GameObject> boxPrefabs;
        public List<float> spawnTimes;
        public Timeline(string[] data)
        {
            boxPrefabs = new List<GameObject>();
            spawnTimes = new List<float>();

            for (int i = 1; i < data.Length; i += 2)
            {
                GameObject boxPrefab = Resources.Load<GameObject>(data[i]);
                boxPrefabs.Add(boxPrefab);
                float spawnTime = float.Parse(data[i + 1]);
                spawnTimes.Add(spawnTime);
            }
        }
    }

    [SerializeField] private List<Timeline> timelines;
    private int currentTimelineIndex;
    public float spawnTimer;
    private BoxMover boxMover;

    public bool noBoxesLeft = false;

    public string csvFilePath;

    private void Start()
    {
        boxMover = FindObjectOfType<BoxMover>();
        currentTimelineIndex = 0;

        timelines = ParseCSVFile(csvFilePath);
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
            SpawnBox(currentTimeline.boxPrefabs[nextBoxIndex].name);

            // Remove the box from the timeline
            currentTimeline.spawnTimes.RemoveAt(nextBoxIndex);
            currentTimeline.boxPrefabs.RemoveAt(nextBoxIndex);
        }

        noBoxesLeft = boxMover.boxes.Count != 0;

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

    private void SpawnBox(string boxPrefabName)
    {
        GameObject boxPrefab = Resources.Load<GameObject>(boxPrefabName);
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