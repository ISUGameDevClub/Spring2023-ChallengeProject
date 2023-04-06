using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BuildModeEnabler BME;
    public BoxSpawner boxSpawner;

    public int currentRound = 0;

    public bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying && boxSpawner.noBoxesLeft)
        {
            EndRound();
        }
    }

    public void StartRoundButton()
    {
        if(!isPlaying)
        {
            StartRound();
        }
    }

    void StartRound()
    {
        if(IsListOfListsEmpty(BME.minorGameObjectsList))
        {
            isPlaying = true;

            BME.isBuildMode = false;
            BME.isEarseMode = false;

            currentRound++;
            boxSpawner.SetTimeline(currentRound);
            boxSpawner.spawnTimer = 0;
        } else 
        {
            Debug.Log("failed" + BME.minorGameObjectsList.Count); 
        }

    }

    bool IsListOfListsEmpty<T>(List<List<T>> list)
{
    if (list == null || list.Count == 0)
    {
        return true;
    }

    foreach (var sublist in list)
    {
        if (sublist != null && sublist.Count > 0)
        {
            return false;
        }
    }

    return true;
}


    void EndRound()
    {
        isPlaying = false;

    }
}
