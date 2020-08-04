using UnityEngine;

/// <summary>
/// To easily play soundFXs on awake or to link to other classes to quickly play sfx using the provided public variables.
/// </summary>
public class SoundHandlerGlobal : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(2, 10)]
    public string notes = "To easily play soundFXs on awake or to link to other classes to quickly play sfx using the provided public variables.";

    public Sound[] sounds;

    [Tooltip("If enabled the sound will start playing automatically when Start() method is called.")]
    [SerializeField] private bool playOnStart = false;

    [SerializeField] private bool playAllSounds = false;

    [Tooltip("Check to send this sound to the Unstoppable AudioSource.")]
    [SerializeField] private bool isUnstoppable = false;

    private void Start()
    {
        if (playOnStart)
            PlaySound(0, isUnstoppable);
    }

    public void PlaySound(int index = 0, bool unstoppable = false)
    {
        if (sounds.Length > 0)
        {
            if (playAllSounds)
            {
                for (int i = 0; i < sounds.Length; i++)
                    if (sounds[i] != null)
                        SoundManager.Instance.SFX_PlayOneShot(sounds[i].clip, sounds[i].Volume,
                            sounds[i].MinPitch, sounds[i].MaxPitch, unstoppable);
            }
            else
            {
                if (index <= sounds.Length - 1 && sounds[index] != null)
                    SoundManager.Instance.SFX_PlayOneShot(sounds[index].clip,sounds[index].Volume,
                        sounds[index].MinPitch, sounds[index].MaxPitch, unstoppable);
            }
        }
    }

    public void StopSound()
    {
        SoundManager.Instance.SFX_Stop();
    }
}
