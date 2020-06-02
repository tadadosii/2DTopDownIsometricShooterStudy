using UnityEngine;

/// <summary>
/// To quickly add to a gameobject and simply set it to play a clip on awake
/// </summary>
public class MusicHandler : MonoBehaviour
{    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public float volume;
    public AudioClip[] musicClips;
    public bool playOnAwake;

    private void Start()
    {
        if (playOnAwake)
            PlaySound();
    }

    public void PlaySound(int index = 0)
    {
        if (musicClips.Length > 0)
        {
            if (index <= musicClips.Length - 1 && musicClips[index] != null)
            {
                SoundManager.Instance.SetMusicVolume(volume);
                SoundManager.Instance.Music_Play(musicClips[index]);
            }
        }
    }
}
