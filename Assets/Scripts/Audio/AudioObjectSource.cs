using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObjectSource : MonoBehaviour
{
    [SerializeField]
    public AudioObjectScriptableObject audioObject;

    private int activeSourceIndex = -1;

    //cache
    private AudioSource[] audioSources;
    private bool loopAudio;
    private bool randomizeOrderPlayed;
    private bool avoidPlayingSameSoundInARow;
    private float volume;
    private bool varyVolume;
    private float volumeVariation;
    private float pitch;
    private bool varyPitch;
    private float pitchVariation;

    private float[] clipTimes;
    private Coroutine loopCoroutine;

    void Awake()
    {
        if (audioObject == null) 
        {
            Debug.LogError("AUDIO OBJECT NOT SET", gameObject);
            return;
        }

        //  Cache AudioObject stuff

        //bools
        loopAudio = audioObject.IsLoopAudio();
        randomizeOrderPlayed = audioObject.IsRangomOrderPlayed();
        avoidPlayingSameSoundInARow = audioObject.IsAvoidPlayingSameSoundInARow();

        //volume
        volume = audioObject.GetVolume();
        varyVolume = audioObject.IsVaryVolume();
        volumeVariation = audioObject.GetVolumeVariation();
        
        //pitch
        pitch = audioObject.GetPitch();
        varyPitch = audioObject.IsVaryPitch();
        pitchVariation = audioObject.GetPitchVariation();


        //  Generate Sources
        GenerateSources();

    }

    private void GenerateSources() {
        AudioClip[] audioClips = audioObject.GetAudioClips();
        audioSources = new AudioSource[ audioClips.Length ];
        clipTimes = new float[ audioClips.Length ];
        
        bool playOnAwake = audioObject.IsPlayOnAwake();

        int i = 0;
        foreach (AudioClip clip in audioClips)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = clip;
            newSource.volume = volume;
            newSource.pitch = pitch;

            newSource.playOnAwake = playOnAwake;
            //if (! playOnAwake ) newSource.Stop();

            newSource.hideFlags = HideFlags.HideInInspector;

            clipTimes[i] = clip.length;
            audioSources[i] = newSource;

            i += 1;
        }
    }

    public void Play() {
        Stop();

        //get a new source index
        if ( randomizeOrderPlayed )
        {
            if ( avoidPlayingSameSoundInARow && audioSources.Length > 1 )
            {
                int oldSourceIndex = activeSourceIndex;
                do 
                {
                    activeSourceIndex = Random.Range(0, audioSources.Length);
                } while ( activeSourceIndex == oldSourceIndex );
            }
            else
            {
                activeSourceIndex = Random.Range(0, audioSources.Length);
            }
        }
        else 
        {
            activeSourceIndex += 1;
            activeSourceIndex %= audioSources.Length;
        }
        
        //cache new source
        AudioSource sourceToPlay = audioSources[ activeSourceIndex ];

        //variations
        sourceToPlay.volume = varyVolume ? volume + Random.Range( - volumeVariation, volumeVariation ) : volume;
        sourceToPlay.pitch  = varyPitch  ? pitch  + Random.Range( - pitchVariation, pitchVariation )   : pitch;
        // if ( varyVolume )
        // {
        //     sourceToPlay.volume = volume + Random.Range( - volumeVariation, volumeVariation );
        // } else
        // {
        //     sourceToPlay.volume = volume;
        // }
        // if ( varyPitch )
        // {
        //     sourceToPlay.pitch = pitch + Random.Range( - pitchVariation, pitchVariation );
        // } else
        // {
        //     sourceToPlay.pitch = pitch;
        // }

        //play source
        sourceToPlay.Play();

        //loop coroutine
        if ( loopAudio ) loopCoroutine = StartCoroutine( Loop() );
    }

    public void Stop() {
        if (activeSourceIndex < 0) return;

        audioSources[activeSourceIndex].Stop();

        //end any looping coroutines
        if ( loopCoroutine != null ) 
        {
            StopCoroutine( loopCoroutine );
            loopCoroutine = null;
        }
    }

    // public void StopLoop() {
        
    // }

    private IEnumerator Loop() {
        yield return new WaitForSeconds( clipTimes[activeSourceIndex] );
        Play();
    }

    public void SetVolume( float newVolume ) {
        volume = newVolume;
        foreach (AudioSource s in audioSources)
        {
            s.volume = volume;
        }
    }


}
