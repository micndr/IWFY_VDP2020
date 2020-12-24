using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    private GlobalState _globalState;
    
    public enum Soundscape // your custom enumeration
    {
        MainMenu,
        WorldHub,
        World1,
        World2,
        World3,
        World4,
        World5
    }

    public Soundscape currentSoundscape;

    [SerializeField] private AudioMixer mixer;
    
    [SerializeField] private AudioMixerSnapshot[] snapshot;
    
    [SerializeField] private AudioSource[] ui;
    [SerializeField] private AudioSource[] ost;
    [SerializeField] private AudioSource[] ambient;
    [SerializeField] private AudioSource[] video;

    private void Start()
    {
        _globalState = FindObjectOfType<GlobalState>();
        Debug.Log("Selected soundscape: " + currentSoundscape);
        if (currentSoundscape == Soundscape.MainMenu) snapshot[2].TransitionTo(.01f);
        if (currentSoundscape == Soundscape.WorldHub) snapshot[1].TransitionTo(.01f);
        if (currentSoundscape == Soundscape.World4) snapshot[3].TransitionTo(1f);

        
        
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>() as AudioSource[];
        Debug.Log(allAudioSources.Length);
        
    }

    private void Update()
    {
        if (_globalState) mixer.SetFloat("MasterVolume", _globalState.globalVolume);

        if (video[0].isPlaying) snapshot[1].TransitionTo(.1f);
        else
        {
            if (currentSoundscape == Soundscape.World4)
            {
                snapshot[3].TransitionTo(1f);
            }
            else
            {
                snapshot[0].TransitionTo(1f);
            }
        }
    }

    public void PlayUI(int index)
    {
        ui[index].Play();
    }

    public void PlayFlowerOn()
    {
        ambient[0].Play();
    }
}
