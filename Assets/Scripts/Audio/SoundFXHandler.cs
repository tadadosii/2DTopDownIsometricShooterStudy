using UnityEngine;

/// <summary>
/// To easily play soundFXs on awake or to link to other classes to quickly play sfx using the provided public variables
/// </summary>
public class SoundFXHandler : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public float volume;
    public float minPitch;
    public float maxPitch;
    public AudioClip[] soundFXs;
    public bool playOnAwake;
    public bool playAllSounds;

    private void OnEnable()
    {
        if (playOnAwake)
            PlaySound();
    }

    public void PlaySound(int index = 0)
    {
        if (soundFXs.Length > 0)
        {
            if (playAllSounds)
            {
                for (int i = 0; i < soundFXs.Length; i++)
                    if (soundFXs[i] != null)
                        SoundManager.Instance.SFX_PlayOneShot(soundFXs[i], volume, minPitch, maxPitch);
            }
            else
            {
                if (index <= soundFXs.Length - 1 && soundFXs[index] != null)
                    SoundManager.Instance.SFX_PlayOneShot(soundFXs[index], volume, minPitch, maxPitch);
            }
        }
    }

    public void StopSound()
    {
        SoundManager.Instance.StopSFX();
    }
}
