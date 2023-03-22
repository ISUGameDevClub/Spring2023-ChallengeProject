//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AudioObject")]
public class AudioObjectScriptableObject : ScriptableObject
{
	[SerializeField]
    private AudioClip[] audioClips;

	[SerializeField]
	private bool loopAudio = false;

	[SerializeField]
	private bool randomizeOrderPlayed = false;

	[SerializeField]
	private bool avoidPlayingSameSoundInARow = false;

	[SerializeField]
	private bool playOnAwake = false;

	[SerializeField] [Range(0f, 2f)]
	private float volume = 1f;

	[SerializeField] [Range(-3f, 3f)]
	private float pitch = 1f;

	[SerializeField]
	private bool varyVolume = false;

	[SerializeField] [Range(0f, 1f)]
	private float volumeVariation = 0f; 

	[SerializeField]
	private bool varyPitch = false;

	[SerializeField] [Range(0f, 2f)]
	private float pitchVariation = 0f;

	// getters (gottem)
	public AudioClip[] 	GetAudioClips() => audioClips;
	public bool IsLoopAudio() => loopAudio;
	public bool IsRangomOrderPlayed() => randomizeOrderPlayed;
	public bool IsAvoidPlayingSameSoundInARow() => avoidPlayingSameSoundInARow;
	public bool IsPlayOnAwake() => playOnAwake;
	public float GetVolume() => volume;
	public float GetPitch() => pitch;
	public bool IsVaryVolume() => varyVolume;
	public float GetVolumeVariation() => volumeVariation;
	public bool IsVaryPitch() => varyPitch;
	public float GetPitchVariation() => pitchVariation; 
}
