/*****************************************************************************
// File Name :         AudioManager.cs
// Author :            Cade R. Naylor
// Creation Date :     October 14, 2023
//
//Brackeys Audio Manager
//Tutorial video: https://youtu.be/6OT43pvUyfY

// Brief Description :  Creates an array of all sound effects and music then adds an
audio source to each of them. Can call sound by using the name of the audio in the
inspector
*****************************************************************************/
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public AudioMixerGroup masterMixer;
    public float musicVolume;

    private bool inInterrogation = false;
    private bool gameStarted = false;

    private int footstepTrack;

    private string previousTrack;

    public Texture2D glassTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    /// <summary>
    /// Start is called before the first frame update. It ensures only one instance
    /// of this script and initializes Sound class
    /// </summary>
    void Start()
    {
        int numAM = FindObjectsOfType<AudioManager>().Length;
        if (numAM != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound sound in Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();


            sound.source.clip = sound.audClip;
            sound.source.outputAudioMixerGroup = masterMixer;
            sound.source.volume = sound.clipVolume;
            sound.source.pitch = sound.clipPitch;
            sound.source.loop = sound.canLoop;
            sound.source.panStereo = sound.panStereo;
            sound.source.spatialBlend = sound.spacialBlend;
            sound.source.minDistance = sound.minSoundDistance;
            sound.source.maxDistance = sound.maxSoundDistance;
            sound.source.rolloffMode = AudioRolloffMode.Linear;
            sound.source.playOnAwake = false;
        }

        PlayMenuMusic();

        Cursor.SetCursor(glassTexture, hotSpot, cursorMode);
    }


    #region Sound Controls

    /// <summary>
    /// Gets the name of a sound and plays it at its point
    /// </summary>
    /// <param name="audioName">the sound name to play</param>
    public void Play(string audioName)
    {
        //Searches through the Sound array until it finds a sound with the 
        //specified name
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.Play();
        }
    }

    /// <summary>
    /// Gets the name of a sound and stops it at its point
    /// </summary>
    /// <param name="audioName">the sound name to stop</param>
    public void Stop(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.Stop();
        }

    }


    /// <summary>
    /// Gets the name of a sound and pauses it at its point
    /// </summary>
    /// <param name="audioName">the sound name to pause</param>
    public void Pause(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.Pause();
        }
    }


    /// <summary>
    /// Gets the name of a sound and resumes it at its point
    /// </summary>
    /// <param name="audioName">the sound name to resume</param>
    public void UnPause(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.UnPause();
        }
    }

    /// <summary>
    /// Gets the name of a sound and disables its volume when it starts playing
    /// </summary>
    /// <param name="audioName">the sound name to play muted</param>
    public void PlayMuted(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.Play();
            sound.source.volume = 0.0f;
        }
    }

    /// <summary>
    /// Gets the name of a sound and mutes it
    /// </summary>
    /// <param name="audioName">the sound name to mute</param>
    public void Mute(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.volume = 0.0f;
        }
    }

    /// <summary>
    /// Gets the name of a sound and unmutes it
    /// </summary>
    /// <param name="audioName">the sound name to unmute</param>
    public void Unmute(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.volume = musicVolume;
        }
    }



    #endregion

    #region Play Music

    public void StopAllSounds()
    {
        for(int i=3; i<Sounds.Length; i++)
        {
            Sounds[i].source.Stop();
        }
    }

    /// <summary>
    /// Plays menu music and stops other tracks
    /// </summary>
    public void PlayMenuMusic()
    {
        Stop("InterrogationBGM");
        Stop("GameBGM");
        Stop("TutorialBGM");
        Unmute("MenuBGM");
        Play("MenuBGM");
    }

    /// <summary>
    /// Plays tutorial music and stops other tracks
    /// </summary>
    public void PlayTutorialMusic()
    {
        Mute("InterrogationBGM");
        Mute("GameBGM");
        Play("TutorialBGM");
        Mute("MenuBGM");
    }

    /// <summary>
    /// Plays game music and mutes/stops other tracks
    /// </summary>
    public void PlayGameMusic()
    {
        if (!gameStarted)
        {
            PlayMuted("InterrogationBGM");
            Play("GameBGM");
            gameStarted = true;
        }
        if (inInterrogation)
        {
            Unmute("GameBGM");
            Mute("InterrogationBGM");
            inInterrogation = false;
        }
        else
        {
            Play("GameBGM");
        }
        Mute("TutorialBGM");
        Mute("MenuBGM");
        Mute("ChoirBGM");
    }

    /// <summary>
    /// Plays interrogation music and stops/mutes other tracks
    /// </summary>
    public void PlayInterrogationMusic()
    {
        inInterrogation = true;
        Unmute("InterrogationBGM");
        Mute("GameBGM");
        Mute("ChoirBGM");
    }

    public void StopInterrogationMusic()
    {
        inInterrogation = false;
        Mute("InterrogationBGM");
        Unmute("GameBGM");
    }

    public void PlayCorporateLadderRoomMusic()
    {
        Mute("GameBGM");
        Mute("InterrogationBGM");
        Unmute("ChoirBGM");
        Play("ChoirBGM");
    }

    public void StopCorporateLadderRoomMusic()
    {
        Stop("ChoirBGM");
        Unmute("GameBGM");
    }

    public void PlayFootsteps()
    {
        footstepTrack = UnityEngine.Random.Range(1, 4);
        if (footstepTrack == 1)
        {
            Play("FootstepOne");
        }
        else if (footstepTrack == 2)
        {
            Play("FootstepTwo");
        }
        else if (footstepTrack == 3)
        {
            Play("FootstepThree");
        }
    }

    public void PlayDoor()
    {
        Play("Door");
    }

    public void PlayPauseMusic()
    {
        for (int i=0; i<4; i++)
        {
            if (Sounds[i].source.volume > 0)
            {
                previousTrack = Sounds[i].name;
            }
        }
        print(previousTrack);
        if(previousTrack != "TutorialBGM")
        {
            PlayMuted(previousTrack);
            Unmute("TutorialBGM");
            //Play("TutorialBGM");
        }
    }

    public void EndPauseMusic()
    {
        Mute("TutorialBGM");
        Unmute(previousTrack);
    }

    public IEnumerator Typing()
    {

        while (true)
        {
            Play("Typing");
            //float wait = UnityEngine.Random.Range(.2f, .6f);
            yield return new WaitForSeconds(2);
        }
    }
    #endregion
}
