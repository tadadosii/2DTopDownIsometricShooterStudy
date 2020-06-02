using System.Collections;
using UnityEngine;

/// <summary>
/// To easily access to a bunch of useful Audio actions and simply have 2 audiosources on the scene
/// one for the music and the other one for the sound fxs
/// </summary>
public class SoundManager : Singleton<SoundManager>
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    #region ---------------------------- PROPERTIES & UNITY CALLBACKS
    private AudioSource sfx;
    private AudioSource music;
    private void Awake()
    {
        if (transform.Find("SoundFXs") == null)
        {
            sfx = new GameObject("SoundFXs").AddComponent<AudioSource>();
            sfx.transform.SetParent(transform);
        }
        if (transform.Find("Music") == null)
        {
            music = new GameObject("Music").AddComponent<AudioSource>();
            music.loop = true;
            music.transform.SetParent(transform);
        }
    }
    #endregion

    #region ---------------------------- SFX
    public void SFX_PlayOneShot(AudioClip clip, float volume, float minPitch = 1f, float maxPitch = 1f)
    {
        if (clip != null && music != null)
        {
            sfx.pitch = Random.Range(minPitch, maxPitch);
            sfx.PlayOneShot(clip, volume);
        }
    }

    public void SFX_PlayDelayedOneShot(AudioClip clip, float volume, float minPitch = 1f, float maxPitch = 1f, float delay = 1f)
    {
        StartCoroutine(CO_SFX_PlayDelayedOneShot(clip, volume, minPitch, maxPitch, delay));
    }

    private IEnumerator CO_SFX_PlayDelayedOneShot(AudioClip clip, float volume, float minPitch, float maxPitch, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (clip != null && music != null)
        {
            sfx.pitch = Random.Range(minPitch, maxPitch);
            sfx.PlayOneShot(clip, volume);
        }
    }

    public void SetSFXVolume(float value)
    {
        if (sfx != null)
            sfx.volume = value;
    }

    public void StopSFX()
    {
        if (sfx != null)
            sfx.Stop();
    }
    #endregion

    #region ---------------------------- MUSIC
    public void Music_PlayOneShot(AudioClip clip, float volume)
    {
        if (clip != null && music != null)
            music.PlayOneShot(clip, volume);
    }

    public void Music_Play(AudioClip clip)
    {
        if (clip != null && music != null)
        {
            music.clip = clip;
            music.Play();
        }
    }

    public void SetMusicVolume(float value)
    {
        if (music != null)
            music.volume = value;
    }
    #endregion
}
