using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

#region soundmapping

/* MAPPING OF THE SOUND INDEXES
 *
 * MAIN
 * 
 * WORLDHUB
 *
 * WORLD1
 *     - Ambient
 *         0 ->
 *         1 ->
 *
 * 
 */

#endregion

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
        if (!_globalState) { 
            GameObject obj = new GameObject();
            _globalState = obj.AddComponent<GlobalState>();
        }
        Debug.Log("Selected soundscape: " + currentSoundscape);
        if (currentSoundscape == Soundscape.MainMenu)
        {
            snapshot[2].TransitionTo(.01f);
            _globalState.OnVolumeChanged((float) (20.0 * Math.Log10(0.125)));    
        }
        if (currentSoundscape == Soundscape.WorldHub) snapshot[1].TransitionTo(.01f);
        if (currentSoundscape == Soundscape.World4) snapshot[3].TransitionTo(1f);

        
        
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>() as AudioSource[];
        Debug.Log(allAudioSources.Length);
        
    }

    private void Update()
    {
        if (_globalState) mixer.SetFloat("MasterVolume", _globalState.globalVolume);

        if (video.Length == 0) return; // Jacopo: just to get rid of the error
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

        if (currentSoundscape == Soundscape.World1)
        {
            // TODO only if sounds are not hearable over the ost
        }
    }

    public void PlayUI(int index)
    {
        ui[index].Play();
    }

    public void PlayOST(int index)
    {
        ost[index].Play();
    }

    public void PlayAmbient(int index)
    {
        ambient[index].Play();
    }
    
    public void StopAmbient(int index)
    {
        // TODO see if fade out is needed
        ambient[index].Stop();
    }

    public void PlayFlowerOn()
    {
        ambient[0].Play();
    }
}
