﻿using System;
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

        if (currentSoundscape == Soundscape.WorldHub)
        {
            snapshot[1].TransitionTo(.01f);
            StartCoroutine("DelayOST");
        }
        
        if (currentSoundscape == Soundscape.World1)
        {
            ambient[0].volume = (float) Math.Pow(10, _globalState.globalVolume / 20)-0.8f;
        }

        if (currentSoundscape == Soundscape.World4)
        {
            snapshot[3].TransitionTo(1f);
            for (int i = 13; i <= 18; i++)
            {
                float newVol = (float) Math.Pow(10, _globalState.globalVolume / 20)-1.11f;
                //Debug.Log("Step " + i + " set to: " + newVol);
                ambient[i].volume = newVol;
            }
        }

        
        
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>() as AudioSource[];
        Debug.Log(allAudioSources.Length);
        
    }
    
    private IEnumerator DelayOST()
    {
        yield return new WaitForSeconds(0.5f);
        ost[0].Play();
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
        if (index < ambient.Length)
        {
            if (ambient[index] != null) ambient[index].Play();
        }
    }
    
    // TODO
    public void PlayAmbientPro(int index, float volume, float pitch)
    {
        ambient[index].Play();
        ambient[index].volume = volume;
        ambient[index].pitch = pitch;
    }
    
    public void StopAmbient(int index)
    {
        // TODO see if fade out is needed
        ambient[index].Stop();
    }

    public void StopAmbient(int index, int time)
    {
        IEnumerator fadeAmbient = FadeOut(ambient[index], time);
        StartCoroutine (fadeAmbient);   
    }

    public void StopOstForSceneChange()
    {
        IEnumerator fadeOST = FadeOut(ost[0], 0.25f);
        StartCoroutine (fadeOST);   
    }

    public void PlayFlowerOn()
    {
        ambient[0].Play();
    }

    // USAGE
    // IEnumerator fadeOST = FadeOut(ost[0], 0.5f);
    // StartCoroutine (fadeOST);   
    private IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }
}
