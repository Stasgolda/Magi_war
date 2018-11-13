using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    public List<AudioClip> _audioClips;

    private Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();
    private AudioSource[] _audio;

	// Use this for initialization
	void Awake () {
       
        _audio = GetComponents<AudioSource>();
        foreach (AudioClip clip in _audioClips)
        {
            _clips.Add(clip.name, clip);
        }
	}
	
    public void PlayAudio(string name , bool loop)
    {
        foreach (AudioSource audio in _audio)
        {
            if (!audio.isPlaying)
            {
                audio.clip = _clips[name];
                audio.loop = loop;
                audio.Play();
                return;
            }
        }
    }

    public void StopAudio()
    {
        foreach (AudioSource audio in _audio)
        {
            if (audio.isPlaying)
            {
                audio.Stop();
            }
        } 
    }
}
