using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioTrackSource : MonoBehaviour
{
    [SerializeField]
    private AudioTrack[] audioTracks;
    [SerializeField]
    private float fadeDurration = 1f;
    [SerializeField]
    private AudioMixer musicMixer;

    private Coroutine[] fadeRoutines;

    private void Awake() {
        fadeRoutines = new Coroutine[audioTracks.Length];

        foreach (AudioTrack track in audioTracks)
        {
            track.Initialize(gameObject , musicMixer);
        }
    }

    public void EnableTrack( int trackIndex ) {
        if (trackIndex < 0 || trackIndex >= audioTracks.Length) 
        {
            Debug.LogError("Invalid track index");
            return;
        }

        SetVolume(trackIndex, audioTracks[trackIndex].MaxVolume);
    }

    public void DisableTrack(int trackIndex) {
        if (trackIndex < 0 || trackIndex >= audioTracks.Length)
        {
            Debug.LogError("Invalid track index");
            return;
        }

        SetVolume(trackIndex, 0f);
    }

    private void SetVolume(int trackIndex, float volume) {
        if (trackIndex < 0 || trackIndex >= audioTracks.Length)
        {
            Debug.LogError("Invalid track index");
            return;
        }

        if (fadeRoutines[trackIndex] != null)
        {
            StopCoroutine(fadeRoutines[trackIndex]);
            fadeRoutines[trackIndex] = null;
        }

        fadeRoutines[trackIndex] = StartCoroutine(audioTracks[trackIndex].FadeTrack(volume, fadeDurration));
    
        Debug.Log("SetVolume: " + trackIndex + " " + volume);
    }
}
