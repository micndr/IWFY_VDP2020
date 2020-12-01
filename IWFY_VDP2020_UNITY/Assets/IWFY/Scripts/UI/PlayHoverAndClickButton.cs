using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHoverAndClickButton : MonoBehaviour
{
    [SerializeField] private AudioSource[] uiSounds;


    public void PlayHover()
    {
        Debug.Log("HOVERRRRRRR");
        uiSounds[0].Play();
    }

    public void PlayClick()
    {
        uiSounds[1].Play();
    }
}
