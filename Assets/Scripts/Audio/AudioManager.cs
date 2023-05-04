using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioTrackSource audioTrack;

    [SerializeField]
    private int[] trackthreashold;
    [SerializeField]
    private AudioSource roundWin;
    [SerializeField]
    private AudioSource roundLose;
    [SerializeField]
    private AudioSource roundStart;
    [SerializeField]
    private AudioSource boxPacked;
    [SerializeField]
    private AudioSource tutorial;
    [SerializeField]
    private AudioSource win;
    [SerializeField]
    private AudioSource lose;

    private int boxQuantity = 0;

    private static bool tutorialPlayerd = false;

    // Start is called before the first frame update
    void Start()
    {
        //tutorial
        if (!tutorialPlayerd) {
            tutorialPlayerd = true;
            tutorial.Play();
        }

        audioTrack = GetComponent<AudioTrackSource>();
        PlayTracks();

        FindObjectOfType<BoxSpawner>().BoxSpawned += OnBoxCreated;
        //FindObjectOfType<BoxHopper>().BoxSpawned += OnBoxCreated;

        GameManager gm = FindObjectOfType<GameManager>();

        gm.roundStart += () => roundStart.Play();
        gm.roundWin += () => roundWin.Play();
        gm.roundLose += () => roundLose.Play();
        gm.roundEnd += OnRoundEnd;

        FindObjectOfType<WorkerPlacer>().workerPlaced += OnWorkerPlaced;
        
    }

    private void OnWorkerPlaced(Worker worker) {
        Debug.Log("boxplaced event");
        worker.boxPacked += _ => boxPacked.Play();
    }

    void OnBoxCreated() {
        boxQuantity++;
        PlayTracks();
    }

    void OnRoundEnd() {
        boxQuantity = 0;
        ResetTracks();
        PlayTracks();
    }

    void PlayTracks() {
        for (int i = 0; i < trackthreashold.Length; i++) {
            if (boxQuantity >= trackthreashold[i]) {
                audioTrack.EnableTrack(i);
            }
        }
    }

    void ResetTracks() {
        for (int i = 0; i < trackthreashold.Length; i++) {
            audioTrack.DisableTrack(i);
        }
    }
}
