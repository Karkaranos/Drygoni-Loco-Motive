/*****************************************************************************
// File Name :         Sound.cs
// Author :            Cade R. Naylor
// Creation Date :     October 22, 2023
//
//Brackeys Audio Manager
//Tutorial video: https://youtu.be/6OT43pvUyfY

// Brief Description :  Creates the settings for audio sources in each sound in 
AudioManager
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    #region variables

    public string name;
    public AudioClip audClip;

    [Range(0, 1)]
    public float clipVolume;
    [Range(0.1f, 3)]
    public float clipPitch;
    public bool canLoop;

    [Range(-1, 1)]
    public float panStereo;
    [Range(0, 1)]
    public float spacialBlend;
    public int minSoundDistance;
    public int maxSoundDistance;

    [HideInInspector]
    public AudioSource source;

    #endregion
}
