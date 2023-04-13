using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoxHopper : MonoBehaviour
{
    public event Action BoxSpawned;

    [System.Serializable]
    internal class Wave
    {
        public float time;
        public int amount;
    }

    // [System.Serializable]
    // private class Round
    // {
    //     private Wave[] waves;
    // }

    [SerializeField]
    private float hopperDelay = 1.5f;
    [SerializeField]
    private GameObject boxPrefab;

    [SerializeField]
    private Wave[] waves;
    public int numWaves => waves.Length;

    private BoxMover boxMover;
    private Coroutine hopperCoroutineCache;
    private int hopper = 0;

    private void Awake() {
        boxMover = FindObjectOfType<BoxMover>();
        // StartRound();
    }

    public void StartRound(){
        StartCoroutine(roundCoroutine());
    }

    private IEnumerator roundCoroutine() {
        for (int i = 0; i < waves.Length; i++)
        {
            FeedHopper(waves[i].amount);
            yield return new WaitForSeconds(waves[i].time);
        }
    }

    private void FeedHopper(int amount) {
        if ( hopper <=0 ) 
        {
            hopper = amount;
            hopperCoroutineCache = StartCoroutine(hopperCoroutine());
        } else 
        {
            hopper += amount;
        }
    }

    private IEnumerator hopperCoroutine() {
        while (hopper > 0)
        {
            SpawnBox();
            hopper--;
            yield return new WaitForSeconds(hopperDelay);
        }
    }

    private void SpawnBox() {
        Box newBox = Instantiate(boxPrefab, transform.position, Quaternion.identity).GetComponent<Box>();
        boxMover.AddBoxToGameList(newBox);
        //invoke event
        BoxSpawned?.Invoke();
    }


}
