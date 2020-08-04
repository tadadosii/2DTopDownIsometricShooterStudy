using UnityEngine;

/// <summary>
/// This class currently has a method that plays a single audio clip by calling the SoundManager music methods. The volume can be manually
/// set on the Inspector and it also has a bool called playOnAwake that can be checked to play the audio clip when the Start() method happens.
/// </summary>
public class MusicHandler : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(4, 10)]
    public string notes = "This class currently has a method that plays a single audio clip by calling the SoundManager music methods. The volume can be manually " +
        "set on the Inspector and it also has a bool called playOnAwake that can be checked to play the audio clip when the Start() method happens.";

    [SerializeField] private float volume = 0.65f;
    public AudioClip[] musicClips;
    [SerializeField] private bool playOnAwake = true;

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
                SoundManager.Instance.Music_SetVolume(volume);
                SoundManager.Instance.Music_Play(musicClips[index]);
            }
        }
    }
}
