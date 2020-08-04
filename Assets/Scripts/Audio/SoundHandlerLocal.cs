using UnityEngine;

/// <summary>
/// Any gameobject with this class will have an AudioSource and will be able to easily play sounds on start() 
/// or link to other classes to quickly play sounds using the provided public variables.
/// </summary>
public class SoundHandlerLocal : SoundEmitter
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(6, 11)]
    public string notes = "Any gameobject with this class will have an AudioSource and will be able to easily play sounds on start() " +
        "or link to other classes to quickly play sounds using the provided public variables. Make sure to disable AudioSource Play " +
        "On Awake.";

    public Sound[] sounds;

    [SerializeField] private bool isLoop = false;

    [Tooltip("If enabled the sound will start playing automatically when Start() method is called.")]
    [SerializeField] private bool isPlayOnStart = false;

    [SerializeField] private bool isPlayAllSounds = false;

    private void Start()
    {
        if (isLoop)
            _Source.loop = true;
        CheckIfReady();
        if (isPlayOnStart)
            PlaySound();
    }

    public void PlaySound(int index = 0)
    {
        if (isPlayAllSounds)
        {
            for (int i = 0; i < sounds.Length; i++)
                PlaySound(sounds[i].clip, sounds[i].Volume, sounds[i].MinPitch, sounds[i].MaxPitch, true);
        }
        else
            PlaySound(sounds[index].clip, sounds[index].Volume, sounds[index].MinPitch, sounds[index].MaxPitch, true);
    }

    public void PlaySound(AudioClip clip, float volume, float minPitch, float maxPitch, bool isPlayOneShot = false)
    {
        _Source.volume = volume;
        float pitch = Random.Range(minPitch, maxPitch);
        _Source.pitch = pitch;

        if (isPlayOneShot)
        {
            _Source.PlayOneShot(clip);
            return;
        }
        _Source.clip = clip;
        _Source.Play();
    }

    public void StopSound()
    {
        _Source.Stop();
    }

    private void CheckIfReady()
    {
        if (sounds.Length == 0)
        {
            Debug.LogError(gameObject.name + ": No sound classes!");
            Debug.Break();
            return;
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i] == null)
            {
                Debug.LogError(gameObject.name + ": Missing sound class!");
                Debug.Break();
                return;
            }
        }
    }
}
