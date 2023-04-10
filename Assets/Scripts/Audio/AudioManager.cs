using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioTrackSource audioTrack;

    [SerializeField]
    private int[] trackthreashold;

    private int boxQuantity = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioTrack = GetComponent<AudioTrackSource>();
        PlayTracks();

        FindObjectOfType<BoxSpawner>().OnBoxSpawned += OnBoxCreated;
    }

    void OnBoxCreated() {
        boxQuantity++;
        PlayTracks();
    }

    void PlayTracks() {
        for (int i = 0; i < trackthreashold.Length; i++) {
            if (boxQuantity >= trackthreashold[i]) {
                audioTrack.EnableTrack(i);
            }
        }
    }
}
