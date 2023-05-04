using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioTrack
{
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private float maxVolume = 1f;
    //public audioModu

    private AudioSource source;

    // public AudioClip Clip => clip;
    // public AudioSource Source => source;
    public float MaxVolume => maxVolume;

    public void Initialize( GameObject hostObject) {
        if ( source != null) {
            Debug.LogError("AudioTrack already initialized");
            return;
        }

        source = hostObject.AddComponent<AudioSource>();
        source.hideFlags = HideFlags.HideInInspector;
        source.clip = clip;
        source.volume = 0f;
        source.loop = true;

        source.Play();
    }

    public IEnumerator FadeTrack( float targetVolume, float duration ) {
        float startVolume = source.volume;
        float time = 0f;

        while ( time < duration ) {
            time += Time.deltaTime;
            float volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            source.volume = volume;
            yield return null;
        }

        source.volume = targetVolume;
    }
}
