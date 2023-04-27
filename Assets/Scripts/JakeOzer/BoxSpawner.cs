using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BoxSpawner : MonoBehaviour
{
    public event Action BoxSpawned;

    [System.Serializable]
    public class Timeline
    {
        public List<GameObject> boxPrefabs;
        public List<float> spawnTimes;
    }

    [SerializeField] private List<Timeline> timelines;
    public int currentTimelineIndex;
    public float spawnTimer;
    private BoxMover boxMover;

    public bool noBoxesLeft = false;
    public GameObject boxPrefab0;
    public GameObject boxPrefab1;
    public GameObject boxPrefab2;


    private void Start()
    {
        boxMover = FindObjectOfType<BoxMover>();
        currentTimelineIndex = 0;
        LoadCSV("Timelines.csv"); // Load timelines from CSV file
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

        noBoxesLeft =  boxMover.boxes.Count <= 0 && timelines[currentTimelineIndex].spawnTimes.Count <= 0;

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
        //invoke event
        BoxSpawned?.Invoke();
    }

    public void SetTimeline(int timelineIndex)
    {
        if (timelineIndex < timelines.Count)
        {
            currentTimelineIndex = timelineIndex;
        }
    }

    private void LoadCSV(string filePath)
    {
         int recordingCurrentTimelineIndex = 0;
            // Check if filePath is null or empty
    if (string.IsNullOrEmpty(filePath))
    {
        Debug.LogError("File path is null or empty!");
        return;
    }
        // Read the file
        string[] lines = File.ReadAllLines(filePath);

        // Parse each line
        foreach (string line in lines)
        {
            string[] values = line.Split(',');

            // Check if the box slot is empty
            if (string.IsNullOrEmpty(values[0]))
            {
                Debug.Log("Empty box slot");
                // Increment the timeline index
                recordingCurrentTimelineIndex++;
                continue;
            }

            // Create a new timeline if necessary
            if (timelines.Count <= recordingCurrentTimelineIndex)
            {
                timelines.Add(new Timeline());
            }

            // Add the box to the current timeline


           
            float spawnTime = float.Parse(values[1]);

                switch( values[0])
    {
        case "0":
            timelines[recordingCurrentTimelineIndex].boxPrefabs.Add(boxPrefab0);
            break;
        case "1":
            timelines[recordingCurrentTimelineIndex].boxPrefabs.Add(boxPrefab1);
            break;
        case "2":
            timelines[recordingCurrentTimelineIndex].boxPrefabs.Add(boxPrefab2);
            break;
        default:
            Debug.LogError("Invalid box index");
            return;
    }
            timelines[recordingCurrentTimelineIndex].spawnTimes.Add(spawnTime);
        }
    }
}
